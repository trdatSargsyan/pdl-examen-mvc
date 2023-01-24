using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Interface;

public interface IAdmin
{
    //Country
    Task<List<Country>> GetCountries();
    Task<List<Country>> GetCountriesForUI();
    Task<Country> GetCountryById(int id);
    Task<HttpResponseMessage> EditCountry(Country country);
    Task<HttpResponseMessage> PostCountry(CountryCreationDto countryDto);
    Task DeleteCountry(int Id);

    //Aeroport
    Task<List<AeroportDto>> GetAeroports(int Id);
    Task<List<AeroportDto>> GetAeroportsForUI(int Id);
    Task<HttpResponseMessage> PostAeroport(AeroportCreationDto aeropotDto);
    Task<AeroportDto> EditAeroport(int Id);
    Task<HttpResponseMessage> EditAeroport(AeroportDto aeroport);



    //Price
    Task<List<PriceDto>> GetPrices(int Id); //Id Aeroport
    Task<HttpResponseMessage> PostPrice(PriceCreationDto priceCreationDto);
    Task<PriceDto> EditPrice(int Id); //Id Price
    Task<HttpResponseMessage> EditPrice(PriceDto price);
    Task DeletePrice(int Id);

    //Client
    Task<List<Client>> GetClient();
    Task<List<ReservationUI>> GetReservationByClientId(string Id);
    Task<List<ReservationIndexDto>> GetCarIndexDto(int Id);
    Task<ReservationDetailsDto> GetCarDetailsByIdDto(int Id);

    Task AssignRoleToUser(string User_Id,string accessToken);

}
