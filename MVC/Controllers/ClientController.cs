 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Interface;
using MVC.Models;
using System.Globalization;

namespace MVC.Controllers;

public class ClientController : Controller
{
    private readonly IClient _client;
    private readonly IAdmin _admin;
    private string _userId = HomeController.user_Id;
    private static ReservationCreationDto dto = new();
    private static CreditCard _creditCard;
    public ClientController(IClient client, IAdmin admin)
    {
        _client = client;
        _admin = admin;
    }
    public async Task<IActionResult> Index()
    {
        var countries = await _admin.GetCountriesForUI();
        ReservationCreation rc = new();
        rc.Countries = countries; 
        return View(rc);
    }


    [HttpGet]
    public ActionResult PostClient()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> PostClient(ClientCreationDto clientCreationDto)
    {       
        await _client.PostClient(clientCreationDto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult<Client>> GetMyInfo(string userId)
    {
        var info = await _client.GetUserByUserId(userId);
        return View(info);
    }

    [HttpGet]
    public async Task<List<ReservationUI>> GetReservationsForUserUi()
    {
        return await _client.GetReservationsForUserUI();    
    }

    [HttpPost]
    public async Task CancelReservation(int Id)
    {
        await _client.CancelReservation(Id);
    }

    [HttpGet]
    public async Task<ActionResult> EditClient(string UserId)
    {
        var info = await _client.GetUserByUserId(UserId);
        return View(info);
    }

    [HttpPost]
    public async Task<ActionResult> EditClient(Client clientDto)
    {
        await _client.EditClient(clientDto);
        return RedirectToAction(nameof(GetMyInfo), new { userId= clientDto.UserId });
    }

    public async Task<JsonResult> DisplayAeroports(int IdCountry)
    {
        var aeroports = await _admin.GetAeroportsForUI(IdCountry);
        return Json(new SelectList(aeroports, "AeroportId", "Name"));
    }

    //GET Cars
    public async Task<List<ReservationIndexDto>> DisplayCars(int IdAeroport)
    {   
        var cars = await _admin.GetCarIndexDto(IdAeroport);
        return cars;
    }

    //GET Car
    [HttpGet]
    public async Task<IActionResult> GetCarForResById(int Id)
    {
        var car = await _admin.GetCarDetailsByIdDto(Id);
        return View(car);
    }


    [HttpPost]
    public  async Task PostReservationDetails(ReservationCreationDto resCreationDto)
    {
        dto.Total = resCreationDto.Total;
        dto.carId = resCreationDto.carId;
        dto.sDate = resCreationDto.sDate;
        dto.eDate = resCreationDto.eDate;
        dto.UserId = _userId;
    }

    //POST CreditCard
    [HttpGet]
    public async Task<IActionResult> PostCreditCard()
    {
        //GEt Credit card from DB 
        _creditCard = await _client.GetCreditCard(_userId);
        _creditCard.Amount =  dto.Total;
        _creditCard.CarId = dto.carId;
        return View(_creditCard);
    }

    [HttpPost]
    public async Task<IActionResult> PostCreditCard(CreditCard creditCard)
    {
        creditCard.UserId = _userId;
        var exist = _client.creditCardExist(_creditCard, creditCard);
        if (!exist)
        {
            await _client.PostCreditCard(creditCard);
        }
        await _client.PostReservation(dto);
        return RedirectToAction(nameof(Index));
    }
}
