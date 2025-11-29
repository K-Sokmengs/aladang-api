using System;
using aladang_server_api.Models;
using aladang_server_api.ViewModel;

namespace aladang_server_api.Interface
{
	public interface IOrderDetail
	{
		public List<OrderDetail> GetAll();
		public List<OrderDetail> GetOrderDetail(int page);
        public List<OrderDetail> GetByOrderId(int orderid);
        public List<OrderDetailViewModel> GetOrderDetailViewModel(int orderid);
        public List<OrderDetail> GetByProductId(int productid, int page); 

        public double Count();
        public double CountO(int orderid);
        public double CountP(int productid);

        public double PageCount();
        public double PageCountO(int orderid);
        public double PageCountP(int productid);

        

        public OrderDetail GetById(int id);
        public OrderDetail Update(OrderDetail req);
        public OrderDetail CreateNew(OrderDetail req);
    }
}

