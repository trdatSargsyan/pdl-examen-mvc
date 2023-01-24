using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.AppDbContext;
using WebApi.Entities;
using WebApi.Halper;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Service;

public class AdminService : IAdmin
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public AdminService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    //-------------<TypeOfCar>-------------
    public async Task<List<TypeOfCarDto>> GetTypesOfCars()
    {
        var nots = await _dbContext.TypeOfCars.ToListAsync();
        return _mapper.Map<List<TypeOfCarDto>>(nots);     
    }

    public async Task<List<CarDto>> GetCarsByType(int Id)
    {
        var cars = await _dbContext.Cars.Where(x => x.TypeOfCar.TypeOfCarId== Id).ToListAsync();

        return _mapper.Map<List<CarDto>>(cars);
    }

    public async Task PostTypeOfCar(TypeOfCarCreationDto notorieteCreationDto)
    {
        var not = _mapper.Map<TypeOfCar>(notorieteCreationDto);
        _dbContext.TypeOfCars.Add(not);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TypeOfCarDto> EditGetTypeOfCar(int Id)
    {
        var not = await _dbContext.TypeOfCars.FirstOrDefaultAsync(x => x.TypeOfCarId == Id);
        return _mapper.Map<TypeOfCarDto>(not);
    }

    public async Task EditTypeOfCar(int id,TypeOfCarDto notorieteDto)
    {
        var not = _mapper.Map<TypeOfCar>(notorieteDto);
        not.TypeOfCarId = id;
        _dbContext.Entry(not).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTypeOfCar(int Id)
    {
        var car = await _dbContext.Cars.Where(x => x.TypeOfCar.TypeOfCarId == Id).ToListAsync();
        if(car.Count == 0)
        {
            var typeOfCar = await _dbContext.TypeOfCars.FirstOrDefaultAsync(x => x.TypeOfCarId == Id);
            _dbContext.TypeOfCars.Remove(typeOfCar);
            await _dbContext.SaveChangesAsync();
        } else 
            throw new Exception($"There are {car.Count} car(s). Before delete this item You have to delete the car(s) !");
    }

    //---------------Price------------------------
    public async Task PostPrice(PriceCreationDto priceCreationDto)
    {
        var price = _mapper.Map<FormulePrice>(priceCreationDto);
        var aero = await _dbContext.Aeroports.FirstOrDefaultAsync(x => x.AeroportId == priceCreationDto.AeroportId);
        var car = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == priceCreationDto.CarId);
        price.Aeroport = aero;
        price.Car = car;
        _dbContext.FormulePrices.Add(price);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<PriceDto>> GetPrice(int Id) //Id Aeroport
    {
        var price = await _dbContext.FormulePrices.Where(x => x.Aeroport.AeroportId == Id).ToListAsync();

        if (price == null) price = new List<FormulePrice>();
        return _mapper.Map<List<PriceDto>>(price);
    }

    public async Task<PriceDto> EditGetPrice(int Id)
    {
        var price = await _dbContext.FormulePrices.FirstOrDefaultAsync(x => x.FormulePriceId == Id);
        return _mapper.Map<PriceDto>(price);
    }

    public async Task EditPrice(PriceDto priceDto)
    {
        var price = _mapper.Map<FormulePrice>(priceDto);
        _dbContext.FormulePrices.Update(price);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePrice(int Id)
    {
        var price = await _dbContext.FormulePrices.FirstOrDefaultAsync(x => x.FormulePriceId == Id);
        _dbContext.FormulePrices.Remove(price);
        await _dbContext.SaveChangesAsync();
    }


}
