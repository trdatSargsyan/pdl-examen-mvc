using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using MVC.Interface;
using MVC.Models;
using System.Dynamic;

namespace MVC.Controllers;

public class AdminController : Controller
{
    private static int _idCountry;
    private static int _idAeroport;

    private IAdmin _admin;
    private ICar _car;

    public AdminController(IAdmin admin, ICar car)
    {
        _admin = admin;
        _car = car;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        dynamic model = new ExpandoObject();
        model.Countries = await _admin.GetCountries(); 
        model.TypesOfCars = await _car.GetTypesOfCars();
        model.Motors = await _car.GetMotors();
        return View(model);
    }

    //---------------------Country---------------------
    public IActionResult PostCountry()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> PostCountry(CountryCreationDto country)
    {
        await _admin.PostCountry(country);
        return RedirectToAction(nameof(Index));
    }

    //EditCountry
    [HttpGet]
    public async Task<IActionResult> EditCountry(int Id)
    {
        var country = await _admin.GetCountryById(Id);
        return View(country);
    }

    [HttpPost]
    public async Task<IActionResult> EditCountry(Country country)
    {
        await _admin.EditCountry(country);
        return RedirectToAction(nameof(Index));
    }

    //DeleteCountry
    [HttpGet]
    public async Task<IActionResult> DeleteCountry(int Id)
    {
        await _admin.DeleteCountry(Id);
        return RedirectToAction(nameof(Index));
    }

    //---------------------Aeroport---------------------
    [HttpGet]
    public async Task<IActionResult> GetAeroports(int id)
    {
        _idCountry = id;
        var aeroports = await _admin.GetAeroports(id);
        return View(aeroports);
    }

    [HttpGet]
    public IActionResult PostAeroport()
    {
        AeroportCreationDto dto = new();
        dto.CountryId = _idCountry;
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> PostAeroport(AeroportCreationDto aeroportDto)
    {
        await _admin.PostAeroport(aeroportDto);
        return RedirectToAction(nameof(GetAeroports), new { id = _idCountry });
    }

    //EditAeroport
    [HttpGet]
    public async Task<IActionResult> EditAeroport(int Id)
    {
        var aeroport = await _admin.EditAeroport(Id);
        return View(aeroport);
    }

    [HttpPost]
    public async Task<ActionResult> EditAeroport(AeroportDto aeroport)
    {
        await _admin.EditAeroport(aeroport);
        return RedirectToAction(nameof(GetAeroports), new { id = _idCountry});
    }
    //DeleteAeroport

    //---------------------Price---------------------
    [HttpGet]
    public async Task<ActionResult> GetPrices(int Id)
    {
        _idAeroport = Id;
        var price = await _admin.GetPrices(Id);
        return View(price);
    }

    [HttpGet]
    public async Task<IActionResult> PostPrice()
    {
        PriceCreationDto dto = new();
        dto.Cars = await _car.GetCarsByAeroportId(_idAeroport);
        return View(dto);
    }

    [HttpPost]
    public async Task<ActionResult> PostPrice(PriceCreationDto pricePostDto)
    {
        pricePostDto.AeroportId = _idAeroport;
        await _admin.PostPrice(pricePostDto);
        return RedirectToAction(nameof(GetPrices), new {id = _idAeroport});
    }

    [HttpGet]
    public async Task<ActionResult> EditPrice(int Id)
    {
        var price = await _admin.EditPrice(Id);
        return View(price);
    }

    [HttpPost]
    public async Task<ActionResult> EditPrice(PriceDto priceDto)
    {
        await _admin.EditPrice(priceDto);
        return RedirectToAction(nameof(GetPrices), new { id = _idAeroport });
    }

    //[HttpDelete]
    public async Task<ActionResult> DeletePrice(int Id)
    {
        await _admin.DeletePrice(Id);
        return RedirectToAction(nameof(GetPrices), new { id = _idAeroport });
    }

    //---------------------Type Of Car---------------------
    [HttpGet]
    public async Task<ActionResult> GetCarsByType(int Id)
    {
        var cars = await _car.GetCarsByType(Id);
        return View(cars);
    }

    public IActionResult PostTypeOfCar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> PostTypeOfCar(TypeOfCarCreationDto notorieteDto)
    {
        await _car.PostTypeOfCar(notorieteDto);
        return RedirectToAction(nameof(Index));
    }

    //EditNotoriete
    [HttpGet]
    public async Task<ActionResult> EditTypeOfCar(int Id)
    {
        var not = await _car.EditTypeOfCar(Id);
        return View(not);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditTypeOfCar(TypeOfCarDto notorieteDto)
    {
        await _car.EditTypeOfCar(notorieteDto);
        return RedirectToAction(nameof(Index));
    }

    //DeleteTypeOfCar
    [HttpGet]
    public async Task<ActionResult> DeleteTypeOfCar(int Id)
    {
        await _car.DeleteTypeOfCar(Id);
        return RedirectToAction(nameof(Index));
    }

    //---------------------Gearbox---------------------
    [HttpGet]
    public async Task<ActionResult> GetCarsByMotor(int Id)
    {
        var cars = await _car.GetCarsByMotor(Id);
        return View(cars);
    }


    public IActionResult PostMotor()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> PostMotor(MotorCreationDto motorCreationDTO)
    {
        await _car.PostMotor(motorCreationDTO);
        return RedirectToAction(nameof(Index));
    }

    //EditNotoriete
    [HttpGet]
    public async Task<ActionResult> EditMotor(int Id)
    {
        var gearbox = await _car.EditMotor(Id);
        return View(gearbox);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditMotor(int Id,MotorCreationDto motorCreationDTO)
    {
        await _car.EditMotor(Id ,motorCreationDTO);
        return RedirectToAction(nameof(Index));
    }

  
    [HttpGet]
    public async Task<ActionResult> DeleteMotor(int Id)
    {
        await _car.DeleteMotor(Id);
        return RedirectToAction(nameof(Index));
    }

    //---------------------Client---------------------
    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var listClients = await _admin.GetClient();
        return View(listClients);
    }
    [HttpGet]
    public async Task<IActionResult> GetReservtions(string Id) 
    {
        var res = await _admin.GetReservationByClientId(Id);
        return View(res);
    }
}
