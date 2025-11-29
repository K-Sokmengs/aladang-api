using System.Configuration;
using System.Text;
using EntityFrameworkCore.UseRowNumberForPaging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models.AppAuthorize;
using aladang_server_api.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"),
    opt =>
    {
        opt.UseRowNumberForPaging();
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }));

//////token
//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IUserServiceWithToken, UserServicewithToken>();
//======
//model service====
builder.Services.AddTransient<CustomerService, CustomerServiceImpl>();
builder.Services.AddTransient<ShopService, ShopServiceImp>();
builder.Services.AddTransient<IDeliveryType, DeliveryTypeService>();
builder.Services.AddTransient<AppUserLoginService, AppUserLoginServiceImpl>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IBanner, BannerService>();
builder.Services.AddTransient<ICurrency, CurrencyService>();
builder.Services.AddTransient<IExchange, ExchangeService>();
builder.Services.AddTransient<ILocation, LocationService>();
builder.Services.AddTransient<IOrderDetail, OrderDetailService>();
builder.Services.AddTransient<IOrder, OrderService>();
builder.Services.AddTransient<IProduct, ProductService>();

builder.Services.AddTransient<IPaymentMethod, PaymentMethodService>();
builder.Services.AddTransient<IPrivacy, PrivacyService>();
builder.Services.AddTransient<IProductImage, ProductImageService>();
builder.Services.AddTransient<IQRCode, QRCodeService>();
builder.Services.AddTransient<IReport, ReportService>();
builder.Services.AddTransient<IReportType, ReportTypeService>();
builder.Services.AddTransient<IShopPayment, ShopPaymentService>();
builder.Services.AddTransient<ISetUpFee, SetUpFeeService>();
builder.Services.AddTransient<IAppLoginUser, AppLoginUserService>();


//builder.Services.AddScoped<IJwtUtils, JwtUtils>();

/// end token ///


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "WebAPI",
        Description = "Sambocapp WebAPI"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                }
            },
            new List < string > ()
        }
    });
});

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = ConfigurationManagerApp.AppSetting["JWT:ValidIssuer"],
        ValidAudience = ConfigurationManagerApp.AppSetting["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagerApp.AppSetting["JWT:Secret"]))
    };
});




var app = builder.Build();

///
////////////
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"imagesUpload")),
    RequestPath = new PathString("/imagesUpload")
});


///////////////

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//    app.UseSwagger();
//    app.UseSwaggerUI(options => {
//        options.SwaggerEndpoint("/swagger/V1/swagger.json", "Sambocapp WebAPI");
//        options.RoutePrefix = string.Empty;
//    });
//}

app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/V1/swagger.json", "Sambocapp WebAPI");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
//app.UseRouting();
//Quick and Easy Exception Handling
app.UseExceptionHandler(c => c.Run(async context =>
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()
        .Error;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    var response = new { error = exception.Message };
    await context.Response.WriteAsJsonAsync(response);
}));
///end Quick and Easy Exception Handling
///////tken
///
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


//end token
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

