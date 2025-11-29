using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface IShopPayment
	{
        public double Count();
        public double PageCount();

        public List<ShopPayment> GetAll();
        public List<ShopPayment> GetShopPayments(int page);
        public ShopPayment GetShopPaymentById(int id);
        public ShopPayment Update(ShopPayment obj);
        public ShopPayment CreateNew(ShopPayment req);
    }
}

