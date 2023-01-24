
namespace WebApi.Models;

public class Aeroport
{
    public int AeroportId { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public Country Country { get; set; }

    public ICollection<Car> Cars { get; set; }

    public ICollection<FormulePrice> Prices { get; set; }

   
}
