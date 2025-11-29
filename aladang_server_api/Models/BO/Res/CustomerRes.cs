using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aladang_server_api.Models.BO.Req;

namespace aladang_server_api.Models.BO.Res
{
    //[Table("tbl_customer")]
    public class CustomerRes
    {
        [Key]
        public int id { get; set; }
        public DateTime? date { get; set; }
        public string? phone { get; set; }
        public string? tokenid { get; set; }
        public string? currentLocation { get; set; }
        public string? customerName { get; set; }
        public string? gender { get; set; }
        public string? imageProfile { get; set; }
        public string? password { get; set; }

        public void setData(Customer data)
        {
            this.id = data.id;
            this.date = data.date;
            this.phone = data.phone;
            this.tokenid = data.tokenid;
            this.currentLocation = data.currentLocation;
            this.customerName = data.customerName;
            this.gender = data.gender;
            this.imageProfile = data.imageProfile;
            this.password = data.password;
        }
    }
}

