using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class AeroportDto
{
    public int AeroportId { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;

}
public class AeroportCreationDto
{
    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 120 characters")]
    [RegularExpression("^[a-zA-Z \\-\\/_]*$", ErrorMessage = "Only Alphabets, Dashes ans Underscores allowed.")]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 120 characters")]
    public string Address { get; set; } = null!;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression("^[0-9]*$", ErrorMessage ="Only numbers allowed")]
    public string Phone { get; set; } = null!;

    public int CountryId { get; set; }
}
