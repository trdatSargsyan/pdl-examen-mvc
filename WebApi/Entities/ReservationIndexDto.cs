namespace WebApi.Entities;
public class ReservationCreationDto
{
    public int carId { get; set; }
    public DateTime sDate { get; set; }
    public DateTime eDate { get; set; }
    public double Total { get; set; }
    public string UserId { get; set; }

}

public class ReservationIndexDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; } = null!;
    public DateTime ProductionDate { get; set; }
    public double Km { get; set; }
    public int Seater { get; set; }
    public string Picture { get; set; }//IfromFile
    public PriceDto PriceDto { get; set; }
}

public class ReservationDetailsDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; } = null!;
    public DateTime ProductionDate { get; set; }
    public double Km { get; set; }
    public int Seater { get; set; }
    public int AeroportId { get; set; }
    public TypeOfCarDto TypeOfCarDto { get; set; }
    public MotorDto MotorDto { get; set; }
    public GearboxDto GearboxDto { get; set; }
    public PriceDto PriceDto { get; set; }
}

public class ResDates
{
    public DateTime Start_Date { get; set; }
    public DateTime End_Date { get; set; }
}

public class ReservationUI {
    public int ReservationId { get; set; }
    public string UserId { get; set; }
    public DateTime Start_Date { get; set; }
    public DateTime End_Date { get; set; }
    public int CarId { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public double Solde { get; set; }
}

public class ReservationForAdminUI
{
    public int ReservationId { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Start_Date { get; set; }
    public DateTime End_Date { get; set; }
    public double Solde { get; set; }
}