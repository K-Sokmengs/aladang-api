using System;
using aladang_server_api.Models;
using Microsoft.CodeAnalysis;
using aladang_server_api.Models.BO.Req;

namespace aladang_server_api.Interface
{
	public interface ILocation
	{
        public List<Locations> GetAll();
        public List<Locations> GetLocations(int page);
        public double Count();
        public double PageCount();

        public double Count(string status);
        public double PageCount(string status);
        public List<Locations> GetByStatus(string status, int page);
        public Locations GetById(int id);
        public Locations CreateNew(Locations req);
        public Locations Update(Locations req);

        
    }
}

