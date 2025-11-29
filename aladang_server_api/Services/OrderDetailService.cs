using System;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using aladang_server_api.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace aladang_server_api.Services
{
	public class OrderDetailService : IOrderDetail
	{
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;
        double pageResult = 10;
        public OrderDetailService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _contex.orderDetails!.Count();
        }
        public double CountO(int orderid)
        {
            return _contex.orderDetails!.Where(s => s.orderid == orderid).Count();
        }
        public double CountP(int productid)
        {
            return _contex.orderDetails!.Where(s => s.productid == productid).Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.orderDetails!.Count() / pageResult)!;
        }
        public double PageCountO(int orderid)
        {
            return Math.Ceiling((double)_contex.orderDetails!.Where(s=>s.orderid==orderid).Count() / pageResult)!;
        }
        public double PageCountP(int productid)
        {
            return Math.Ceiling((double)_contex.orderDetails!.Where(s=>s.productid==productid).Count() / pageResult)!;
        }

        public List<OrderDetail> GetAll()
        {
            var orderDetails = _contex.orderDetails!
               .OrderByDescending(d => d.id) 
               .ToList();
            if (orderDetails != null)
            { 
                return orderDetails!;
            }

            return null!;
        }


        public List<OrderDetail> GetOrderDetail(int page)
        {
            if (page != 0)
            {
                var location = _contex.orderDetails!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }


        public List<OrderDetail> GetByOrderId(int orderid)
        {
            if (orderid != 0)
            {
                var location = _contex.orderDetails!
                    .Where(s => s.orderid == orderid)
                    .OrderByDescending(d => d.id) 
                    .Take((int)pageResult) 
                    .ToList();
                return location!;
            }

            return null!;
        }

        public List<OrderDetailViewModel> GetOrderDetailViewModel(int orderid)
        {
          
           var result = (from o in _contex.orders
                          join c in _contex.customers! on o.CustomerId equals c.id
                          join od in _contex.orderDetails! on o.id equals od.orderid
                          join p in _contex.products! on od.productid equals p.id
                          join s in _contex.shops! on o.ShopId equals s.id
                         where o.id==orderid
                          select new OrderDetailViewModel
                          {
                              orderid = o.id,
                              productid= p.id,
                              productcode=p.ProductCode,
                              productname=p.ProductName,
                              qty=p.QtyInStock,
                              price=p.Price,
                              discount=od.discount,
                              invoiceno=o.InvoiceNo,
                              Date=o.Date,
                              shopId=o.ShopId,
                              shopName=s.shopName,
                              customerId=c.id,
                              customerName=c.customerName,
                              deliveryTypeIn=o.DeliveryTypeIn,
                              currentLocation=o.CurrentLocation,
                              phone=c.phone,
                              paymentType=o.PaymentType,
                              bankName=o.BankName,
                              accountName=o.AccountName,
                              accountNumber=o.AccountNumber,
                              receiptUpload=o.ReceiptUpload,
                              amountTobePaid=o.AmountTobePaid,
                              exchangeId=o.ExchangeId,
                              Status=o.Status 

                      
                          }).ToList();


            return result;
           
        }
        public List<OrderDetail> GetByProductId(int productid,int page)
        {
            if (page != 0)
            {
                var location = _contex.orderDetails!
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .Where(s=>s.productid==productid)
                .ToList();
                return location!;
            }

            return null!;
        }

        public OrderDetail GetById(int id)
        {
            var orderDetail = _contex.orderDetails!.Where(l => l.id == id).SingleOrDefault()!;
            if (orderDetail != null)
            {
                return orderDetail;
            }
            return null!;
        }

        public OrderDetail CreateNew(OrderDetail req)
        {
            var order = _contex.orders!.Where(u => u.id == req.orderid).FirstOrDefault();
            

            _contex.Add(req);
            _contex.SaveChanges();

            CutStock cutStock = new CutStock();
            cutStock.shopid = order!.FromShopId;
            cutStock.productid = req.productid;
            cutStock.qty = req.qty;
            CutStockProduct(cutStock);
            OrderDetail result = _contex.orderDetails!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;
        }

        public OrderDetail Update(OrderDetail req)
        {
            OrderDetail orderDetail = _contex.orderDetails!.FirstOrDefault(c => c.id == req.id)!;
            orderDetail.orderid = req.orderid;
            orderDetail.productid = req.productid;
            orderDetail.qty = req.qty;
            orderDetail.price = req.price;
            orderDetail.discount = req.discount;
            _contex.SaveChanges();
            OrderDetail result = _contex.orderDetails!.Where(c => c.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;
        }


        public void CutStockProduct(CutStock cutStock)
        {
            Product product = _contex.products!.FirstOrDefault(c => c.ShopId == cutStock.shopid && c.id == cutStock.productid)!;
            if(product != null)
            {
                product.QtyInStock = product.QtyInStock - cutStock.qty;
                _contex.SaveChanges();
            }
            
        }
    }
}

