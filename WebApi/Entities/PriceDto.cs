
using WebApi.Models;

namespace WebApi.Entities;

public class PriceDto
{
    public int FormulePriceId { get; set; }
    public double PriceDay { get; set; }
    public double PriceWeek { get; set; }
    public int CarId { get; set; }
    public List<Car> Cars{ get; set; }
    public int AeroportId { get; set; }

}

public class PriceCreationDto
{
    public double PriceDay { get; set; }
    public double PriceWeek { get; set; }
    public int CarId { get; set; }
    public int AeroportId { get; set; }
}

public class PricesDto
{
    public int CarId { get; set; }
    public double PriceDay { get; set; }
    public double PriceWeek { get; set; }
}

