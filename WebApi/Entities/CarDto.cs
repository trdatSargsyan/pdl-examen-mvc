using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace WebApi.Entities;

public class CarDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; } = null!;
    public DateTime ProductionDate { get; set; }
    public double Km { get; set; }
    public int Seater { get; set; }
    public string Picture { get; set; } //
    public TypeOfCarDto TypeOfCarDto { get; set; }
    public MotorDto MotorDto { get; set; }
    public GearboxDto GearboxDto { get; set; }
    public PricesDto Prices { get; set; }
    //public FormulePrice PriceDto { get; set; }
    public AeroportDto AeroportDto { get; set; }
}

public class CarCreationDto
{
    [Required]
    public string Brand { get; set; }

    [Required]
    public string Model { get; set; }

    public DateTime ProductionDate { get; set; }

    [Required]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers allowed")]
    public double Km { get; set; }

    [Required]
    public int Seater { get; set; }
    public int GearboxId { get; set; }
    public int MotorId { get; set; }
    public int AeroportId { get; set; }
    public int TypeOfCarId { get; set; }

    [Required]
    public IFormFile Picture { get; set; }

}

public class CarEditModel
{
    public string Brand { get; set; }
    public string Model { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ProductionDate { get; set; }
    public double Km { get; set; }
    public string Picture { get; set; }
    public int Seater { get; set; }
    public int MotorId { get; set; }
    public int GearboxId { get; set; }
    public int TypeOfCarId { get; set; }
    public List<GearboxDto> GearboxDto { get; set; } = new();
    public List<MotorDto> MotorDtos { get; set; } = new();
    public List<TypeOfCarDto> TypeOfCarDto { get; set; } = new();
}


/*

 -------------
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; } = null!;
    public DateTime ProductionDate { get; set; }
    public double Km { get; set; }
    public int Seater { get; set; }
    public string Picture { get; set; } //
    public TypeOfCarDto TypeOfCarDto { get; set; }
    public MotorDto MotorDto { get; set; }
    public GearboxDto GearboxDto { get; set; }

    public FormulePrice PriceDto { get; set; }
    public Aeroport Aeroport { get; set; }
 */


