using WebApi.Entities;
using WebApi.Halper;

namespace WebApi.Interface;

public interface ICar
{
    Task<List<CarDto>> GetCars();
    Task<CarEditModel> GetCar(int Id);
    Task<List<CarDto>> GetCarsByIdCountry(int Id);
    Task<List<CarDto>> GetCarByAeroport(int Id);
    Task<List<CarDto>> GetCarsByMotor(int Id);
    Task<List<CarDto>> GetCarsByGearbox(int Id);

    Task PostCar(CarCreationDto carCreationDto, IFileStorageService fileStorageService);
    Task EditCar(int Id, CarCreationDto carCreationDto, IFileStorageService fileStorageService);
    Task DeleteCar(int Id, IFileStorageService fileStorageService);

    Task<List<ReservationIndexDto>> GetCarsForRes(int Id);
    Task<ReservationDetailsDto> GetCarAllInfoById(int Id);
    Task<List<ResDates>> GetCarsResDates();
    Task<List<ReservationForAdminUI>> GetReservationAdminUI(int Id);

    //----------------Motor---------------------------
    Task<List<MotorDto>> GetMotors();
    Task PostMotor(MotorCreationDto motorCreationDto);
    Task<MotorDto> GetMotorsById(int Id);
    Task EditMotor(int Id, MotorCreationDto motorCreationDto);
    Task DeleteMotor(int Id);

    //----------------Gearbox---------------------------
    Task<List<GearboxDto>> GetGearboxes();

    Task PostGearbox(GearboxCreationDto gearboxCreationDto);
    Task<GearboxDto> GetGearboxById(int Id);
    Task EditGearbox(int id, GearboxCreationDto gearboxDto);
    //----------------TypeOfCarDto---------------------------
    Task<List<TypeOfCarDto>> GetTypesOfCars();
}
