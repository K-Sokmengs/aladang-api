using System;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using aladang_server_api.Models.BO.Req;

namespace aladang_server_api.Services
{
	public class LocationService :  ILocation
	{
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;

        double pageResult = 10;
        public LocationService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration; 
            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _contex.locations!.Count();
        }
        public double Count(string status)
        {
            return _contex.locations!.Where(b => b.active == status).Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.locations!.Count() / pageResult)!;
        }
        public double PageCount(string status)
        {
            return Math.Ceiling((double)_contex.locations!.Where(b => b.active == status).Count() / pageResult)!;
        }

        public List<Locations> GetAll()
        {
            var location = _contex.locations!.OrderByDescending(d => d.id).ToList();
            if (location != null)
            { 
                return location!;
            }

            return null!;
        }

        public List<Locations> GetLocations(int page)
        {
            if (page != 0) {
                var location = _contex.locations!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }

        public List<Locations> GetByStatus(string status, int page)
        {
            if (page != 0)
            {
                var location = _contex.locations!
                    .Where(s => s.active == status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult) 
                    .ToList();
                return location!;
            }

            return null!;
        }

        public Locations GetById(int id)
        {
            Locations temItemList = _contex.locations!.Find(id)!;
            if (temItemList != null)
            {
                return temItemList;
            }
            return null!;

        }

        public Locations CreateNew(Locations req)
        {
            req.active = "True";
            _contex.Add(req);
            _contex.SaveChanges();
            var result = _contex.locations!.Where(u => u.id == req.id).FirstOrDefault();
            return result!;
            //return CreatedAtAction(nameof(GetAll), new { id = req.id }, req);
        }

        public Locations Update(Locations req)
        {

            var locations = _contex.locations!.SingleOrDefault(c => c.id == req.id)!;
            locations.location = req.location;
            locations.active = req.active;
            _contex.SaveChanges();
            Locations result = _contex.locations!.Where(c => c.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }

    }
}

