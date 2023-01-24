using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Interface;

public interface ICountry
{
    Task<CountryDto> GetCountry(int Id);
    Task<List<CountryDto>> GetCountries();
    Task<List<CountryDto>> GetCountriesForUI();
    Task PostCountry(CountryCreationDto countryCreationDto);
    Task EditCountry(int id, CountryCreationDto countryDto);
    Task DeleteCountry(int id);

}
