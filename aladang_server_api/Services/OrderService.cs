using System;
using System.Net.NetworkInformation;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using aladang_server_api.Migrations;
using aladang_server_api.Models.BO.Req;
using Microsoft.EntityFrameworkCore;

namespace aladang_server_api.Services
{
	public class OrderService : IOrder
	{
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;
        double pageResult = 10;
        public OrderService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _contex.orders!.Count();
        }
        public double Count(string status)
        {
            return _contex.orders!.Where(s => s.Status == status).Count();
        }
        public double CountS(int shopid)
        {
            return _contex.orders!.Where(s => s.FromShopId == shopid).Count();
        }
        public double CountS(int shopid, string status)
        {
            if (status.ToLower() == "all")
            {
                return _contex.orders!.Where(s => s.FromShopId == shopid).Count();
            }
            return _contex.orders!.Where(s => s.FromShopId == shopid && s.Status==status).Count();
        }
        public double CountC(int customerid, string status)
        {
            if (status.ToLower() == "all")
            {
                return _contex.orders!.Where(s => s.CustomerId == customerid).Count();
            }
                return _contex.orders!.Where(s => s.CustomerId == customerid && s.Status==status).Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.orders!.Count() / pageResult)!;
        }

        public double PageCount(string status)
        {
            return Math.Ceiling((double)_contex.orders!.Where(s=>s.Status==status).Count() / pageResult)!;
        }
        public double PageCountS(int shopid, string status)
        {
            if (status.ToLower() == "all")
            {
                return Math.Ceiling((double)_contex.orders!.Where(s => s.FromShopId == shopid).Count() / pageResult)!;
            }
                return Math.Ceiling((double)_contex.orders!.Where(s => s.FromShopId == shopid && s.Status == status).Count() / pageResult)!;
        }

        public double PageCountC(int customerid, string status)
        {
            if (status.ToLower() == "all")
            {
                return Math.Ceiling((double)_contex.orders!.Where(s => s.CustomerId == customerid).Count() / pageResult)!;
            }
                return Math.Ceiling((double)_contex.orders!.Where(s=>s.CustomerId==customerid && s.Status == status).Count() / pageResult)!;
        }

        public List<Order> GetAll()
        {
            var orders = _contex.orders!
                    .OrderByDescending(d => d.id) 
                    .ToList();
            if (orders != null)
            { 
                return orders!;
            }

            return null!;
        }


        public List<Order> GetOrder(int page)
        {
            if (page != 0)
            {
                var location = _contex.orders!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }
            return null!;
        }

        public List<Order> GetByShopId(int shopid)
        {
            if (shopid != 0)
            {
                var location = _contex.orders!
                   .Where(s => s.FromShopId == shopid)
                   .ToList();
                return location!;
            }

            return null!;
        }


        public List<Order> GetByShopId(int shopid, int page, string status)
        {
            if (page != 0)
            {
                if (status.ToLower() == "all")
                {
                    var orders = _contex.orders!
                    .Where(s => s.FromShopId == shopid)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                    return orders!;
                }
                var location = _contex.orders!
                .Where(s => s.FromShopId == shopid && s.Status == status)
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)

                .ToList();
                return location!;
            }

            return null!;
        }

        public List<Order> GetByCustomerId(int customerid, int page, string status)
        {
            if (page != 0)
            {
                if (status.ToLower() == "all")
                {
                    var orders = _contex.orders!
                    .Where(s => s.CustomerId == customerid)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult) 
                    .ToList();
                    return orders!;
                }
                    var location = _contex.orders!
                    .Where(s => s.CustomerId == customerid && s.Status==status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    
                    .ToList();
                return location!;
            }

            return null!;
        }


        public List<Order> GetByCustomer(int shopid)
        {
            
            var location = _contex.orders!.Where(s => s.CustomerId == 0 && s.FromShopId==shopid).ToList();
            if (location != null)
            {
                return location!;
            } 
            return null!;
        }

        public Order GetById(int id)
        {
            var order = _contex.orders!.Where(l => l.id == id).SingleOrDefault()!;
            if (order != null)
            {
                return order;
            }
            return null!;
        }

        public List<Order> GetByStatus(string status,int page)
        {
            if (page != 0)
            {
                if (status.ToLower() == "all")
                {
                    var orders = _contex.orders!
                    .Where(s => s.Status == status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult) 
                    .ToList();
                    return orders!;
                }
                    var location = _contex.orders!
                    .Where(s => s.Status == status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    
                    .ToList();
                return location!;
            }

            return null!;

        }

        public List<Order> CreateNew(OrderMultiReq req)
        {
            List<Order> createdOrders = new List<Order>();

            try
            {
                foreach (var shopId in req.FromShopIds)
                {
                    // get invoice number for THIS shop
                    using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase")))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(
                            "SELECT CASE WHEN MAX(InvoiceNo) IS NULL THEN 1 ELSE MAX(InvoiceNo)+1 END FROM Orders WHERE FromShopId=@shopId",
                            conn);
                        cmd.Parameters.AddWithValue("@shopId", shopId);
                        int invNo = Convert.ToInt32(cmd.ExecuteScalar());

                        // create order object
                        Order newOrder = new Order
                        {
                            FromShopId = shopId,
                            InvoiceNo = invNo,
                            Date = DateTime.Now,
                            CustomerId = req.CustomerId,
                            DeliveryTypeIn = req.DeliveryTypeIn,
                            CurrentLocation = req.CurrentLocation,
                            Phone = req.Phone,
                            PaymentType = req.PaymentType,
                            QrcodeShopName = req.QrcodeShopName,
                            BankName = req.BankName,
                            AccountName = req.AccountName,
                            AccountNumber = req.AccountNumber,
                            ReceiptUpload = req.ReceiptUpload,
                            AmountTobePaid = req.AmountTobePaid,
                            ExchangeId = req.ExchangeId,
                            Status = "Paid"
                        };

                        _contex.orders.Add(newOrder);
                        _contex.SaveChanges();

                        // tracking 3 steps
                        AddTracking(newOrder.id, req.CustomerId, "Packed");
                        AddTracking(newOrder.id, req.CustomerId, "Transported");
                        AddTracking(newOrder.id, req.CustomerId, "Customer");

                        createdOrders.Add(newOrder);
                    }
                }

                return createdOrders;
            }
            catch
            {
                return null;
            }
        }

        // helper tracking method
        private void AddTracking(int orderId, int customerId, string trackingName)
        {
            var tracking = new OrderTracking
            {
                Status = "Padding",
                OrderId = orderId,
                DeliveryTrackingTypeId = _contex.DeliveryTrackingTypes
                    .Where(c => c.Status == "ACT" && c.Name == trackingName)
                    .Select(x => x.Id)
                    .FirstOrDefault(),
                CreatedBy = customerId.ToString(),
                CreatedDate = DateTime.Now,
                Remark = "First Order"
            };

            _contex.OrderTrackings.Add(tracking);
            _contex.SaveChanges();
        }

        /*  public Order CreateNew(Order req)
          {
              try
              {
                  SqlConnection cond = new SqlConnection();
                  cond.ConnectionString = _configuration.GetConnectionString("WebApiDatabase");
                  cond.Open();
                  SqlCommand cmd = new SqlCommand("SELECT case when MAX(InvoiceNo) is null then 1 else MAX(InvoiceNo)+1 end as id from Orders where FromShopId=" + req.FromShopId, cond);
                  int max_invno = Convert.ToInt16(cmd.ExecuteScalar());
                  cond.Close();

                  req.InvoiceNo = max_invno;
                  req.Date = DateTime.Now;
                  req.Status = "Paid";
                  _contex.Add(req);
                  _contex.SaveChanges();
                  Order result = _contex.orders!.Where(u => u.id == req.id).FirstOrDefault()!;
                  // insert tracking all
                 var orderTrackingPacked = new OrderTracking
                 {
                     Status = "Padding",
                     OrderId = result.id,
                     DeliveryTrackingTypeId = _contex.DeliveryTrackingTypes?.Where(c => c.Status == "ACT" && c.Name=="Packed").SingleOrDefault()?.Id??0,
                     CreatedBy = req.CustomerId.ToString(),
                     CreatedDate = DateTime.Now,
                     Remark = "First Order"
                 };
                 _contex.Add(orderTrackingPacked);
                 _contex.SaveChanges();

                 var orderTrackingTransported = new OrderTracking
                 {
                     Status = "Padding",
                     OrderId = result.id,
                     DeliveryTrackingTypeId = _contex.DeliveryTrackingTypes?.Where(c => c.Status == "ACT" && c.Name=="Transported").SingleOrDefault()?.Id??0,
                     CreatedBy = req.CustomerId.ToString(),
                     CreatedDate = DateTime.Now,
                     Remark = "First Order"
                 };
                 _contex.Add(orderTrackingTransported);
                 _contex.SaveChanges();

                 var orderTrackingCustomer = new OrderTracking
                 {
                     Status = "Padding",
                     OrderId = result.id,
                     DeliveryTrackingTypeId = _contex.DeliveryTrackingTypes?.Where(c => c.Status == "ACT" && c.Name=="Customer").SingleOrDefault()?.Id??0,
                     CreatedBy = req.CustomerId.ToString(),
                     CreatedDate = DateTime.Now,
                     Remark = "First Order"
                 };
                 _contex.Add(orderTrackingCustomer);
                 _contex.SaveChanges();

                  return result;
              }
              catch(Exception e)
              {
                  return null!;
              }

          }
  */
        public List<OrderTracking> GetOrderTrackingByOrderId(int orderId)
        {
            return _contex.OrderTrackings
                .Include(c => c.DeliveryTrackingType).Where(c => c.OrderId == orderId)
                .OrderBy(c => c.DeliveryTrackingType!.OrderIndex)
                .ToList();
        }

        public OrderTracking UpdateTracking(OrderTrackingRequest req)
        {
            var orderTracking =_contex.OrderTrackings.SingleOrDefault(c => c.Id == req.Id && c.OrderId == req.OrderId);
            if (orderTracking == null) return null;
            orderTracking.UpdatedBy = req.ShopId.ToString();
            orderTracking.UpdatedDate = DateTime.Now;
            orderTracking.Remark = req.Remark;
            orderTracking.Status = req.Status;
            _contex.Update(orderTracking);
            _contex.SaveChanges();
            return orderTracking;
        }

        public Order Update(Order req)
        {
            Order order = _contex.orders!.FirstOrDefault(c => c.id == req.id)!;
            order.InvoiceNo = req.InvoiceNo;
            order.Date = req.Date;
            order.ShopId = req.ShopId;
            order.CustomerId = req.CustomerId;
            order.DeliveryTypeIn = req.DeliveryTypeIn;
            order.CurrentLocation = req.CurrentLocation;
            order.Phone = req.Phone;
            order.PaymentType = req.PaymentType;
            order.QrcodeShopName = req.QrcodeShopName;
            order.BankName = req.BankName;
            order.AccountName = req.AccountName;
            order.AccountNumber = req.AccountNumber;
            order.ReceiptUpload = req.ReceiptUpload;
            order.AmountTobePaid = req.AmountTobePaid;
            order.ExchangeId = req.ExchangeId;
            order.Status = req.Status;
            order.DeliveryAmount = req.DeliveryAmount;
            order.TaxAmount = req.TaxAmount;
            order.DiscountAmount = req.DiscountAmount;
            _contex.SaveChanges();
            Order result = _contex.orders!.Where(c => c.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;
        }


        
    }
}

