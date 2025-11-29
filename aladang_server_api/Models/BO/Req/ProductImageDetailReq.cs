using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models.BO.Req;

public class ProductImageDetailReq
{
    public int Id { get; set; }
    public int ProductId { get; set; } 
    public string? Url { get; set; }
    public string? Name { get; set; }
    public int? OrderIndex { get; set; }
}