using System;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace aladang_server_api.Services
{
    public class BannerService : IBanner
    {
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;
        double pageResult = 10;

        public BannerService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }
        public double Count()
        {
            return _contex.banners!.Count();
        }

        public double Count(int shopid)
        {
            return _contex.banners!.Where(s => s.shopid == shopid).Count();
        }


        public double PageCount()
        {
            return Math.Ceiling((double)_contex.banners!.Count() / pageResult)!;
        }
        public double PageCount(int shopid)
        {
            return Math.Ceiling((double)_contex.banners!.Where(b => b.shopid == shopid).Count() / pageResult)!;
        }


        public List<Banner> GetAll()
        {
            var banners = _contex.banners!
                    .OrderByDescending(d => d.id)
                    .ToList();
            if (banners != null)
            {
                return banners!;
            }

            return null!;
        }
       

        public List<Banner> GetBanners(int page)
        {
            if (page != 0)
            {
                var location = _contex.banners!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }

        public List<Banner> GetByShopId(int shopid,int page)
        {
            if (page != 0)
            {
                var location = _contex.banners!
                    .Where(b => b.shopid == shopid)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }


        public Banner GetById(int id)
        {
            Banner banner = _contex.banners!.Where(l => l.id == id).SingleOrDefault()!;
            if (banner != null)
            {
                return banner;
            }
            return null!;

        }

        public Banner CreateNew(Banner req)
        {
            req.date = DateTime.Now;
            _contex.Add(req);
            _contex.SaveChanges();
            Banner result = _contex.banners!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;

        }

        public  Banner Update(Banner req)
        {
            Banner banner = _contex.banners!.FirstOrDefault(c => c.id == req.id)!;
            banner.date = req.date;
            banner.userid = req.userid;
            banner.shopid = req.shopid;
            banner.exireddate = req.exireddate;
            banner.qtymonth = req.qtymonth;
            banner.bannerimage = req.bannerimage;
            banner.bannerstatus = req.bannerstatus;
            _contex.SaveChanges();
            Banner result = _contex.banners!.Where(u => u.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }
    }
}

