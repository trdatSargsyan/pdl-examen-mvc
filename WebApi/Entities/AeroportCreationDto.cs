using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace WebApi.Entities;

public class AeroportCreationDto
{
    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 120 characters")]
    [RegularExpression("^[a-zA-Z \\-\\/_]*$", ErrorMessage = "Only Alphabets, Dashes ans Underscores allowed.")]
    public string Name { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 120 characters")]
    public string Address { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers allowed")]
    public string Phone { get; set; }

    public int CountryId { get; set; }
}
