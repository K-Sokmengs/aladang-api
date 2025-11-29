using System;
using System.ComponentModel.DataAnnotations;

namespace aladang_server_api.Models.BO.Res
{
	public class DeliveryTypeRes
	{
        [Key]
        public int id { get; set; }
        public string? delivery_name { get; set; }
        public string? status { get; set; }

        public void setData(DeliveryType data)
        {
            this.id = data.id;
            this.delivery_name = data.delivery_name;
            this.status = data.status;
        }

    }
}

