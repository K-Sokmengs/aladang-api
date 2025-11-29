using System;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;

namespace aladang_server_api.Interface
{
    public interface IProduct
    {
        public List<ProductRes> GetAll();
        public List<ProductRes> GetProduct(int page);
        public List<ProductRes> GetByShopId(int shopid, int page, string status);
        public List<ProductRes> GetByShopId(int shopid);
        public List<ProductRes> GetByStatus(string status, int page);

        public double Count();
        public double Count(string status);
        public double Count(int shopid, string status);
        public double Count(int shopid);

        public double PageCount();
        public double PageCount(string status);
        public double PageCount(int shopid, string status);

        

        public ProductRes GetById(int id);
        public Product Update(ProductReq req);
        public Product CreateNew(ProductReq req);
    }
}

