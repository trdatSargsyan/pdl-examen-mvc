namespace WebApi.Entities;

public class CreditCardDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CardTypeName { get; set; }
    public int CardType { get; set; } 
    public string CardNumber { get; set; }
    public DateTime DateValid { get; set; } = DateTime.Now;
    public int CVV { get; set; }
}

public class CreditCardCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CardType { get; set; }
    public string CreditTypeName { get; set; }
    public string CardNumber { get; set; }
    public DateTime DateValid { get; set; }
    public int CVV { get; set; }
    public string UserId { get; set; }//AuthId
}
