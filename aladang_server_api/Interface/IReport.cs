using System;
using System.Collections;
using aladang_server_api.ViewModel;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface IReport
	{
        public List<OrderDetailViewModel> GetOrderDetail();
        public List<OrderDetailViewModel> GetOrderDetailByShopId(int id);
    }
}

