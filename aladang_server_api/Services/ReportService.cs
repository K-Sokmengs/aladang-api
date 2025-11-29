using System;
using aladang_server_api.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.CodeAnalysis;
using aladang_server_api.Migrations;
using System.Net.NetworkInformation;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.ViewModel;

namespace aladang_server_api.Services
{
	public class ReportService : IReport
	{
        private readonly IConfiguration _configuration;
        private AppDBContext _context; 


        public ReportService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _context = dbConnection;
            _configuration = configuration;
        }

        public List<OrderDetailViewModel> GetOrderDetail()
        {
            SqlConnection conx = new SqlConnection();
            conx.ConnectionString = _configuration.GetConnectionString("WebApiDatabase"); 
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT
            orders.Id,orders.FromShopId,orderdetail_tbl.productid,Products.ProductCode,Products.ProductName,orderdetail_tbl.qty,orderdetail_tbl.price,orderdetail_tbl.discount,
            Orders.InvoiceNo,Orders.[Date],Orders.ShopId,tbl_shop.shopName,Orders.CustomerId,tbl_customer.customerName,Orders.DeliveryTypeIn
            ,Orders.CurrentLocation,Orders.Phone,Orders.PaymentType,Orders.BankName,Orders.AccountName,Orders.AccountNumber,Orders.ReceiptUpload,orders.AmountTobePaid,Orders.ExchangeId,Orders.[Status]
            from Orders INNER join orderdetail_tbl on orderdetail_tbl.orderid=Orders.Id INNER join Products on Products.Id=orderdetail_tbl.productid LEFT join tbl_customer on tbl_customer.id=Orders.CustomerId left join tbl_shop on tbl_shop.id=Orders.ShopId", conx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<OrderDetailViewModel> res = new List<OrderDetailViewModel>();

            foreach(DataRow row in dt.Rows)
            {
                OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel();
                orderDetailViewModel.fromshopid = Convert.ToInt16(row["FromShopId"]);
                orderDetailViewModel.orderid = Convert.ToInt16(row["id"]);
                orderDetailViewModel.productid = Convert.ToInt16(row["productid"]);
                orderDetailViewModel.productcode = row["productcode"].ToString();
                orderDetailViewModel.productname = row["productname"].ToString();
                orderDetailViewModel.qty = Convert.ToInt16(row["qty"]);
                orderDetailViewModel.price = Convert.ToDecimal(row["price"]);
                orderDetailViewModel.discount = Convert.ToDecimal(row["discount"]);
                orderDetailViewModel.invoiceno = Convert.ToInt16(row["invoiceno"]);
                orderDetailViewModel.Date = Convert.ToDateTime(row["Date"]);
                orderDetailViewModel.shopId = Convert.ToInt16(row["shopId"]);
                orderDetailViewModel.shopName = row["shopName"].ToString();
                orderDetailViewModel.customerId = Convert.ToInt16(row["customerId"]);
                orderDetailViewModel.customerName = row["customerName"].ToString();
                orderDetailViewModel.deliveryTypeIn = row["deliveryTypeIn"].ToString();
                orderDetailViewModel.currentLocation = row["currentLocation"].ToString();
                orderDetailViewModel.phone = row["phone"].ToString();
                orderDetailViewModel.paymentType = row["paymentType"].ToString();
                orderDetailViewModel.bankName = row["bankName"].ToString();
                orderDetailViewModel.accountName = row["accountName"].ToString();
                orderDetailViewModel.accountNumber = row["accountNumber"].ToString();
                orderDetailViewModel.receiptUpload = row["receiptUpload"].ToString();
                orderDetailViewModel.amountTobePaid = Convert.ToDecimal(row["amountTobePaid"]);
                orderDetailViewModel.exchangeId = Convert.ToInt16(row["exchangeId"]);
                orderDetailViewModel.Status = row["Status"].ToString();
                res.Add(orderDetailViewModel);
            }

            return res;
        }

        public List<OrderDetailViewModel> GetOrderDetailByShopId(int id)
        {
            SqlConnection conx = new SqlConnection();
            conx.ConnectionString = _configuration.GetConnectionString("WebApiDatabase");
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT
            orders.Id,orders.FromShopId,Orders.FromShopId,orderdetail_tbl.productid,Products.ProductCode,Products.ProductName,orderdetail_tbl.qty,orderdetail_tbl.price,orderdetail_tbl.discount,
            Orders.InvoiceNo,Orders.[Date],Orders.ShopId,tbl_shop.shopName,Orders.CustomerId,tbl_customer.customerName,Orders.DeliveryTypeIn
            ,Orders.CurrentLocation,Orders.Phone,Orders.PaymentType,Orders.BankName,Orders.AccountName,Orders.AccountNumber,Orders.ReceiptUpload,orders.AmountTobePaid,Orders.ExchangeId,Orders.[Status]
            from Orders INNER join orderdetail_tbl on orderdetail_tbl.orderid=Orders.Id INNER join Products on Products.Id=orderdetail_tbl.productid LEFT join tbl_customer on tbl_customer.id=Orders.CustomerId left join tbl_shop on tbl_shop.id=Orders.ShopId where Orders.FromShopId=" + id, conx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<OrderDetailViewModel> res = new List<OrderDetailViewModel>();

            foreach (DataRow row in dt.Rows)
            {
                OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel();
                orderDetailViewModel.orderid = Convert.ToInt16(row["id"]);
                orderDetailViewModel.fromshopid = Convert.ToInt16(row["FromShopId"]);
                orderDetailViewModel.productid = Convert.ToInt16(row["productid"]);
                orderDetailViewModel.productcode = row["productcode"].ToString();
                orderDetailViewModel.productname = row["productname"].ToString();
                orderDetailViewModel.qty = Convert.ToInt16(row["qty"]);
                orderDetailViewModel.price = Convert.ToDecimal(row["price"]);
                orderDetailViewModel.discount = Convert.ToDecimal(row["discount"]);
                orderDetailViewModel.invoiceno = Convert.ToInt16(row["invoiceno"]);
                orderDetailViewModel.Date = Convert.ToDateTime(row["Date"]);
                orderDetailViewModel.shopId = Convert.ToInt16(row["shopId"]);
                orderDetailViewModel.shopName = row["shopName"].ToString();
                orderDetailViewModel.customerId = Convert.ToInt16(row["customerId"]);
                orderDetailViewModel.customerName = row["customerName"].ToString();
                orderDetailViewModel.deliveryTypeIn = row["deliveryTypeIn"].ToString();
                orderDetailViewModel.currentLocation = row["currentLocation"].ToString();
                orderDetailViewModel.phone = row["phone"].ToString();
                orderDetailViewModel.paymentType = row["paymentType"].ToString();
                orderDetailViewModel.bankName = row["bankName"].ToString();
                orderDetailViewModel.accountName = row["accountName"].ToString();
                orderDetailViewModel.accountNumber = row["accountNumber"].ToString();
                orderDetailViewModel.receiptUpload = row["receiptUpload"].ToString();
                orderDetailViewModel.amountTobePaid = Convert.ToDecimal(row["amountTobePaid"]);
                orderDetailViewModel.exchangeId = Convert.ToInt16(row["exchangeId"]);
                orderDetailViewModel.Status = row["Status"].ToString();
                res.Add(orderDetailViewModel);
            }

            return res;
        }

    }
}

