using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Reservation
{
    [Key]
    public int Id { get; set; }
    public DateTime Start_Date { get; set; }
    public DateTime  End_Date { get; set; }
    public int Start_Km { get; set; }

    public Car Car { get; set; }

    public Bill Bill { get; set; }

    public Client Client { get; set; }
    
    
}
