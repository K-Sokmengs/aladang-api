using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models.BO.Res;

public class ProductImageDetailRes
{
    public int Id { get; set; }
    public int ProductId { get; set; } 
    public string? Url { get; set; }
    public string? Name { get; set; }
    public int? OrderIndex { get; set; }
    
    public void setData(ProductImageDetail data)
    {
        Id = data.Id;
        ProductId = data.ProductId;
        Url = data.Url;
        Name = data.Name;
        OrderIndex = data.OrderIndex;
    }
}