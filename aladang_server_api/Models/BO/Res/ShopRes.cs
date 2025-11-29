using System;
using System.ComponentModel.DataAnnotations;
using aladang_server_api.Models.BO.Req;

namespace aladang_server_api.Models.BO.Res
{
	public class ShopRes
	{
        [Key]
        public int id { get; set; }
        
        public string? shopid { get; set; }
        public string? shopName { get; set; }
        public string? gender { get; set; }
        public DateTime? dob { get; set; }
        public string? nationality { get; set; }
        public string? ownerName { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public string? tokenid { get; set; }
        public string? facebookPage { get; set; }
        public string? location { get; set; }
        public string? logoShop { get; set; }
        public string? paymentType { get; set; }
        public string? qrCodeImage { get; set; }
        public int? bankNameid { get; set; }
        public string? accountNumber { get; set; }
        public string? accountName { get; set; }
        public string? feetype { get; set; }
        public decimal? feecharge { get; set; }
        public DateTime? shophistorydate { get; set; }
        public string? note { get; set; }
        public string? status { get; set; }
        public string? idcard { get; set; }
        public DateTime? expiredate { get; set; }

        public void setData(Shop data)
        {
            this.id = data.id;
            this.shopid = data.shopid;
            this.shopName = data.shopName;
            this.gender = data.gender;
            this.dob = data.dob;
            this.nationality = data.nationality;
            this.ownerName = data.ownerName;
            this.phone = data.phone;
            this.password = data.password;
            this.tokenid = data.tokenid;
            this.facebookPage = data.facebookPage;
            this.location = data.location;
            this.logoShop = data.logoShop;
            this.paymentType = data.paymentType;
            this.qrCodeImage = data.qrCodeImage;
            this.bankNameid = data.bankNameid;
            this.accountNumber = data.accountNumber;
            this.accountName = data.accountName;
            this.feetype = data.feetype;
            this.feecharge = data.feecharge;
            this.shophistorydate = data.shophistorydate;
            this.note = data.note;
            this.status = data.status;
            this.idcard = data.idcard;
            this.expiredate = data.expiredate;
        }
    }
}

