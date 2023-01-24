using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class MotorDto
{
    public int Id { get; set; }
    [Required]
    public string Type { get; set; }
}

public class MotorCreationDto
{
    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 120 characters")]
    [RegularExpression("^[a-zA-Z \\-\\/]*$", ErrorMessage = "Only Alphabets and Dashes allowed.")]
    public string Type { get; set; } = null!;
}