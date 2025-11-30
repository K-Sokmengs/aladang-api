/*using System;
using System.Net.NetworkInformation;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aladang_server_api.Services
{
    public class ProductService : IProduct
    {
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;
        double pageResult = 10;
        public ProductService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _contex.products!.Count();
        }

        public double Count(string status)
        {
            return _contex.products!.Where(p => p.Status == status).Count();
        }

        public double Count(int shopid, string status)
        {
            if (status.ToLower() == "all")
            {
                return _contex.products!.Where(p => p.ShopId == shopid).Count();
            }
            return _contex.products!.Where(p => p.ShopId == shopid).Where(s=>s.Status==status).Count();
        }

        public double Count(int shopid)
        {
            return _contex.products!.Where(p => p.ShopId == shopid).Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.products!.Count() / pageResult)!;
        }

        public double PageCount(string status)
        {
            return Math.Ceiling((double)_contex.products!.Where(p => p.Status == status).Count() / pageResult)!;
        }

        public double PageCount(int shopid, string status)
        {
            if (status.ToLower() == "all")
            {
                return Math.Ceiling((double)_contex.products!.Where(p => p.ShopId == shopid).Count() / pageResult)!;
            }
            return Math.Ceiling((double)_contex.products!.Where(p => p.ShopId == shopid).Where(s => s.Status == status).Count() / pageResult)!;
        }

        public List<ProductRes> GetAll()
        {
            var products = _contex.products!
               .OrderByDescending(d => d.id) 
               .ToList();
            var productResList = new List<ProductRes>();
            products.ForEach(product =>
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product); 
                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                listImage.ForEach(image =>
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                });
                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);

            });
            return productResList!;
        }

        public List<ProductRes> GetProduct(int page)
        {
            if (page != 0)
            {
                var products = _contex.products!
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
                var productResList = new List<ProductRes>();
                products.ForEach(product =>
                {
                    var productRes = new ProductRes();
                    productRes.SetProductCreate(product); 
                    var productImageDetailResList = new List<ProductImageDetailRes>();
                    var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                    listImage.ForEach(image =>
                    {
                        var productImageDetail = new ProductImageDetailRes();
                        productImageDetail.setData(image);
                        productImageDetailResList.Add(productImageDetail);
                    });
                    productRes.ProductImageDetails = productImageDetailResList;
                    productResList.Add(productRes);

                });
                return productResList!;
            }

            return null!;
        }


        public List<ProductRes> GetByShopId(int shopid,int page, string status)
        {
            var productResList = new List<ProductRes>();
            if (page != 0)
            {
                if (status == "all")
                {
                    var products = _contex.products!
                        .Where(s => s.ShopId == shopid)
                        .OrderByDescending(d => d.id)
                        .Skip((page - 1) * (int)pageResult) 
                        .Take((int)pageResult)
                        .ToList();
                   
                    products.ForEach(product =>
                    {
                        var productRes = new ProductRes();
                        productRes.SetProductCreate(product); 
                        var productImageDetailResList = new List<ProductImageDetailRes>();
                        var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                        listImage.ForEach(image =>
                        {
                            var productImageDetail = new ProductImageDetailRes();
                            productImageDetail.setData(image);
                            productImageDetailResList.Add(productImageDetail);
                        });
                        productRes.ProductImageDetails = productImageDetailResList;
                        productResList.Add(productRes);

                    });
                    return productResList!;
                }
                else
                {
                    var products = _contex.products!
                        .Where(s => s.ShopId == shopid)
                        .Where(s => s.Status == status)
                        .OrderByDescending(d => d.id)
                        .Skip((page - 1) * (int)pageResult)
                        .Take((int)pageResult) 
                        .ToList();
                    products.ForEach(product =>
                    {
                        var productRes = new ProductRes();
                        productRes.SetProductCreate(product); 
                        var productImageDetailResList = new List<ProductImageDetailRes>();
                        var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                        listImage.ForEach(image =>
                        {
                            var productImageDetail = new ProductImageDetailRes();
                            productImageDetail.setData(image);
                            productImageDetailResList.Add(productImageDetail);
                        });
                        productRes.ProductImageDetails = productImageDetailResList;
                        productResList.Add(productRes);

                    });
                    return  productResList;
                }
              
            }

            return null!;
        }

        public List<ProductRes> GetByShopId(int shopid)
        {
            if (shopid != 0)
            { 
                var products = _contex.products!
                    .Where(s => s.ShopId == shopid) 
                    .Take((int)pageResult)
                    .ToList();
                var productResList = new List<ProductRes>();
                products.ForEach(product =>
                {
                    var productRes = new ProductRes();
                    productRes.SetProductCreate(product); 
                    var productImageDetailResList = new List<ProductImageDetailRes>();
                    var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                    listImage.ForEach(image =>
                    {
                        var productImageDetail = new ProductImageDetailRes();
                        productImageDetail.setData(image);
                        productImageDetailResList.Add(productImageDetail);
                    });
                    productRes.ProductImageDetails = productImageDetailResList;
                    productResList.Add(productRes);

                });
                return productResList!;
            }

            return null!;
        }

        public ProductRes GetById(int id)
        {
            var productRes = new ProductRes();
            var product = _contex.products!.FirstOrDefault(l => l.id == id)!;
            {
                productRes.SetProductCreate(product); 
                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                listImage.ForEach(image =>
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                });
                productRes.ProductImageDetails = productImageDetailResList;
                return productRes;
            }

        }


        public List<ProductRes> GetByStatus(string status,int page)
        {
            if (page != 0)
            {
                var products = _contex.products!
                    .Where(p => p.Status == status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                
                    .ToList();
                var productResList = new List<ProductRes>();
                products.ForEach(product =>
                {
                    var productRes = new ProductRes();
                    productRes.SetProductCreate(product); 
                    var productImageDetailResList = new List<ProductImageDetailRes>();
                    var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                    listImage.ForEach(image =>
                    {
                        var productImageDetail = new ProductImageDetailRes();
                        productImageDetail.setData(image);
                        productImageDetailResList.Add(productImageDetail);
                    });
                    productRes.ProductImageDetails = productImageDetailResList;
                    productResList.Add(productRes);

                });
                return productResList!;
            }

            //List<Product> product = _contex.products!.Where(p =>p.Status==status).ToList();
            //if (product != null)
            //{
            //    return product;
            //}
            return null!;

        }

        public Product CreateNew(ProductReq req)
        {
            Product create = new Product();
            create.SetProductCreate(req);
            _contex.Add(create);
            _contex.SaveChanges();
            var result = _contex.products!.FirstOrDefault(u => u.id == create.id)!;
            if (result.id != 0)
            {
                req.ProductImageDetails.ForEach(data =>
                {
                    var productImageDetail = new ProductImageDetail();
                    productImageDetail.setData(data);
                    productImageDetail.ProductId = result.id;
                    _contex.Add(productImageDetail);
                    _contex.SaveChanges();
                });
            }

            return result;

        }

        public Product Update(ProductReq req)
        {
            Product product = _contex.products!.FirstOrDefault(c => c.id == req.Id)!;
            // product.ShopId = req.ShopId;
            // product.ProductCode = req.ProductCode;
            // product.ProductName = req.ProductName;
            // product.Description = req.Description;
            // product.QtyInStock = req.QtyInStock;
            // product.Price = req.Price;
            // product.NewPrice = req.NewPrice;
            // product.CurrencyId = req.CurrencyId;
            // product.CutStockType = req.CutStockType;
            // product.ExpiredDate = req.ExpiredDate;
            // product.LinkVideo = req.LinkVideo;
            // product.ImageThumbnail = req.ImageThumbnail;
            // product.Status = req.Status;
            product.SetProductCreate(req);
            _contex.Update(product);
            _contex.SaveChanges();
            if (product.id != 0)
            {
                req.ProductImageDetails.ForEach(data =>
                {
                    var findById = _contex.ProductImageDetails!.FirstOrDefault(c=>c.Id==data.Id);
                    if (findById != null)
                    {
                        findById.setData(data);
                        _contex.Update(findById);
                        _contex.SaveChanges();
                    }
                    else
                    {
                        var productImageDetail = new ProductImageDetail();
                        productImageDetail.setData(data);
                        productImageDetail.ProductId = product.id;
                        _contex.Add(productImageDetail);
                        _contex.SaveChanges();
                    }
                 
                });
            }
            return product!;

        }

    }
}

*//*


using System;
using System.Collections.Generic;
using System.Linq;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using Microsoft.Extensions.Configuration;

namespace aladang_server_api.Services
{
    public class ProductService : IProduct
    {
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _context;
        double pageResult = 10;

        public ProductService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _context = dbConnection;
            _configuration = configuration;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _context.products?.Count() ?? 0;
        }

        public double Count(string status)
        {
            return _context.products?.Where(p => p.Status == status).Count() ?? 0;
        }

        public double Count(int shopid, string status)
        {
            if (status?.ToLower() == "all")
            {
                return _context.products?.Where(p => p.ShopId == shopid).Count() ?? 0;
            }
            return _context.products?.Where(p => p.ShopId == shopid && p.Status == status).Count() ?? 0;
        }

        public double Count(int shopid)
        {
            return _context.products?.Where(p => p.ShopId == shopid).Count() ?? 0;
        }

        public double PageCount()
        {
            var count = _context.products?.Count() ?? 0;
            return Math.Ceiling(count / pageResult);
        }

        public double PageCount(string status)
        {
            var count = _context.products?.Where(p => p.Status == status).Count() ?? 0;
            return Math.Ceiling(count / pageResult);
        }

        public double PageCount(int shopid, string status)
        {
            int count;
            if (status?.ToLower() == "all")
            {
                count = _context.products?.Where(p => p.ShopId == shopid).Count() ?? 0;
            }
            else
            {
                count = _context.products?.Where(p => p.ShopId == shopid && p.Status == status).Count() ?? 0;
            }
            return Math.Ceiling(count / pageResult);
        }

        public List<ProductRes> GetAll()
        {
            var products = _context.products?
               .OrderByDescending(d => d.id)
               .ToList() ?? new List<Product>();

            var productResList = new List<ProductRes>();

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public List<ProductRes> GetProduct(int page)
        {
            if (page <= 0) return new List<ProductRes>();

            var products = _context.products?
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList() ?? new List<Product>();

            var productResList = new List<ProductRes>();

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public List<ProductRes> GetByShopId(int shopid, int page, string status)
        {
            if (page <= 0) return new List<ProductRes>();

            var productResList = new List<ProductRes>();
            List<Product> products;

            if (status?.ToLower() == "all")
            {
                products = _context.products?
                    .Where(s => s.ShopId == shopid)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList() ?? new List<Product>();
            }
            else
            {
                products = _context.products?
                    .Where(s => s.ShopId == shopid && s.Status == status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList() ?? new List<Product>();
            }

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public List<ProductRes> GetByShopId(int shopid)
        {
            var products = _context.products?
                .Where(s => s.ShopId == shopid)
                .Take((int)pageResult)
                .ToList() ?? new List<Product>();

            var productResList = new List<ProductRes>();

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public ProductRes GetById(int id)
        {
            var product = _context.products?.FirstOrDefault(l => l.id == id);
            if (product == null) return null;

            var productRes = new ProductRes();
            productRes.SetProductCreate(product);

            var productImageDetailResList = new List<ProductImageDetailRes>();
            var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

            foreach (var image in listImage)
            {
                var productImageDetail = new ProductImageDetailRes();
                productImageDetail.setData(image);
                productImageDetailResList.Add(productImageDetail);
            }

            productRes.ProductImageDetails = productImageDetailResList;
            return productRes;
        }

        public List<ProductRes> GetByStatus(string status, int page)
        {
            if (page <= 0) return new List<ProductRes>();

            var products = _context.products?
                .Where(p => p.Status == status)
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList() ?? new List<Product>();

            var productResList = new List<ProductRes>();

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public Product CreateNew(ProductReq req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            Product create = new Product();
            create.SetProductCreate(req);
            _context.Add(create);
            _context.SaveChanges();

            var result = _context.products?.FirstOrDefault(u => u.id == create.id);
            if (result?.id != 0 && req.ProductImageDetails != null)
            {
                foreach (var data in req.ProductImageDetails)
                {
                    var productImageDetail = new ProductImageDetail();
                    productImageDetail.setData(data);
                    productImageDetail.ProductId = result.id;
                    _context.Add(productImageDetail);
                }
                _context.SaveChanges();
            }

            return result;
        }

        public Product Update(ProductReq req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            Product product = _context.products?.FirstOrDefault(c => c.id == req.Id);
            if (product == null) throw new Exception("Product not found");

            product.SetProductCreate(req);
            _context.Update(product);
            _context.SaveChanges();

            if (product.id != 0 && req.ProductImageDetails != null)
            {
                foreach (var data in req.ProductImageDetails)
                {
                    var findById = _context.ProductImageDetails?.FirstOrDefault(c => c.Id == data.Id);
                    if (findById != null)
                    {
                        findById.setData(data);
                        _context.Update(findById);
                    }
                    else
                    {
                        var productImageDetail = new ProductImageDetail();
                        productImageDetail.setData(data);
                        productImageDetail.ProductId = product.id;
                        _context.Add(productImageDetail);
                    }
                }
                _context.SaveChanges();
            }

            return product;
        }
    }
}*/



using System;
using System.Collections.Generic;
using System.Linq;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace aladang_server_api.Services
{
    public class ProductService : IProduct
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductService> _logger;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _context;
        double pageResult = 10;

        public ProductService(AppDBContext dbConnection, IConfiguration configuration, ILogger<ProductService> logger)
        {
            _context = dbConnection;
            _configuration = configuration;
            _logger = logger;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _context.products?.Count() ?? 0;
        }

        public double Count(string status)
        {
            return _context.products?.Where(p => p.Status == status).Count() ?? 0;
        }

        public double Count(int shopid, string status)
        {
            if (status?.ToLower() == "all")
            {
                return _context.products?.Where(p => p.ShopId == shopid).Count() ?? 0;
            }
            return _context.products?.Where(p => p.ShopId == shopid && p.Status == status).Count() ?? 0;
        }

        public double Count(int shopid)
        {
            return _context.products?.Where(p => p.ShopId == shopid).Count() ?? 0;
        }

        public double PageCount()
        {
            var count = _context.products?.Count() ?? 0;
            return Math.Ceiling(count / pageResult);
        }

        public double PageCount(string status)
        {
            var count = _context.products?.Where(p => p.Status == status).Count() ?? 0;
            return Math.Ceiling(count / pageResult);
        }

        public double PageCount(int shopid, string status)
        {
            int count;
            if (status?.ToLower() == "all")
            {
                count = _context.products?.Where(p => p.ShopId == shopid).Count() ?? 0;
            }
            else
            {
                count = _context.products?.Where(p => p.ShopId == shopid && p.Status == status).Count() ?? 0;
            }
            return Math.Ceiling(count / pageResult);
        }

        public List<ProductRes> GetAll()
        {
            var products = _context.products?
               .OrderByDescending(d => d.id)
               .ToList() ?? new List<Product>();

            var productResList = new List<ProductRes>();

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public List<ProductRes> GetProduct(int page)
        {
            if (page <= 0) return new List<ProductRes>();

            var products = _context.products?
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList() ?? new List<Product>();

            var productResList = new List<ProductRes>();

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public List<ProductRes> GetByShopId(int shopid, int page, string status)
        {
            if (page <= 0) return new List<ProductRes>();

            var productResList = new List<ProductRes>();
            List<Product> products;

            if (status?.ToLower() == "all")
            {
                products = _context.products?
                    .Where(s => s.ShopId == shopid)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList() ?? new List<Product>();
            }
            else
            {
                products = _context.products?
                    .Where(s => s.ShopId == shopid && s.Status == status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList() ?? new List<Product>();
            }

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public List<ProductRes> GetByShopId(int shopid)
        {
            var products = _context.products?
                .Where(s => s.ShopId == shopid)
                .Take((int)pageResult)
                .ToList() ?? new List<Product>();

            var productResList = new List<ProductRes>();

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public ProductRes GetById(int id)
        {
            var product = _context.products?.FirstOrDefault(l => l.id == id);
            if (product == null) return null;

            var productRes = new ProductRes();
            productRes.SetProductCreate(product);

            var productImageDetailResList = new List<ProductImageDetailRes>();
            var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

            foreach (var image in listImage)
            {
                var productImageDetail = new ProductImageDetailRes();
                productImageDetail.setData(image);
                productImageDetailResList.Add(productImageDetail);
            }

            productRes.ProductImageDetails = productImageDetailResList;
            return productRes;
        }

        public List<ProductRes> GetByStatus(string status, int page)
        {
            if (page <= 0) return new List<ProductRes>();

            var products = _context.products?
                .Where(p => p.Status == status)
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList() ?? new List<Product>();

            var productResList = new List<ProductRes>();

            foreach (var product in products)
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product);

                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _context.ProductImageDetails?.Where(c => c.ProductId == product.id).ToList() ?? new List<ProductImageDetail>();

                foreach (var image in listImage)
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                }

                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);
            }

            return productResList;
        }

        public Product CreateNew(ProductReq req)
        {
            if (req == null)
                throw new ArgumentNullException(nameof(req));

            try
            {
                _logger.LogInformation("Starting product creation for ShopId: {ShopId}, ProductName: {ProductName}",
                    req.ShopId, req.ProductName);

                // Validate required fields
                if (!req.ShopId.HasValue || req.ShopId.Value == 0)
                    throw new ArgumentException("ShopId is required");

                if (string.IsNullOrEmpty(req.ProductName))
                    throw new ArgumentException("ProductName is required");

                Product create = new Product();
                create.SetProductCreate(req);

                _logger.LogInformation("Adding product to database");
                _context.Add(create);
                _context.SaveChanges();

                _logger.LogInformation("Product created with ID: {ProductId}", create.id);

                var result = _context.products?.FirstOrDefault(u => u.id == create.id);
                if (result == null)
                    throw new Exception("Failed to retrieve created product");

                // Handle product images
                if (req.ProductImageDetails != null && req.ProductImageDetails.Any())
                {
                    _logger.LogInformation("Adding {ImageCount} product images", req.ProductImageDetails.Count);

                    foreach (var data in req.ProductImageDetails)
                    {
                        var productImageDetail = new ProductImageDetail();
                        productImageDetail.setData(data);
                        productImageDetail.ProductId = result.id;
                        _context.Add(productImageDetail);
                    }
                    _context.SaveChanges();
                    _logger.LogInformation("Product images added successfully");
                }

                _logger.LogInformation("Product creation completed successfully for ID: {ProductId}", result.id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product for ShopId: {ShopId}", req.ShopId);
                throw new Exception($"Failed to create product: {ex.Message}", ex);
            }
        }

        public Product Update(ProductReq req)
        {
            if (req == null)
                throw new ArgumentNullException(nameof(req));

            try
            {
                _logger.LogInformation("Updating product with ID: {ProductId}", req.Id);

                Product product = _context.products?.FirstOrDefault(c => c.id == req.Id);
                if (product == null)
                    throw new Exception($"Product with ID {req.Id} not found");

                product.SetProductCreate(req);
                _context.Update(product);
                _context.SaveChanges();

                if (product.id != 0 && req.ProductImageDetails != null)
                {
                    foreach (var data in req.ProductImageDetails)
                    {
                        var findById = _context.ProductImageDetails?.FirstOrDefault(c => c.Id == data.Id);
                        if (findById != null)
                        {
                            findById.setData(data);
                            _context.Update(findById);
                        }
                        else
                        {
                            var productImageDetail = new ProductImageDetail();
                            productImageDetail.setData(data);
                            productImageDetail.ProductId = product.id;
                            _context.Add(productImageDetail);
                        }
                    }
                    _context.SaveChanges();
                }

                _logger.LogInformation("Product updated successfully for ID: {ProductId}", product.id);
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID: {ProductId}", req.Id);
                throw new Exception($"Failed to update product: {ex.Message}", ex);
            }
        }
    }
}