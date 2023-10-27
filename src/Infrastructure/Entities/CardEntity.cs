using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
[Table("cards")]
public class Cards : PlatformModel
{
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string Type { get; set; }

    [Column]
    public string Desc { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string ImageUrl { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string ImageUrlSmall { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string ImageUrlCropped { get; set; }

}