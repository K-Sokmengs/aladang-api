using System;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;

namespace aladang_server_api.Services
{
	public class PrivacyService : IPrivacy
	{
        private readonly IConfiguration _configuration;
        private AppDBContext _contex;
        double pageResult = 10;


        public PrivacyService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;
        }

        public double Count()
        {
            return _contex.privacies!.Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.privacies!.Count() / pageResult)!;
        }


        public List<Privacy> GetAll()
        {
            var privacies = _contex.privacies!
                    .OrderByDescending(d => d.id)
                    .ToList();
            if (privacies != null)
            {
                return privacies!;
            }

            return null!;
        }


        public List<Privacy> GetPrivacies(int page)
        {
            if (page != 0)
            {
                var location = _contex.privacies!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }


        public Privacy GetPrivacyById(int id)
        {
            Privacy privacy = _contex.privacies!.Where(l => l.id == id).SingleOrDefault()!;
            if (privacy != null)
            {
                return privacy;
            }
            return null!;

        }

        public Privacy CreateNew(Privacy req)
        {
            _contex.Add(req);
            _contex.SaveChanges();
            Privacy result = _contex.privacies!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;

        }

        public Privacy Update(Privacy req)
        {
            Privacy privacy = _contex.privacies!.FirstOrDefault(c => c.id == req.id)!;
            privacy.description = req.description;
            _contex.SaveChanges();
            Privacy result = _contex.privacies!.Where(u => u.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }
    }
}

