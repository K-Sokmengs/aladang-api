using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
    [Table("product_image_tbl")]
	public class ProductImage
	{
        [Key]
        public int id { get; set; }
        public int productid { get; set; }
        public string? productimage { get; set; }
    }
}

