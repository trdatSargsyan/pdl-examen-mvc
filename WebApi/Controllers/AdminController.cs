using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Errors;
using WebApi.Interface;

namespace WebApi.Controllers;

public class AdminController : BaseApiController
{
    private readonly ICountry _country;
    private readonly IAdmin _admin;
    private readonly IAeroport _aeroport;
    public AdminController(IAdmin admin, ICountry country, IAeroport aeroport)
	{
        _country = country;
        _admin= admin;
        _aeroport=aeroport;
	}

 
    [HttpGet]
    [Route("GetCountry/{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryDto>> GetCountry(int Id)
    {
        var country = await _country.GetCountry(Id);
        if (country == null) return NotFound(new ApiResponse(404));
        return country;
    }

    [HttpGet]
    [Route("GetCountries")]
    public async Task<ActionResult<List<CountryDto>>> GetCountries()
    {
        return await _country.GetCountries();
    }

    [HttpGet]
    [Route("GetCountriesForUI")]
    public async Task<ActionResult<List<CountryDto>>> GetCountriesForUI()
    {
        return await _country.GetCountriesForUI();
    }

   // [Authorize(Policy = "Admin")] 
    [HttpPost]
    [Route("PostCountry")]
    [Produces("application/json")]
    public async Task<ActionResult> PostCountry([FromBody] CountryCreationDto countryCreationDto)
    {
        await _country.PostCountry(countryCreationDto);
        return NoContent();
    }
    
   // [Authorize(Policy = "Admin")] 
    [HttpPut]
    [Route("EditCountry/{id}")]
    public async Task<ActionResult> EditCountry(int id,[FromBody] CountryCreationDto countryDto)
    {
        await _country.EditCountry(id, countryDto);
        return NoContent();
    }

   // [Authorize(Policy = "Admin")]
    [HttpDelete]
    [Route("DeleteCountry/{Id}")]
    public async Task<ActionResult> DeleteCountry(int Id)
    {
        await _country.DeleteCountry(Id);
        return NoContent();
    }


    ///--------------------------------------
    // -----------------Aeroport-------------
    ///--------------------------------------
    
    [HttpGet]
    [Route("GetAeroportByCountryId/{Id}")]
    public async Task<ActionResult<List<AeroportDto>>> GetAeroport(int Id)
    {
        return  await _aeroport.GetAeroports(Id);
    }

    [HttpGet]
    [Route("GetAeroportForUI/{Id}")]
    public async Task<ActionResult<List<AeroportDto>>> GetAeroportForUI(int Id)
    {
        return await _aeroport.GetAeroportForUI(Id);
    }

    //[Authorize(Policy = "Admin")]
    [HttpPost]
    [Route("PostAeroport")]
    [Produces("application/json")]
    public async Task<ActionResult> PostAeroport([FromBody] AeroportCreationDto aeroportCreationDto)
    {
        await _aeroport.PostAeroport(aeroportCreationDto);
        return NoContent();
    }

    //[Authorize(Policy = "Admin")]
    [HttpGet]
    [Route("EditAeroport/{Id}")]
    public async Task<ActionResult<AeroportDto>> EditAeroport(int Id)
    {
        return await _aeroport.EditAeroport(Id);
    }
    
    //[Authorize(Policy = "Admin")]
    [HttpPut]
    [Route("EditAeroport")]
    public async Task<ActionResult> EditAeroport(AeroportDto aeroportDto)
    {
        await _aeroport.EditAeroport(aeroportDto);
        return NoContent();
    }

    //--------------------------------------
    // -----------------TypeOfCar-------------
    //--------------------------------------

    [HttpGet]
    [Route("GetTypesOfCars")]
    public async Task<ActionResult<List<TypeOfCarDto>>> GetTypesOfCars()
    {
        return await _admin.GetTypesOfCars();
    }


    [HttpGet]
    [Route("GetCarsByType/{Id}")]
    public async Task<ActionResult<List<CarDto>>> GetCarsByType(int Id)
    {
        return await _admin.GetCarsByType(Id);
    } 

    //[Authorize(Policy = "Admin")]
    [HttpPost]
    [Route("PostTypeOfCar")]
    [Produces("application/json")]
    public async Task<ActionResult> PostTypeOfCar([FromBody] TypeOfCarCreationDto notorieteCreationDto)
    {
        await _admin.PostTypeOfCar(notorieteCreationDto);
        return NoContent();
    }

    [HttpGet]
    [Route("EditTypeOfCar/{Id}")]
    public async Task<TypeOfCarDto> EditTypeOfCar(int Id)
    {
        return await _admin.EditGetTypeOfCar(Id);
    }

    [Authorize(Policy = "Admin")] //OK
    [HttpPut]
    [Route("EditTypeOfCar/{id}")]
    public async Task<ActionResult> EditTypeOfCar(int id,TypeOfCarDto notorieteDto)
    {
        await _admin.EditTypeOfCar(id,notorieteDto);
        return NoContent();
    }

    //[Authorize(Policy = "Admin")]
    [HttpDelete]
    [Route("DeleteTypeOfCat/{Id}")]
    public async Task<ActionResult> DeleteTypeOfCar(int Id)
    {
        await _admin.DeleteTypeOfCar(Id);
        return NoContent();
    }

    //--------------------------------------
    //-----------------FormulePrice---------
    //--------------------------------------
    /// <summary>
    /// Create a formulePrice
    /// </summary>
    /// <param name="priceCreationDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("PostFormulePrice")]
    [Produces("application/json")]
    public async Task<ActionResult> PostPrice([FromBody] PriceCreationDto priceCreationDto)
    {
        await _admin.PostPrice(priceCreationDto);
        return NoContent();
    }

    [HttpGet]
    [Route("GetPriceByAeroportId/{Id}")]
    public async Task<List<PriceDto>> GetPrice(int Id)
    {
        return await _admin.GetPrice(Id);
    }


    [HttpGet]
    [Route("EditPrice/{Id}")]
    public async Task<PriceDto> EditPrice(int Id)
    {
        return await _admin.EditGetPrice(Id);
    }

   // [Authorize(Policy = "Admin")]
    [HttpPut]
    [Route("EditPrice")]
    public async Task<ActionResult> EditPrice(PriceDto priceDto)
    {
        await _admin.EditPrice(priceDto);
        return NoContent();
    }

   // [Authorize(Policy = "Admin")]
    [HttpDelete]
    [Route("DeletePrice/{Id}")]
    public async Task<ActionResult> DeletePrice(int Id)
    {
        await _admin.DeletePrice(Id);
        return NoContent();
    }

}
