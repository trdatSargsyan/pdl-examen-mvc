using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.AppDbContext;
using WebApi.Entities;
using WebApi.Halper;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Service;

public class CarService : ICar
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly string containerName = "car";
    private static int _carId;
    public CarService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

        
    //-------------------------------------------
    public async Task<List<CarDto>> GetCars()
    {
        var cars = await _dbContext.Cars.Include(x => x.Aeroport).Include(x => x.Aeroport.Prices)
            .Include(x => x.Gearbox).Include(x => x.TypeOfCar).Include(x => x.Motor)
            .ToListAsync();
        return _mapper.Map<List<CarDto>>(cars);
    }
    public async Task<List<CarDto>> GetCarsByIdCountry(int Id)
    {
        var cars = await _dbContext.Cars.Include(x => x.Aeroport).Include(x => x.Aeroport.Prices)
            .Where(x => x.Aeroport.Country.Id == Id).ToListAsync();

        return _mapper.Map<List<CarDto>>(cars);
    }

    public async Task<List<CarDto>> GetCarsByMotor(int Id)
    {
        var cars = await _dbContext.Cars.Where(x => x.Motor.Id == Id).ToListAsync();
        return _mapper.Map<List<CarDto>>(cars);
    }

    public async Task<CarEditModel> GetCar(int Id)
    {
        var car = await _dbContext.Cars
            .Include(i => i.TypeOfCar).Include(i => i.Gearbox).Include(i => i.Motor)
            .FirstOrDefaultAsync(x => x.Id == Id);
        var map = _mapper.Map<CarEditModel>(car);

        var motors = await GetMotors();
        var gearbox = await GetGearboxes();
        var typeOfCar = await GetTypesOfCars();
        map.GearboxDto = gearbox;
        map.MotorDtos = motors;
        map.TypeOfCarDto = typeOfCar;
        return map;
    }

    public async Task PostCar(CarCreationDto carCreationDto, IFileStorageService fileStorageService)
    {
        var car = _mapper.Map<Car>(carCreationDto);

        if(car.Picture != null)
        {
            car.Picture = await fileStorageService.SaveFile(containerName, carCreationDto.Picture);
        }


        var aeroport = await _dbContext.Aeroports.FirstOrDefaultAsync(x => x.AeroportId == carCreationDto.AeroportId);
        var typeOfCar = await _dbContext.TypeOfCars.FirstOrDefaultAsync(x => x.TypeOfCarId == carCreationDto.TypeOfCarId);
        var gearbox = await _dbContext.Gearboxes.FirstOrDefaultAsync(x => x.Id == carCreationDto.GearboxId);
        var motor = await _dbContext.Motors.FirstOrDefaultAsync(x => x.Id == carCreationDto.MotorId);
        
        car.Aeroport = aeroport;
        car.TypeOfCar = typeOfCar;
        car.Motor = motor;
        car.Gearbox = gearbox;
        _dbContext.Cars.Add(car);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditCar(int Id, CarCreationDto carCreationDto, IFileStorageService fileStorageService)
    {
        var car = await _dbContext.Cars
           .Include(i => i.TypeOfCar).Include(i => i.Gearbox).Include(i => i.Motor)
           .FirstOrDefaultAsync(x => x.Id == Id);
        car = _mapper.Map<Car>(carCreationDto);

        if (car.Picture != null)
        {
            car.Picture = await fileStorageService.EditFile(containerName, carCreationDto.Picture, car.Picture);
        }

        var aeroport = await _dbContext.Aeroports.FirstOrDefaultAsync(x => x.AeroportId == carCreationDto.AeroportId);
        var typeOfCar = await _dbContext.TypeOfCars.FirstOrDefaultAsync(x => x.TypeOfCarId == carCreationDto.TypeOfCarId);
        var gearbox = await _dbContext.Gearboxes.FirstOrDefaultAsync(x => x.Id == carCreationDto.GearboxId);
        var motor = await _dbContext.Motors.FirstOrDefaultAsync(x => x.Id == carCreationDto.MotorId);

        car.Aeroport = aeroport;
        car.TypeOfCar = typeOfCar;
        car.Motor = motor;
        car.Gearbox = gearbox;
        car.Id = Id;
        _dbContext.Entry(car).State = EntityState.Modified;

       // _dbContext.Cars.Update(car);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCar(int Id, IFileStorageService fileStorageService)
    {
        var car = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == Id);

        var resExist = await _dbContext.Reservations.Where(x => x.Car.Id == Id).ToListAsync();
        bool valide = true;
        if (resExist.Count > 0)
        {
            for (int i = 0; i < resExist.Count; i++)
            {
                if (resExist[i].End_Date > DateTime.Now)
                {
                    valide = false;
                    break;
                }
            }
        }
        if (valide)
        {
            var x = 1;
            _dbContext.Remove(car);
            await _dbContext.SaveChangesAsync();
            await fileStorageService.DeleteFile(car.Picture, containerName);
        } else
        {
            throw new Exception($"There is/are {resExist.Count} reservation(s) for this car. Before delete this item You have to delete/cancel the/all reservation(s) !");
        }
    }
    
    public async Task<List<CarDto>> GetCarByAeroport(int Id)
    {
        var cars = await _dbContext.Cars.Include(i => i.TypeOfCar).Include(i => i.Gearbox).Include(i => i.Motor)
            .Where(x => x.Aeroport.AeroportId == Id).ToListAsync();
        return _mapper.Map<List<CarDto>>(cars);
    }

    public async  Task<List<ReservationIndexDto>> GetCarsForRes(int Id){
        var cars = await _dbContext.Cars.Include(x => x.Aeroport.Prices)
            .Where(x => x.Aeroport.AeroportId == Id).ToListAsync();

        return _mapper.Map<List<ReservationIndexDto>>(cars);
        
    }

    public async Task<ReservationDetailsDto> GetCarAllInfoById(int Id)
    {
        _carId = Id;
        var car = await _dbContext.Cars
            .Include(x => x.TypeOfCar).Include(x => x.Motor)
            .Include(x => x.Aeroport.Prices)
            .Include(x => x.Gearbox).FirstOrDefaultAsync(x => x.Id == Id);

        return _mapper.Map<ReservationDetailsDto>(car);
    }

    public async Task<List<ResDates>> GetCarsResDates()
    {
        var resDates = await (from d in _dbContext.Reservations 
                 where d.Car.Id == _carId && d.Start_Date >= DateTime.Now
                 select d).ToListAsync();

        return _mapper.Map<List<ResDates>>(resDates);
    }

    public async Task<List<ReservationForAdminUI>> GetReservationAdminUI(int Id) //Id Car
    {
        var res = await _dbContext.Reservations.Where(x => x.Car.Id == Id).Include(x => x.Client).Include(x => x.Bill).ToListAsync();
        List<ReservationForAdminUI> lstRes = new();
        for (int i = 0; i < res.Count; i++)
        {
            ReservationForAdminUI ui = new();
            ui.ReservationId = res[i].Id;
            ui.UserId = res[i].Client.UserId;
            ui.FirstName = res[i].Client.FirstName;
            ui.LastName = res[i].Client.LastName;
            ui.Start_Date = res[i].Start_Date;
            ui.End_Date = res[i].End_Date;
            ui.Solde = res[i].Bill.Solde;
            lstRes.Add(ui);
        }
        return lstRes;

    }

    //----------------Motor---------------------------
    public async Task<List<MotorDto>> GetMotors()
    {
        var motors = await _dbContext.Motors.ToListAsync();
        return _mapper.Map<List<MotorDto>>(motors);
    }
    public async Task PostMotor(MotorCreationDto motorCreationDto)
    {
        var motor = _mapper.Map<Motor>(motorCreationDto);
        _dbContext.Motors.Add(motor);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<MotorDto> GetMotorsById(int Id)
    {
        var motors = await _dbContext.Motors.FirstOrDefaultAsync(x => x.Id == Id);
        return _mapper.Map<MotorDto>(motors);
    }
    public async Task EditMotor(int Id, MotorCreationDto motorDto)
    {
        var motor = _mapper.Map<Motor>(motorDto);
        motor.Id = Id;
        _dbContext.Entry(motor).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMotor(int Id)
    {
        var cars = await _dbContext.Cars.Where(x => x.Motor.Id == Id).ToListAsync();

        if(cars.Count == 0)
        {
            var motor = await _dbContext.Motors.FirstOrDefaultAsync(x =>x.Id == Id);
            _dbContext.Motors.Remove(motor);
            await _dbContext.SaveChangesAsync();
        } else
            throw new Exception($"There are {cars.Count} car(s). Before delete this item You have to delete the car(s) !");
    }

    //----------------Gearbox---------------------------
    public async Task<List<GearboxDto>> GetGearboxes()
    {
        var gearboxes = await _dbContext.Gearboxes.ToListAsync();
        return _mapper.Map<List<GearboxDto>>(gearboxes);
    }

    public async Task<List<CarDto>> GetCarsByGearbox(int Id)
    {
        var cars = await _dbContext.Cars.Where(x => x.Gearbox.Id == Id).ToListAsync();
        return _mapper.Map<List<CarDto>>(cars);
    }
    public async Task PostGearbox(GearboxCreationDto gearboxCreationDto)
    {
        var gearbox = _mapper.Map<Gearbox>(gearboxCreationDto);
        _dbContext.Gearboxes.Add(gearbox);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<GearboxDto> GetGearboxById(int Id)
    {
        var gearbox = await _dbContext.Gearboxes.FirstOrDefaultAsync(x => x.Id == Id);
        return _mapper.Map<GearboxDto>(gearbox);
    }
    public async Task EditGearbox(int id, GearboxCreationDto gearboxDto)
    {
        var gearbox = _mapper.Map<Gearbox>(gearboxDto);
        gearbox.Id = id;
        // _dbContext.Gearboxes.Update(gearbox);
        _dbContext.Entry(gearbox).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    //----------------TypeOfCarDto---------------------------
    public async Task<List<TypeOfCarDto>> GetTypesOfCars()
    {
        var nots = await _dbContext.TypeOfCars.ToListAsync();
        return _mapper.Map<List<TypeOfCarDto>>(nots);
    }
}
