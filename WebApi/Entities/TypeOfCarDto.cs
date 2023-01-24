using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class TypeOfCarDto
{
    [Key]
    public int TypeOfCarId { get; set; }
    public string Type { get; set; } = null!;

}

public class TypeOfCarCreationDto
{
    [Required]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 25 characters")]
    public string Type { get; set; } = null!;
}
