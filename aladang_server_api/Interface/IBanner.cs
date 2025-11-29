using System;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;

namespace aladang_server_api.Interface
{
    public interface IBanner
    {
        public List<Banner> GetAll();
        public List<Banner> GetBanners(int page);
        public double Count();
        public double PageCount();

        public double Count(int shopid);
        public double PageCount(int shopid);
        public List<Banner> GetByShopId(int shopid, int page);

        public Banner GetById(int id);
        public Banner Update(Banner obj);
        public Banner CreateNew(Banner req);
    }
}

