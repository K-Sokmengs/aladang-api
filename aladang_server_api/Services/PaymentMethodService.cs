using System;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using aladang_server_api.Helpers;

namespace aladang_server_api.Services
{
	public class PaymentMethodService : IPaymentMethod
	{
        private readonly IConfiguration _configuration; 
        private AppDBContext _contex;
        double pageResult = 10;


        public PaymentMethodService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;
        }

        public double Count()
        {
            return _contex.paymentMethods!.Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.paymentMethods!.Count() / pageResult)!;
        }


        public List<PaymentMethod> GetAll()
        {
            var paymentMethods = _contex.paymentMethods!.OrderByDescending(d => d.id).OrderBy(c=>c.methodname).ToList();
            if (paymentMethods != null)
            {
                return paymentMethods!;
            }

            return null!;
        }


        public List<PaymentMethod> GetPaymentMethods(int page)
        {
            if (page != 0)
            {
                var location = _contex.paymentMethods!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }


        public PaymentMethod GetPaymentMethodById(int id)
        {
            PaymentMethod paymentMethod = _contex.paymentMethods!.Where(l => l.id == id).SingleOrDefault()!;
            if (paymentMethod != null)
            {
                return paymentMethod;
            }
            return null!;

        }

        public PaymentMethod CreateNew(PaymentMethod req)
        {
            req.createdate = DateTime.Now;
            _contex.Add(req);
            _contex.SaveChanges();
            PaymentMethod result = _contex.paymentMethods!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;

        }

        public PaymentMethod Update(PaymentMethod req)
        {
            PaymentMethod paymentMethod = _contex.paymentMethods!.FirstOrDefault(c => c.id == req.id)!;
            paymentMethod.methodname = req.methodname;
            paymentMethod.createby = req.createby;
            paymentMethod.createdate = req.createdate;
            paymentMethod.status = req.status;
            _contex.SaveChanges();
            PaymentMethod result = _contex.paymentMethods!.Where(u => u.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }
    }
}

