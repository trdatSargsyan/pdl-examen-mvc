namespace WebApi.Models;

public class CreditCard
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CardNumber { get; set; }
    public string CardType { get; set; }
    public DateTime DateValid { get; set; }
    public int CVV { get; set; }
    public Client Client { get; set; }
}
