using MVC.Controllers;
using MVC.Interface;
using MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MVC.Service;

public class AdminService : IAdmin
{
    private string _urlAdmin;
    private string _urlCar;
    private string _urlClient;
    private readonly HttpClient _httpClient;
    private string access_token = HomeController.access_Token;
    public AdminService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _urlAdmin = config["urlAdmin:Admin"];
        _urlCar = config["urlCar:Car"];
        _urlClient = config["urlClient:Client"];
    }
    public void ThrowException(HttpResponseMessage httpResponse)
    {
        var err = JsonConvert.DeserializeObject<dynamic>(httpResponse.Content.ReadAsStringAsync().Result);
        throw new Exception($"{err.message} ,  {err.statusCode}");
    }

    //---------------------Country---------------------

    public async Task<List<Country>> GetCountries()
    {
        var route = "GetCountries";

        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            ThrowException(httpResponse);
        }
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<Country>>(content);
        if (task == null) throw new Exception();
        return task;

    }

    public async Task<List<Country>> GetCountriesForUI()
    {
        var route = "GetCountriesForUI";
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);

        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<Country>>(content);
        if (task == null) throw new Exception();
        return task;
    }
    public async Task<Country> GetCountryById(int Id)
    {
        string route = $"GetCountry/{Id}";
        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<Country>(content);
        if (task == null) throw new Exception("Country is null !!!!");
        return task;
    }

    public async Task<HttpResponseMessage> PostCountry(CountryCreationDto countryDto)
    {
        var path = "PostCountry/";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var res = await _httpClient.PostAsJsonAsync($"{_urlAdmin}{path}", countryDto);
        if (!res.IsSuccessStatusCode)
            ThrowException(res);       
        return res;
    }
    public async Task<HttpResponseMessage> EditCountry(Country country)
    {
        string path = $"EditCountry/{country.Id}";
        var content = JsonConvert.SerializeObject(country);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var res = await _httpClient.PutAsJsonAsync($"{_urlAdmin}{path}", country);
        if (!res.IsSuccessStatusCode)        
            ThrowException(res);
        return res;
    }
    public async Task DeleteCountry(int Id)
    {
        var route = $"DeleteCountry/{Id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var response = await _httpClient.DeleteAsync($"{_urlAdmin}{route}");
        if (!response.IsSuccessStatusCode)
            ThrowException(response);
    }

    //---------------------Aeroport---------------------
    public async Task<List<AeroportDto>> GetAeroports(int Id)
    {
        var route = "GetAeroportByCountryId/" + Id;
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            ThrowException(httpResponse);
        }
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<AeroportDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }
    public async Task<List<AeroportDto>> GetAeroportsForUI(int Id)
    {
        var route = "GetAeroportForUI/" + Id;
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            ThrowException(httpResponse);
        }
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<AeroportDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }
    public async Task<HttpResponseMessage> PostAeroport(AeroportCreationDto aeroportDto)
    {
        var path = "PostAeroport/";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var response = await _httpClient.PostAsJsonAsync($"{_urlAdmin}{path}", aeroportDto);
        if (!response.IsSuccessStatusCode) 
            ThrowException(response);
        return response;
    }

    public async Task<AeroportDto> EditAeroport(int Id)
    {
        var route = $"EditAeroport/{Id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)       
            ThrowException(httpResponse);
        
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<AeroportDto>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<HttpResponseMessage> EditAeroport(AeroportDto aeroport)
    {
        var path = "EditAeroport/";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponseMessage = await _httpClient.PutAsJsonAsync($"{_urlAdmin}{path}", aeroport);
        if (!httpResponseMessage.IsSuccessStatusCode)
            ThrowException(httpResponseMessage);
        return httpResponseMessage;
    }


    //---------------------Pice---------------------
    public async Task<List<PriceDto>> GetPrices(int Id) //id Aeroport
    {
        var route = $"GetPriceByAeroportId/{Id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<PriceDto>>(content);
        if (task == null) 
            throw new Exception();
        return task;
    }

    public async Task<HttpResponseMessage> PostPrice(PriceCreationDto priceCreationDto)
    {
        var path = "PostFormulePrice/";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponseMessage = await _httpClient.PostAsJsonAsync($"{_urlAdmin}{path}", priceCreationDto);
        if (!httpResponseMessage.IsSuccessStatusCode)
            ThrowException(httpResponseMessage);
        return httpResponseMessage;
    }

    public async Task<PriceDto> EditPrice(int Id)
    {
        var route = $"EditPrice/{Id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<PriceDto>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<HttpResponseMessage> EditPrice(PriceDto priceDto)
    {
        var path = "EditPrice/";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponseMessage = await _httpClient.PutAsJsonAsync($"{_urlAdmin}{path}", priceDto);
        if (!httpResponseMessage.IsSuccessStatusCode)
            ThrowException(httpResponseMessage);
        return httpResponseMessage;
    }

    public async Task DeletePrice(int Id)
    {
        var route = $"DeletePrice/{Id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var response = await _httpClient.DeleteAsync($"{_urlAdmin}{route}");
        if (!response.IsSuccessStatusCode)
            ThrowException(response);
    }

    //---------------------Client---------------------
    public async Task<List<Client>> GetClient()
    {
        var route = "GetAllClients";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

        var httpResponse = await _httpClient.GetAsync($"{_urlClient}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<Client>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<List<ReservationUI>> GetReservationByClientId(string Id)
    {
        string route = $"GetReservationsForUserUI/{Id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlClient}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<ReservationUI>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<List<ReservationIndexDto>> GetCarIndexDto(int Id)
    {
        var route = $"GetCarsForRes/{Id}";
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<ReservationIndexDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<ReservationDetailsDto> GetCarDetailsByIdDto(int Id)
    {
        var route = $"GetCarAllInfoById/{Id}";
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<ReservationDetailsDto>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task AssignRoleToUser(string User_Id, string accessToken)
    {
        string path = "AssignRoleToUser";
        var res = await _httpClient.PostAsJsonAsync($"{_urlClient}{path}", new AssignRole { UserId = User_Id, AccessToken = accessToken});
        if (!res.IsSuccessStatusCode)
            ThrowException(res);
    }
}


