using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Country
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(120)]
    public string Name { get; set; } = null!;

    public ICollection<Aeroport> Aeroports { get; set; }

}
