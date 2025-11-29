using System;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;

namespace aladang_server_api.Interface
{
	public interface IOrder
	{
        public List<Order> GetAll();
        public List<Order> GetOrder(int page);
        public List<Order> GetByShopId(int shopid, int page, string status);
        public List<Order> GetByShopId(int shopid);
        public List<Order> GetByCustomerId(int customerid, int page, string status);
        public List<Order> GetByCustomer(int shopid);
        public List<Order> GetByStatus(string status, int page);

        public double Count();
        public double CountS(int shopid, string status);
        public double CountS(int shopid);
        public double CountC(int customerid, string status);
        public double Count(string status);

        public double PageCount();
        public double PageCountS(int shopid, string status);
        public double PageCountC(int customerid, string status);
        public double PageCount(string status);

        

        public Order GetById(int id);
        public Order Update(Order req);
       /* public Order CreateNew(Order req);*/
        public List<Order> CreateNew(OrderMultiReq req);

        public List<OrderTracking> GetOrderTrackingByOrderId(int orderId);
        public OrderTracking UpdateTracking(OrderTrackingRequest req);
	}
}

