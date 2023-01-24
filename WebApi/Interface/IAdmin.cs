using WebApi.Entities;

namespace WebApi.Interface;

public interface IAdmin
{
    //-------------<TypeOfCar>-------------
    Task<List<TypeOfCarDto>> GetTypesOfCars();
    Task<List<CarDto>> GetCarsByType(int Id);
    Task PostTypeOfCar(TypeOfCarCreationDto notorieteCreationDto);
    Task<TypeOfCarDto> EditGetTypeOfCar(int Id);
    Task EditTypeOfCar(int id, TypeOfCarDto notorieteDto);
    Task DeleteTypeOfCar(int Id);

    //---------------Price------------------------
    Task PostPrice(PriceCreationDto priceCreationDto);
    Task<List<PriceDto>> GetPrice(int Id);
    Task<PriceDto> EditGetPrice(int Id);
    Task EditPrice(PriceDto priceDto);
    Task DeletePrice(int Id);
}
