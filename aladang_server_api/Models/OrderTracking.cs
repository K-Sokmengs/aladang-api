using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models;

[Table("OrderTracking")]
public class OrderTracking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? DeliveryTrackingTypeId { get; set; }

    public int? OrderId { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? Status { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? Remark { get; set; }

    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? UpdatedBy { get; set; }
    
    [ForeignKey("DeliveryTrackingTypeId")]
    public DeliveryTrackingType? DeliveryTrackingType { get; set; }
}