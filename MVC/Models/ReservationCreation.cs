using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class ReservationCreation
{

    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime? Start_Date { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? End_Date { get; set; } 

    public int CountryId { get; set; }
    public List<Country> Countries { get; set; } = new List<Country>();

    public int AeroportId { get; set; }
    public List<AeroportDto> Aeroports { get; set; } = new List<AeroportDto>();

    public int CarForReservationId { get; set; }
    public List<CarForReservation> CarForReservation { get; set; } = new List<CarForReservation>();

}
