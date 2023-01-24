using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.AppDbContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> db) : base(db)
    {

    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Aeroport> Aeroports { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<TypeOfCar> TypeOfCars { get; set; }
    public DbSet<Bill> Bill { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<FormulePrice> FormulePrices { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Gearbox> Gearboxes { get; set; }
    public DbSet<Motor> Motors { get; set; }
    public DbSet<CreditCard> CreditCards{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //one to many   
        //Bill
        modelBuilder.Entity<Car>().HasMany(c => c.Reservations).WithOne(e => e.Car);
        modelBuilder.Entity<Client>().HasMany(c => c.Reservations).WithOne(e => e.Client);
        modelBuilder.Entity<Country>().HasMany(c => c.Aeroports).WithOne(e => e.Country);
        //Price

        modelBuilder.Entity<Gearbox>().HasMany(c => c.Cars).WithOne(e => e.Gearbox);
        modelBuilder.Entity<Motor>().HasMany(c => c.Cars).WithOne(e => e.Motor);
        modelBuilder.Entity<TypeOfCar>().HasMany(c => c.Cars).WithOne(e => e.TypeOfCar);

        //Aeroport->Cars,Prices
        modelBuilder.Entity<Aeroport>().HasMany(c => c.Cars).WithOne(e => e.Aeroport);
        modelBuilder.Entity<Aeroport>().HasMany(c => c.Prices).WithOne(e => e.Aeroport);

        //CreditCard->Client
        modelBuilder.Entity<Client>().HasMany(c => c.CreditCards).WithOne(e => e.Client);
        
        //one to one 

        modelBuilder.Entity<Reservation>().HasOne(c => c.Bill).WithOne(e => e.Reservation).
            HasForeignKey<Bill>(x => x.ReservationId);

        modelBuilder.Entity<Car>().HasOne(x => x.Price)
            .WithOne(x => x.Car)
            .HasForeignKey<FormulePrice>(x => x.CarId);
    }
}
