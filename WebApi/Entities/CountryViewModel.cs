using WebApi.Models;

namespace WebApi.Entities;

public class CountryViewModelDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Aeroport> Aeroports { get; set; } 
}
