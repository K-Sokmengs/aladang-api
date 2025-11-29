using System;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;

namespace aladang_server_api.Services
{
	public class ReportTypeService : IReportType
	{
        private readonly IConfiguration _configuration;
        private AppDBContext _contex;
        double pageResult = 10;


        public ReportTypeService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;
        }

        public double Count()
        {
            return _contex.reportTypes!.Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.reportTypes!.Count() / pageResult)!;
        }


        public List<ReportType> GetAll()
        {
            var privacies = _contex.reportTypes!
                    .OrderByDescending(d => d.Id)
                    .ToList();
            if (privacies != null)
            {
                return privacies!;
            }

            return null!;
        }


        public List<ReportType> GetReportTypes(int page)
        {
            if (page != 0)
            {
                var location = _contex.reportTypes!
                    .OrderByDescending(d => d.Id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }


        public ReportType GetReportTypeById(int id)
        {
            ReportType productImage = _contex.reportTypes!.Where(l => l.Id == id).SingleOrDefault()!;
            if (productImage != null)
            {
                return productImage;
            }
            return null!;

        }

        public ReportType CreateNew(ReportType req)
        {
            _contex.Add(req);
            _contex.SaveChanges();
            ReportType result = _contex.reportTypes!.Where(u => u.Id == req.Id).FirstOrDefault()!;
            return result;

        }

        public ReportType Update(ReportType req)
        {
            ReportType reportType = _contex.reportTypes!.FirstOrDefault(c => c.Id == req.Id)!;
            reportType.Name = req.Name;
            _contex.SaveChanges();
            ReportType result = _contex.reportTypes!.Where(u => u.Id == req.Id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }
    }
}

