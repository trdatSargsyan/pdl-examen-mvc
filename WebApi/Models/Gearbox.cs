namespace WebApi.Models;

public class Gearbox
{
    public int Id { get; set; }
    public string Type { get; set; } = null!;
    public ICollection<Car> Cars { get; set; }
}
