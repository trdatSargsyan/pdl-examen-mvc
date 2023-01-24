using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Halper;
using WebApi.Interface;

namespace WebApi.Controllers;

public class CarController : BaseApiController
{
    private readonly IFileStorageService _fileStorageService;
    private ICar _car;
    public CarController(ICar car, IFileStorageService fileStorageService)
    {
        _car = car;
        _fileStorageService = fileStorageService;
    }

    [HttpGet]
    [Route("GetCars")]
    public async Task<ActionResult<List<CarDto>>> GetCars()
    {
        return await _car.GetCars();
    }

    [HttpGet]
    [Route("GetCarsByIdCountry/{Id}")]
    public async Task<ActionResult<List<CarDto>>> GetCarsByIdCountry(int Id)
    {
        return await _car.GetCarsByIdCountry(Id);        
    }

    [HttpGet]
    [Route("GetCarsByMotor/{Id}")]
    public async Task<ActionResult<List<CarDto>>> GetCarsByMotor(int Id)
    {
        return await _car.GetCarsByMotor(Id);
    }

    [HttpGet]
    [Route("GetCar/{Id}")]
    public async Task<ActionResult<CarEditModel>>  GetCar(int Id)
    {
       return await _car.GetCar(Id);   
    }

    [HttpPost]
    [Route("PostCar")]
    //[Produces("application/json")]
    public async Task<ActionResult> PostCar([FromForm] CarCreationDto carCreationDto)
    {

        await _car.PostCar(carCreationDto, _fileStorageService);
        return NoContent();
    }

    [HttpPut]
    [Route("EditCar/{Id}")]
    public async Task<ActionResult> EditCar(int Id, [FromForm] CarCreationDto carCreationDto)
    {
        await _car.EditCar(Id, carCreationDto, _fileStorageService);
        return NoContent();
    }

    [HttpDelete]
    [Route("DeleteCarById/{Id}")]
    public async Task<ActionResult> DeleteCar(int Id)
    {
        await _car.DeleteCar(Id, _fileStorageService);
        return NoContent();
    }

    /// <summary>
    /// Get cars by AeroportId
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetCarsByAeroportId/{Id}")]
    public async Task<ActionResult<List<CarDto>>> GetCarsByAeroId(int Id)
    {
        return await _car.GetCarByAeroport(Id);
    }

    [HttpGet]
    [Route("GetCarsForRes/{Id}")]
    public async Task<ActionResult<List<ReservationIndexDto>>> GetCarsForRes(int Id)
    {
        return await _car.GetCarsForRes(Id);
    }

    [HttpGet]
    [Route("GetCarAllInfoById/{Id}")]
    public async Task<ActionResult<ReservationDetailsDto>> GetCarAllInfoByIf(int Id)
    {
        return await _car.GetCarAllInfoById(Id);       
    }

    [HttpGet]
    [Route("GetCarsResDates")]
    public async Task<List<ResDates>> GetCarsResDates()
    {
        return await _car.GetCarsResDates();
    }

    [HttpGet]
    [Route("GetReservationByIdCar/{Id}")]
    public async Task<List<ReservationForAdminUI>> GetReservationByIdCar(int Id)
    {
        return await _car.GetReservationAdminUI(Id);
    }

    //----------------Motor---------------------------
    [HttpGet]
    [Route("GetMotors")]
    public async Task<ActionResult<List<MotorDto>>> GetMotors()
    {
        return await _car.GetMotors();
    }

    [HttpGet]
    [Route("GetMotorsById/{Id}")]
    public async Task<ActionResult<MotorDto>> GetMotorById(int Id)
    {
        return await _car.GetMotorsById(Id);
    }

    [HttpPost]
    [Route("PostMotor")]
    public async Task<ActionResult> PostMotor([FromBody] MotorCreationDto motorCreationDto)
    {
        await _car.PostMotor(motorCreationDto);
        return NoContent();
    }

    [HttpGet]
    [Route("EditMotor/{Id}")]
    public async Task<MotorDto> EditMotor(int Id)
    {
        return await _car.GetMotorsById(Id);
    }

    [HttpPut]
    [Route("EditMotor/{Id}")]
    public async Task<ActionResult> EditMotor(int Id, MotorCreationDto motorDto)
    {
        await _car.EditMotor(Id,motorDto);
        return NoContent();
    }

    [HttpDelete]
    [Route("DeleteMotor/{Id}")]
    public async Task<ActionResult> DeleteMotor(int Id)
    {
        await _car.DeleteMotor(Id);
        return NoContent();
    } 

    //----------------Gearbox---------------------------
    [HttpGet]
    [Route("GetGearboxes")]
    public async Task<ActionResult<List<GearboxDto>>> GetGearboxes()
    {
        return await _car.GetGearboxes();
    }

    [HttpGet]
    [Route("GetGearboxById/{Id}")]
    public async Task<ActionResult<GearboxDto>> GetGearboxById(int Id)
    {
        return await _car.GetGearboxById(Id);
    }

    //Not Used in MVC
    [HttpGet]
    [Route("GetCarsByGearbox/{Id}")]
    public async Task<ActionResult<List<CarDto>>> GetCarsByGearbox(int Id)
    {
        return await _car.GetCarsByGearbox(Id);
    }

    [HttpPost]
    [Route("PostGearbox")]
    public async Task<ActionResult> PostGearbox([FromBody] GearboxCreationDto gearboxCreationDto)
    {
        await _car.PostGearbox(gearboxCreationDto);
        return NoContent();
    }

    [HttpGet]
    [Route("EditGearbox/{Id}")]
    public async Task<GearboxDto> EditGearbox(int Id)
    {
        return await _car.GetGearboxById(Id);
    }

    [HttpPut]
    [Route("EditGearbox/{id}")]
    public async Task<ActionResult> EditGearbox(int id, GearboxCreationDto gearboxDto)
    {
        await _car.EditGearbox(id,gearboxDto);
        return NoContent();
    }
}
