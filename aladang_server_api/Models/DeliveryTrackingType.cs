using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models;

[Table("DeliveryTrackingType")]
public class DeliveryTrackingType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? Name { get; set; }

    [Column(TypeName = "nvarchar(5)")]
    public string? Status { get; set; }

    public int? OrderIndex { get; set; } = 0;
}