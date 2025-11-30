using System;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;

namespace aladang_server_api.Services
{
	public class QRCodeService : IQRCode
	{ 
        private readonly IConfiguration _configuration;
        private AppDBContext _contex;
        double pageResult = 10;


        public QRCodeService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;
        }

        public double Count()
        {
            return _contex.qRCodes!.Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.qRCodes!.Count() / pageResult)!;
        }


        public List<QRCode> GetAll()
        {
            var privacies = _contex.qRCodes!
                    .OrderByDescending(d => d.id)
                    .ToList();
            if (privacies != null)
            {
                return privacies!;
            }

            return null!;
        }


        public List<QRCode> GetQRCodes(int page)
        {
            if (page != 0)
            {
                var location = _contex.qRCodes!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }


        public QRCode GetQRCodeById(int id)
        {
            QRCode productImage = _contex.qRCodes!.Where(l => l.id == id).SingleOrDefault()!;
            if (productImage != null)
            {
                return productImage;
            }
            return null!;
             
        }

        public QRCode CreateNew(QRCode req)
        {
            req.createdate = DateTime.Now;
            _contex.Add(req);
            _contex.SaveChanges();
            QRCode result = _contex.qRCodes!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;

        }

        public QRCode Update(QRCode req)
        {
            var qrcode = _contex.qRCodes!
                .FirstOrDefault(c => c.id == req.id);

            if (qrcode == null)
                return null!;

            qrcode.qrcode = req.qrcode ?? qrcode.qrcode;
            qrcode.createby = req.createby ?? qrcode.createby;
            qrcode.createdate = req.createdate ?? qrcode.createdate;

            _contex.SaveChanges();

            return qrcode;
        }

        /*public QRCode Update(QRCode req)
        {
            QRCode qRCode = _contex.qRCodes!.FirstOrDefault(c => c.id == req.id)!;
            qRCode.qrcode = req.qrcode;
            qRCode.createby = req.createby;
            qRCode.createdate = req.createdate;
            _contex.SaveChanges();
            QRCode result = _contex.qRCodes!.Where(u => u.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }*/
    }
}

