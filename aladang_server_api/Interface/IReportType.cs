using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface IReportType
	{
        public double Count();
        public double PageCount();

        public List<ReportType> GetAll();
        public List<ReportType> GetReportTypes(int page);
        public ReportType GetReportTypeById(int id);
        public ReportType Update(ReportType obj);
        public ReportType CreateNew(ReportType req);
    }
}

