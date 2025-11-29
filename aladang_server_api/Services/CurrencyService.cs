using System;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;

namespace aladang_server_api.Services
{
	public class CurrencyService : ICurrency
	{
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;
        double pageResult = 10;
        public CurrencyService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _contex.currencies!.Count();
        }

        public double Count(string status)
        {
            return _contex.currencies!.Where(s => s.status == status).Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.currencies!.Count() / pageResult)!;
        }
        public double PageCount(string status)
        {
            return Math.Ceiling((double)_contex.currencies!.Where(c => c.status == status).Count() / pageResult)!;
        }


        public List<Currency> GetAll()
        {
            var currencies = _contex.currencies!
                    .OrderByDescending(d => d.id)
                    .ToList();
            if (currencies != null)
            { 
               return currencies!;
            }

            return null!;
        }

        public List<Currency> GetCurrency(int page)
        {
            if (page != 0)
            {
                var location = _contex.currencies!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }

        public List<Currency> GetByStatus(string status,int page)
        {
            if (page != 0)
            {
                var location = _contex.currencies!
                    .Where(s => s.status == status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }

        public Currency GetById(int id)
        {
            var currency = _contex.currencies!.Where(l => l.id == id).SingleOrDefault()!;
            if (currency != null)
            {
                return currency;
            }
            return null!;

        }

        public Currency CreateNew(Currency req)
        {
            _contex.Add(req);
            _contex.SaveChanges();
            Currency result = _contex.currencies!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;

        }

        public Currency Update(Currency req)
        {
            Currency currency = _contex.currencies!.FirstOrDefault(c => c.id == req.id)!;
            currency.currencyname = req.currencyname;
            currency.sign = req.sign;
            currency.status = req.status;
            _contex.SaveChanges();
            Currency result = _contex.currencies!.Where(c => c.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }
    }
}

