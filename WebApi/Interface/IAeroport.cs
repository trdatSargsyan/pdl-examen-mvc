using WebApi.Entities;

namespace WebApi.Interface;

public interface IAeroport
{
    Task<List<AeroportDto>> GetAeroports(int Id);
    Task<List<AeroportDto>> GetAeroportForUI(int Id);
    Task EditAeroport(AeroportDto aeroportDto);
    Task<AeroportDto> EditAeroport(int Id);
    Task PostAeroport(AeroportCreationDto aeroportCreationDto);
}
