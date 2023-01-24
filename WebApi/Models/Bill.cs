namespace WebApi.Models;

public class Bill
{
    public int Id { get; set; }
    public double Solde { get; set; }
    public int DistanceTraveled { get; set; }

    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }
}
 