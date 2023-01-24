using MVC.Models;

namespace MVC.Interface;

public interface ICar
{
    //Car
    Task<List<CarDto>> GetCars();
    Task<List<CarDto>> GetCarsByIdCountry(int Id);
    Task<List<CarDto>> GetCarsByAeroportId(int Id);
    Task<CarEditDto> GetCar(int Id);
    Task<HttpResponseMessage> PostCar(CarCreationDto car);
    Task<HttpResponseMessage> EditCar(int Id, CarEditDto car);
    Task DeleteCar(int Id);

    //Type Of Car
    Task<List<CarDto>> GetCarsByType(int Id);
    Task<HttpResponseMessage> PostTypeOfCar(TypeOfCarCreationDto notorieteDto);
    Task<List<TypeOfCarDto>> GetTypesOfCars();
    Task<TypeOfCarDto> EditTypeOfCar(int Id);
    Task<HttpResponseMessage> EditTypeOfCar(TypeOfCarDto notorieteDto);
    Task DeleteTypeOfCar(int Id);

    //Motor
    Task<List<CarDto>> GetCarsByMotor(int Id);
    Task<HttpResponseMessage> PostMotor(MotorCreationDto motorDto);
    Task<List<MotorDto>> GetMotors();
    Task<MotorDto> EditMotor(int Id);
    Task<HttpResponseMessage> EditMotor(int Id,MotorCreationDto motorDto);
    Task DeleteMotor(int Id);
    //Gearbox

    Task<HttpResponseMessage> PostGearbox(GearboxCreationDto gearboxDto);
    Task<List<GearboxDto>> GetGearboxes();
    Task<GearboxDto> EditGearbox(int Id);
    Task<HttpResponseMessage> EditGearbox(GearboxDto gearboxDto);

    //RES
    Task<string[]> GetCarsResDates();
    Task<List<ReservationsForAdminUI>> ReservationsForAdminUI(int Id);
}
