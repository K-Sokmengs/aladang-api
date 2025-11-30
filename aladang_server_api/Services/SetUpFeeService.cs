/*using System;
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

*/


using System;
using System.Collections.Generic;
using System.Linq;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.Extensions.Configuration;

namespace aladang_server_api.Services
{
    public class SetUpFeeService : ISetUpFee
    {
        private readonly IConfiguration _configuration;
        private AppDBContext _context;
        double pageResult = 10;

        public SetUpFeeService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _context = dbConnection;
            _configuration = configuration;
        }

        public double Count()
        {
            return _context.setUpFees?.Count() ?? 0;
        }

        public double PageCount()
        {
            var count = _context.setUpFees?.Count() ?? 0;
            return Math.Ceiling(count / pageResult);
        }

        public List<SetUpFee> GetAll()
        {
            var setUpFees = _context.setUpFees?
                    .OrderByDescending(d => d.id)
                    .ToList();

            return setUpFees ?? new List<SetUpFee>();
        }

        public List<SetUpFee> GetSetUpFees(int page)
        {
            if (page <= 0) return new List<SetUpFee>();

            var setUpFees = _context.setUpFees?
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();

            return setUpFees ?? new List<SetUpFee>();
        }

        public SetUpFee GetSetUpFeeById(int id)
        {
            var setUpFee = _context.setUpFees?
                .FirstOrDefault(l => l.id == id);

            return setUpFee;
        }

        public SetUpFee CreateNew(SetUpFee req)
        {
            // Set creation date
            req.createdate = DateTime.Now;

            _context.Add(req);
            _context.SaveChanges();

            var result = _context.setUpFees?
                .FirstOrDefault(u => u.id == req.id);

            return result;
        }

        public SetUpFee Update(SetUpFee req)
        {
            // Find the existing entity
            var setUpFee = _context.setUpFees?
                .FirstOrDefault(c => c.id == req.id);

            if (setUpFee == null)
                return null;

            // Update the properties - only update what should be updated
            // Don't update date and createdate if you want to keep original values
            setUpFee.feetype = req.feetype;
            setUpFee.amount = req.amount;
            setUpFee.createby = req.createby;

            // If you want to update the date field with current time, uncomment below:
            // setUpFee.date = DateTime.Now;

            // If you want to keep the original createdate, don't update it
            // setUpFee.createdate = req.createdate; // This keeps the original creation date

            // FIX: Mark the entity as modified and save changes
            _context.Update(setUpFee);
            _context.SaveChanges();

            // Return the updated entity
            var result = _context.setUpFees?
                .FirstOrDefault(u => u.id == req.id);

            return result;
        }
    }
}