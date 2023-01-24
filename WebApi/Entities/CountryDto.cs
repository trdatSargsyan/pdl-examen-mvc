using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class CountryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CountryCreationDto
{
    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 120 characters")]
    [RegularExpression("^[a-zA-Z \\-\\/]*$", ErrorMessage = "Only Alphabets and Dashes allowed.")]
    public string Name { get; set; } = null!;

}