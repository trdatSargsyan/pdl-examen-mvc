

using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class TypeOfCarDto
{
    public int TypeOfCarId { get; set; }
    //[Display(Name ="Type")]
    [Required]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 25 characters")]
    public string Type { get; set; }
}

public class TypeOfCarCreationDto
{
    //[Display(Name = ("Type"))]
    [Required]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 25 characters")]
    public string Type { get; set; }
}