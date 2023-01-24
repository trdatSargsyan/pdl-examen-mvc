using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class CreditCard
{
    [Required(ErrorMessage ="Select your Card")]
    public int CardType { get; set; }

    [Required(ErrorMessage = "Please fill the field")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 25 characters")]
    [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Dashes allowed.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please fill the field")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 25 characters")]
    [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Dashes allowed.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please fill the field : 16 numbers")]
    public string CardNumber { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateValid { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Please fill the field : 3 numbers")]
    public int CVV { get; set; }
    public double Amount { get; set; }

    public int CarId { get; set; } //For Back Page
    public string UserId { get; set; } //AuthId
}

public enum CardType
{
    MasterCard, Visa, AmericanExpress, Discover, JCB
}