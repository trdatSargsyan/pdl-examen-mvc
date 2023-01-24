using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class ReservationIndexDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; } = null!;
    [DataType(DataType.Date)]
    public DateTime ProductionDate { get; set; }
    public double Km { get; set; }
    public int Seater { get; set; }
    public string Picture { get; set; } //IFromFile
    public PriceDto PriceDto { get; set; }
}

public class ReservationDetailsDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; } = null!;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime ProductionDate { get; set; }
    public double Km { get; set; }
    public int Seater { get; set; }

    public TypeOfCarDto TypeOfCarDto { get; set; }
    public MotorDto MotorDto { get; set; }
    public GearboxDto GearboxDto { get; set; }
    public PriceDto PriceDto { get; set; }

    public int AeroportId { get; set; }

    public double Amount { get; set; }
    public ResDates ResDates { get; set; }
}


public class ReservationCreationDto
{
    public int carId { get; set; }
    public DateTime sDate { get; set; }
    public DateTime eDate { get; set; }
    public double Total { get; set; }
    public string UserId { get; set; }
}

public class ResDates
{
    [DataType(DataType.Date)]
    public DateTime Start_Date { get; set; }

    [DataType(DataType.Date)]
    public DateTime End_Date { get; set; }
}

public class ReservationUI
{
    public int ReservationId { get; set; }
    public string UserId { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Start_Date { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime End_Date { get; set; }
    public int CarId { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public double Solde { get; set; }
}

public class ReservationsForAdminUI
{
    public int ReservationId { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [DataType(DataType.Date)]
    public DateTime Start_Date { get; set; }
    [DataType(DataType.Date)]
    public DateTime End_Date { get; set; }
    public double Solde { get; set; }
}