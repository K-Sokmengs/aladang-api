using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface ISetUpFee
	{
        public double Count();
        public double PageCount();

        public List<SetUpFee> GetAll();
        public List<SetUpFee> GetSetUpFees(int page);
        public SetUpFee GetSetUpFeeById(int id);
        public SetUpFee Update(SetUpFee obj);
        public SetUpFee CreateNew(SetUpFee req);
    }
}

