namespace WebApi.Models;

public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public DateTime ProductionDate { get; set; }
    public double Km { get; set; }
    public int Seater { get; set; } //2 à 9
    public string Picture { get; set; }
    public Aeroport Aeroport { get; set; }
    public TypeOfCar TypeOfCar { get; set; } 
    public Gearbox Gearbox {get;set;} 
    public Motor Motor {get;set;}
    public FormulePrice Price { get; set; }

    public ICollection<Reservation> Reservations { get; set; }



}
