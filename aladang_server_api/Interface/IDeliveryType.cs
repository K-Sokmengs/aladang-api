using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
    public interface IDeliveryType
    {
        public List<DeliveryType> GetAll();
        public List<DeliveryType> GetDeliveryTypes(int page);
        public double Count();
        public double PageCount();

        public double Count(string status);
        public double PageCount(string status);
        public List<DeliveryType> GetByStatus(string status,int page);

        public DeliveryType GetById(int id);
        public DeliveryType Update(DeliveryType req);
        public DeliveryType CreateNew(DeliveryType req);
    }

}

