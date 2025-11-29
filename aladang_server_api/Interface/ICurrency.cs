using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface ICurrency
	{
        public List<Currency> GetAll();
        public List<Currency> GetCurrency(int page);
        public double Count();
        public double PageCount();

        public double Count(string status);
        public double PageCount(string status);
        public List<Currency> GetByStatus(string status, int page);


        public Currency GetById(int id);      
        public Currency Update(Currency req);
        public Currency CreateNew(Currency req);
    }
}

