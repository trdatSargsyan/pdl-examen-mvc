using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class PriceDto
{

    public int FormulePriceId { get; set; }


    [Required]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [RegularExpression("^\\d{1,8}(?:\\.\\d{1,2})?$", ErrorMessage = "Max 8 digits before decimal and Max 2 digits after (.).")]
    public double PriceDay { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [RegularExpression("^\\d{1,8}(?:\\.\\d{1,2})?$", ErrorMessage = "Max 8 digits before decimal and Max 2 digits after (.).")]
    public double PriceWeek { get; set; }
    public int CarId{ get; set; }
}

public class PriceCreationDto
{
    [Required]
    [RegularExpression("^\\d{1,8}(?:\\.\\d{1,2})?$", ErrorMessage = "Max 8 digits before decimal and Max 2 digits after (.).")]
    public double PriceDay { get; set; }
    [Required]
    [RegularExpression("^\\d{1,8}(?:\\.\\d{1,2})?$", ErrorMessage = "Max 8 digits before decimal and Max 2 digits after (.).")]
    public double PriceWeek { get; set; }
    public int CarId { get; set; }
    public List<CarDto> Cars { get; set; }
    public int AeroportId { get; set; }
}

public class PricesDto
{
    public double PriceDay { get; set; }
    public double PriceWeek { get; set; }
    public int CarId { get; set; }
}