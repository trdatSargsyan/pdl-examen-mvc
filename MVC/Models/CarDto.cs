using System.ComponentModel.DataAnnotations;

namespace MVC.Models;
public class CarDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ProductionDate { get; set; }
    public double Km { get; set; }
    public int Seater { get; set; }
    public string Picture { get; set; }//
    public TypeOfCarDto TypeOfCarDto { get; set; }
    public GearboxDto GearboxDto { get; set; }
    public MotorDto MotorDto { get; set; }
    public PricesDto Prices { get; set; }
    public AeroportDto AeroportDto { get; set; }
}
public class CarCreationDto
{
    [Required]
    public string Brand { get; set; }

    [Required]
    public string Model { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ProductionDate { get; set; }

    [Required]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers allowed")]
    public double Km { get; set; }

    [Required]
    public IFormFile Picture { get; set; }

    [Required]
    public int Seater { get; set; }
    public int AeroportId { get; set; }
    public int MotorId { get; set; }
    public int GearboxId { get; set; }
    public int TypeOfCarId { get; set; }
    public List<GearboxDto> GearboxDto { get; set; } = new();
    public List<MotorDto> MotorDtos { get; set; } = new();
    public List<TypeOfCarDto> TypeOfCarDto { get; set; } = new();
}

public class CarForReservation
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }
    public double Price { get; set; }
}

public class CarEditDto
{
    public string Brand { get; set; }
    public string Model { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ProductionDate { get; set; }
    public double Km { get; set; }
    public string Picture { get; set; }
    public int Seater { get; set; }
    public int AeroportId { get; set; }
    public int MotorId { get; set; }
    public int GearboxId { get; set; }
    public int TypeOfCarId { get; set; }
    public List<GearboxDto> GearboxDto { get; set; } = new();
    public List<MotorDto> MotorDtos { get; set; } = new();
    public List<TypeOfCarDto> TypeOfCarDto { get; set; } = new();
}

