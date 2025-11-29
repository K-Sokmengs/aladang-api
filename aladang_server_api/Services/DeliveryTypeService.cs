using System;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;

namespace aladang_server_api.Services
{
	
    public class DeliveryTypeService : IDeliveryType
    {
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;
        //private readonly IWebHostEnvironment webHostEnvironment;
        double pageResult = 10;
        public DeliveryTypeService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _contex.deliveryTypes!.Count();
        }
        public double Count(string status)
        {
            return _contex.deliveryTypes!.Where(s => s.status == status).Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.deliveryTypes!.Count() / pageResult)!;
        }
        public double PageCount(string status)
        {
            return Math.Ceiling((double)_contex.deliveryTypes!.Where(s=>s.status==status).Count() / pageResult)!;
        }

        public List<DeliveryType> GetAll()
        {
            var deliveryTypes = _contex.deliveryTypes!
                .OrderByDescending(d => d.id) 
                .ToList();
            if (deliveryTypes != null)
            { 
                return deliveryTypes!;
            } 
            return null!;
        }

        public List<DeliveryType> GetDeliveryTypes(int page)
        {
            if (page != 0)
            {
                var location = _contex.deliveryTypes!
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
                return location!;
            }

            return null!;
        }


        public List<DeliveryType> GetByStatus(string status,int page)
        {
            if (page != 0)
            {
                var location = _contex.deliveryTypes!
                .Where(s => s.status == status)
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
                return location!;
            }

            return null!;
        }

        public DeliveryType GetById(int id)
        {
            var deliveryType = _contex.deliveryTypes!.Where(l => l.id == id).SingleOrDefault()!;
            if (deliveryType != null)
            {
                return deliveryType;
            }
            return null!;
        }

        public DeliveryType CreateNew(DeliveryType req)
        {
            _contex.Add(req);
            _contex.SaveChanges();
            DeliveryType result = _contex.deliveryTypes!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;
        }

        public DeliveryType Update(DeliveryType req)
        {
            // find record
            var deliveryType = _contex.deliveryTypes!
                .FirstOrDefault(c => c.id == req.id);

            if (deliveryType == null)
            {
                return null!;
            }

            // update all fields
            deliveryType.delivery_name = req.delivery_name;
            deliveryType.status = req.status;
            deliveryType.fee = req.fee;

            // save update
            _contex.deliveryTypes.Update(deliveryType);
            _contex.SaveChanges();

            // return updated result
            return deliveryType;
        }

    }
}

