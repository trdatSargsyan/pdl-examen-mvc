using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.AppDbContext;
using WebApi.Entities;
using WebApi.Halper;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Service;

public class CountryService : ICountry
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CountryService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<CountryDto>> GetCountries()
    {
        var countries = await _dbContext.Countries.ToListAsync();
        return _mapper.Map<List<CountryDto>>(countries);
    }
    public async Task<List<CountryDto>> GetCountriesForUI()
    {
        var countries = await _dbContext.Countries.Include(x => x.Aeroports).ThenInclude(x => x.Cars).ToListAsync();
       
        List<CountryDto> lstDto = new();

        for(int i = 0; i < countries.Count; i++)
        {
            if (countries[i].Aeroports.Count > 0)
            {
                var x = countries[i].Aeroports.Select(x => x.Cars).ToList();
                if (x[0].Count > 0)
                {
                    var mapCountry = _mapper.Map<CountryDto>(countries[i]);
                    lstDto.Add(mapCountry);
                }
            }
        }

        return _mapper.Map<List<CountryDto>>(lstDto);
    }

    public async Task<CountryDto> GetCountry(int Id)
    {
        var country = await _dbContext.Countries.Include(x => x.Aeroports).FirstOrDefaultAsync(x => x.Id == Id);
        return _mapper.Map<CountryDto>(country);
    }

    public async Task PostCountry(CountryCreationDto countryCreationDto)
    {
        var country = _mapper.Map<Country>(countryCreationDto);
        _dbContext.Countries.Add(country);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditCountry(int id, CountryCreationDto countryDto)
    {
        var country = _mapper.Map<Country>(countryDto);
        country.Id = id;
        _dbContext.Entry(country).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCountry(int Id)
    {
        var aero = await _dbContext.Aeroports.Where(x => x.Country.Id == Id).ToListAsync();

        if (aero.Count == 0)
        {
            var country = await _dbContext.Countries.FirstOrDefaultAsync(x => x.Id == Id);
            _dbContext.Countries.Remove(country);
            await _dbContext.SaveChangesAsync();
        }
        else
            throw new Exception($"There are {aero.Count} aeroport(s) in this country. Before delete this item You have to delete the country(ies) !");

    }
}
