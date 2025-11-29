using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface IPaymentMethod
	{
        public double Count();
        public double PageCount();

        public List<PaymentMethod> GetAll();
        public List<PaymentMethod> GetPaymentMethods(int page);
        public PaymentMethod GetPaymentMethodById(int id);
        public PaymentMethod Update(PaymentMethod obj);
        public PaymentMethod CreateNew(PaymentMethod req);
    }
}

