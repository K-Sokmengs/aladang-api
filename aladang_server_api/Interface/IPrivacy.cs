using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface IPrivacy
	{
        public double Count();
        public double PageCount();

        public List<Privacy> GetAll();
        public List<Privacy> GetPrivacies(int page);
        public Privacy GetPrivacyById(int id);
        public Privacy Update(Privacy obj);
        public Privacy CreateNew(Privacy req);
    }
}

