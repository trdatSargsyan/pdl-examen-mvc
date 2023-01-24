using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Client
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(120)]
    public string FirstName { get; set; } = null!;
    [Required]
    [StringLength(120)]
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;
    public string UserId { get; set; } = null!;

    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<CreditCard> CreditCards { get; set; }
 
}
