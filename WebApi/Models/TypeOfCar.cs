using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class TypeOfCar
{
    [Key]
    public int TypeOfCarId { get; set; }
    public string Type { get; set; } = null!;//Normal, Luxe , +5 seater
    
    public ICollection<Car> Cars { get; set; }
}
