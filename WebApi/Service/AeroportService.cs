using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.AppDbContext;
using WebApi.Entities;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Service;

public class AeroportService : IAeroport
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public AeroportService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<List<AeroportDto>> GetAeroports(int Id)
    {
        var aero = await _dbContext.Aeroports.Where(x => x.Country.Id == Id).ToListAsync();
        return _mapper.Map<List<AeroportDto>>(aero);
    }

    public async Task<List<AeroportDto>> GetAeroportForUI(int Id)
    {
        var aero = await _dbContext.Aeroports.Include(x => x.Cars).Where(x => x.Country.Id == Id).ToListAsync();

        List<AeroportDto> lstAero = new();
        for(int i = 0; i < aero.Count; i++)
        {
            if (aero[i].Cars.Count > 0)
            {
                var mapAero = _mapper.Map<AeroportDto>(aero[i]);
                lstAero.Add(mapAero);
            }
        }
        return _mapper.Map<List<AeroportDto>>(lstAero);
    }
    public async Task PostAeroport(AeroportCreationDto aeroportCreationDto)
    {
        var aeroport = _mapper.Map<Aeroport>(aeroportCreationDto);
        var country = _dbContext.Countries.FirstOrDefault(x => x.Id == aeroportCreationDto.CountryId);
        aeroport.Country = country;

        _dbContext.Aeroports.Add(aeroport);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<AeroportDto> EditAeroport(int Id)
    {
        var aeroport = await _dbContext.Aeroports.FirstOrDefaultAsync(x => x.AeroportId == Id);
        return _mapper.Map<AeroportDto>(aeroport);
    }

    public async Task EditAeroport( AeroportDto aeroportDto)
    {
        var aeroport = _mapper.Map<Aeroport>(aeroportDto);
        _dbContext.Aeroports.Update(aeroport);
        await _dbContext.SaveChangesAsync();
    }



}
