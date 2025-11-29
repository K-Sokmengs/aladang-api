using System;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aladang_server_api.Services
{
	public class ExchangeService : IExchange
    {
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;
        double pageResult = 10;
        public ExchangeService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _contex.exchanges!.Count();
        }

        public double Count(int shopid)
        {
            return _contex.exchanges!.Where(s => s.shopid == shopid).Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.exchanges!.Count() / pageResult)!;
        }
        public double PageCount(int shopid)
        {
            return Math.Ceiling((double)_contex.exchanges!.Where(b => b.shopid == shopid).Count() / pageResult)!;
        }

        public List<Exchange> GetAll()
        {
            var exchanges = _contex.exchanges!
                .OrderByDescending(d => d.id) 
                .ToList();
            if (exchanges != null)
            {
                return exchanges!;
            }

            return null!;
        }

        public List<Exchange> GetExchange(int page)
        {
            if (page != 0)
            {
                var location = _contex.exchanges!
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }

        public Exchange GetExchangeRateByShop(int shopid)
        {
            if (shopid != 0)
            {
                var location = _contex.exchanges!
                .Where(s => s.shopid == shopid)
                .OrderByDescending(d => d.id)
                .Take(1)
                .FirstOrDefault();
                return location!;
            }

            return null!;
        }

        public List<Exchange> GetAllByShopId(int shopid)
        {
            if (shopid != 0)
            {
                var location = _contex.exchanges!
                .Where(s => s.shopid == shopid)
                .OrderByDescending(d => d.id) 
                .Take(1)
                .ToList();
                return location!;
            }

            return null!;
        }

        public Exchange GetById(int id)
        {
            var exchange = _contex.exchanges!.Where(l => l.id == id).SingleOrDefault()!;
            if (exchange != null)
            {
                return exchange;
            }
            return null!;
        }

        public Exchange GetMax(int shopid)
        {
            var exchange = _contex.exchanges!.Where(l => l.shopid == shopid).OrderByDescending(c =>c.id).Take(1).SingleOrDefault()!;
            if (exchange != null)
            {
                return exchange;
            }
            return null!;
        }

        public Exchange CreateNew(Exchange req)
        {
            req.date = DateTime.Now;
            _contex.Add(req);
            _contex.SaveChanges();
            Exchange result = _contex.exchanges!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;
        }

        public Exchange Update(Exchange req)
        {
            Exchange exchange = _contex.exchanges!.FirstOrDefault(c => c.id == req.id)!;
            //exchange.date = req.date;
            exchange.currencyid = req.currencyid;
            exchange.shopid = req.shopid;
            exchange.rate = req.rate;
            _contex.SaveChanges();
            Exchange result = _contex.exchanges!.Where(c => c.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;
        }
    }
}

