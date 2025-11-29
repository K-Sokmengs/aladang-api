using System;
using System.ComponentModel.DataAnnotations;

namespace aladang_server_api.Models.BO.Res
{
	public class AppUserLoginRes
	{
        public int id { get; set; }
        public string? shopid { get; set; }
        
        [StringLength(100)]
        public string? shopName { get; set; }
        
        [StringLength(100)]
        public string? gender { get; set; }
        public DateTime? dob { get; set; }
        public string? nationality { get; set; }
        public string? ownerName { get; set; }        
        public string? phone { get; set; }        
        
        public string? tokenid { get; set; }
        public string? facebookPage { get; set; }
        
        public string? location { get; set; }
        public string? logoShop { get; set; }
        
        public string? paymentType { get; set; }
        public string? qrCodeImage { get; set; }        
        public int? bankNameid { get; set; }        
        public string? accountNumber { get; set; }        
        [StringLength(100)]
        public string? accountName { get; set; }       

        public string? feetype { get; set; }
        public decimal? feecharge { get; set; }        
        public DateTime? shophistorydate { get; set; }        
        public string? note { get; set; }
        public string? status { get; set; }
        public string? idcard { get; set; }
        public DateTime? expiredate { get; set; }

        ///////////
        
        public DateTime? customerdate { get; set; }       
        public string? currentLocation { get; set; }
        public string? customerName { get; set; }        
        public string? customerImageProfile { get; set; }

        public void setShopUser(Shop shop)
        {
            id = shop.id;
            shopid = shop.shopid;
            shopName = shop.shopName;
            gender = shop.gender;
            dob = shop.dob!;
            nationality = shop.nationality;
            ownerName = shop.ownerName;
            phone = shop.phone;
            tokenid = shop.tokenid;
            facebookPage = shop.facebookPage;
            location = shop.location;
            logoShop = shop.logoShop;
            paymentType = shop.paymentType;
            qrCodeImage = shop.qrCodeImage;
            bankNameid = shop.bankNameid;
            accountNumber = shop.accountNumber;
            accountName = shop.accountName;
            feetype = shop.feetype;
            feecharge = shop.feecharge;
            shophistorydate = shop.shophistorydate;
            note = shop.note;
            status = shop.status;
            idcard = shop.idcard;
            expiredate = shop.expiredate;
        }

        public void setCustomerUser(Customer cus)
        {
            id = cus.id; 
            gender = cus.gender; 
            phone = cus.phone;
            tokenid = cus.tokenid; 
            expiredate = cus.date;
            currentLocation = cus.currentLocation;
            customerName = cus.customerName;
            customerImageProfile = cus.imageProfile;
        
    }
    }
}

