using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class FormulePrice
{
    [Key]
    public int FormulePriceId { get; set; }
    [Required]
    public double PriceDay { get; set; }
    [Required]
    public double PriceWeek { get; set; }
    public Aeroport Aeroport { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
}
/*
 Id
 PriceDay
 PriceWeek
 Aeroport
 Car
 */