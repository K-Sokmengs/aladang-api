using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface IQRCode
	{
        public double Count();
        public double PageCount();

        public List<QRCode> GetAll();
        public List<QRCode> GetQRCodes(int page);
        public QRCode GetQRCodeById(int id);
        public QRCode Update(QRCode obj);
        public QRCode CreateNew(QRCode req);
    }
}

