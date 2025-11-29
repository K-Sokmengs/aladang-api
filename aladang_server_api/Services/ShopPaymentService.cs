using System;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;

namespace aladang_server_api.Services
{
	public class ShopPaymentService : IShopPayment
	{
        private readonly IConfiguration _configuration;
        private AppDBContext _contex;
        double pageResult = 10;


        public ShopPaymentService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;
        }

        public double Count()
        {
            return _contex.shopPayments!.Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.shopPayments!.Count() / pageResult)!;
        }


        public List<ShopPayment> GetAll()
        {
            var privacies = _contex.shopPayments!
                    .OrderByDescending(d => d.id)
                    .ToList();
            if (privacies != null)
            {
                return privacies!;
            }

            return null!;
        }


        public List<ShopPayment> GetShopPayments(int page)
        {
            if (page != 0)
            {
                var location = _contex.shopPayments!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }


        public ShopPayment GetShopPaymentById(int id)
        {
            ShopPayment productImage = _contex.shopPayments!.Where(l => l.id == id).SingleOrDefault()!;
            if (productImage != null)
            {
                return productImage;
            }
            return null!;

        }

        public ShopPayment CreateNew(ShopPayment req)
        {
            _contex.Add(req);
            _contex.SaveChanges();
            ShopPayment result = _contex.shopPayments!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;

        }

        public ShopPayment Update(ShopPayment req)
        {
            ShopPayment shopPayment = _contex.shopPayments!.FirstOrDefault(c => c.id == req.id)!;
            shopPayment.date = DateTime.Now;
            shopPayment.shopid = req.shopid;
            shopPayment.paytype = req.paytype;
            shopPayment.startdate = req.startdate;
            shopPayment.enddate = req.enddate;
            shopPayment.amount = req.amount;
            _contex.SaveChanges();
            ShopPayment result = _contex.shopPayments!.Where(u => u.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }
    }
}

