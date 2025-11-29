using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface IExchange
	{
        public List<Exchange> GetAll();
        public List<Exchange> GetExchange(int page);
        public double Count();
        public double PageCount();

        public double Count(int shopid);
        public double PageCount(int shopid);
        public Exchange GetExchangeRateByShop(int shopid);
        public List<Exchange> GetAllByShopId(int shopid);

        public Exchange GetById(int id);
        public Exchange GetMax(int shopid);
        public Exchange Update(Exchange req);
        public Exchange CreateNew(Exchange req);
    }
}

