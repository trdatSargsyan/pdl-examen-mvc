using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class GearboxDto
{
    public int Id { get; set; }
    public string Type { get; set; }
}

public class GearboxCreationDto
{
    [Required]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 25 characters")]
    public string Type { get; set; } = null!;
}
