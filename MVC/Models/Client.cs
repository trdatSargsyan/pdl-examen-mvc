using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string UserId { get; set; }
    public string Role { get; set; }

}

public class ClientCreationDto
{
    [Required]
    [Display(Name = "First Name")]
    [StringLength(120)]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(120)]
    public string LastName { get; set; }

    [Required]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers allowed")]
    public string Phone { get; set; }

    [Required]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email is not correct")]
    public string Email { get; set; }
    public string Role { get; set; }
    public string UserId { get; set; }
}