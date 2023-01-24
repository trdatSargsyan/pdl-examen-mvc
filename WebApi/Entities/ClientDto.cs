using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class ClientDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public string Role { get; set; }
    public string UserId { get; set; } 

}

public class ClientCreationDto
{
    [Required(ErrorMessage = "Firstname is riquired")]
    [StringLength(120)]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Lastname is riquired")]
    [StringLength(120)]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Phone no. is riquired")]
    //[RegularExpression("([0-9])", ErrorMessage = "Please enter valid phone no.")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Email is riquired")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = null!;


    [Required(ErrorMessage = "Role is riquired")]
    public string Role { get; set; } = null!;

    [Required(ErrorMessage = "UserId is riquired")]
    public string UserId { get; set; } = null!;
}

public class ClientAuthDto
{
    public string UserId { get; set; }
    public string AccessToken { get; set; }
}