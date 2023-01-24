using Microsoft.AspNetCore.Mvc;
using MVC.Interface;
using MVC.Models;

namespace MVC.Controllers;

public class CarController : Controller
{
    private IAdmin _admin;
    private ICar _car;
    private static int _aeroportId;
    public CarController(IAdmin admin, ICar car)
    {
        _admin = admin;
        _car = car; 
    }

    [HttpGet]
    public async Task<List<CarDto>> GetAllCars()
    {
        return await _car.GetCars();   
    }

    [HttpGet]
    public async Task<List<CarDto>> GetCarsByCountry(int Id)
    {
        return await _car.GetCarsByIdCountry(Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetCars(int Id) //Aeroport Id
    {
        _aeroportId = Id;
        var cars = await _car.GetCarsByAeroportId(Id);
        return View(cars);
    }

    [HttpGet]
    public async Task<IActionResult> PostCar()
    {
        var typeOfCars = await _car.GetTypesOfCars();
        var motors = await _car.GetMotors();
        var gearbox = await _car.GetGearboxes();
        CarCreationDto car = new();
        car.AeroportId = _aeroportId;
        car.TypeOfCarDto = typeOfCars;
        car.MotorDtos = motors;
        car.GearboxDto = gearbox;
        car.ProductionDate = DateTime.Today;
        return View(car);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> PostCar(CarCreationDto car)
    {
        car.AeroportId = _aeroportId;
        await _car.PostCar(car);
        return RedirectToAction(nameof(GetCars),new {Id = _aeroportId});
    }

    [HttpGet]
    public async Task<IActionResult> EditCar(int Id)
    {
        var car = await _car.GetCar(Id); 
        return View(car);
    }

    [HttpPost]
    public async Task<ActionResult> EditCar(int Id, CarEditDto carCreationDto)
    {
        carCreationDto.AeroportId = _aeroportId;
        await _car.EditCar(Id, carCreationDto);
        return RedirectToAction(nameof(GetCars), new { Id = _aeroportId });
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCar(int Id)
    {
        await _car.DeleteCar(Id);
        return RedirectToAction(nameof(GetCars), new { Id = _aeroportId });
    }

    [HttpGet]
    public async Task<string[]> GetCarsResDates()
    {
        var dates = await _car.GetCarsResDates();
        return dates;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations(int Id)
    {
        var res = await _car.ReservationsForAdminUI(Id);
        return View(res);
    }
}
