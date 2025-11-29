using System;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;

namespace aladang_server_api.Services 
{
	public class SetUpFeeService : ISetUpFee
	{
        private readonly IConfiguration _configuration;
        private AppDBContext _contex;
        double pageResult = 10;


        public SetUpFeeService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;
        }

        public double Count()
        {
            return _contex.setUpFees!.Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.setUpFees!.Count() / pageResult)!;
        }


        public List<SetUpFee> GetAll()
        {
            var privacies = _contex.setUpFees!
                    .OrderByDescending(d => d.id)
                    .ToList();
            if (privacies != null)
            {
                return privacies!;
            }

            return null!;
        }


        public List<SetUpFee> GetSetUpFees(int page)
        {
            if (page != 0)
            {
                var location = _contex.setUpFees!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }


        public SetUpFee GetSetUpFeeById(int id)
        {
            SetUpFee productImage = _contex.setUpFees!.Where(l => l.id == id).SingleOrDefault()!;
            if (productImage != null)
            {
                return productImage;
            }
            return null!;

        }

        public SetUpFee CreateNew(SetUpFee req)
        {
            _contex.Add(req);
            _contex.SaveChanges();
            SetUpFee result = _contex.setUpFees!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;

        }

        public SetUpFee Update(SetUpFee req)
        {
            SetUpFee setUpFee = _contex.setUpFees!.FirstOrDefault(c => c.id == req.id)!;
            setUpFee.date = DateTime.Now;
            setUpFee.feetype = req.feetype;
            setUpFee.amount = req.amount;
            setUpFee.createby = req.createby;
            setUpFee.createdate = DateTime.Now;
            _contex.SaveChanges();
            SetUpFee result = _contex.setUpFees!.Where(u => u.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }
    }
}

