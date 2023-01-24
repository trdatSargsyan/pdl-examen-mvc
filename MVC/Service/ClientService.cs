using MVC.Controllers;
using MVC.Interface;
using MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MVC.Service;

public class ClientService : IClient
{
    private string _urlClient;
    private readonly HttpClient _httpClient;
    private string _userId = HomeController.user_Id;
    private string access_token = HomeController.access_Token;
    public ClientService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _urlClient = config["urlClient:Client"];
        //_urlCar = config["urlCar:Car"];
    }
    public void ThrowException(HttpResponseMessage httpResponse)
    {
        var err = JsonConvert.DeserializeObject<dynamic>(httpResponse.Content.ReadAsStringAsync().Result);
        throw new Exception($"{err.message} ,  {err.statusCode}");
    }
    public async Task<List<ReservationUI>> GetReservationsForUserUI()
    {
        string route = "GetReservationsForUserUI/";
        //OK
        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlClient}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<ReservationUI>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<HttpResponseMessage> PostClient(ClientCreationDto clientCreationDto)
    {
        clientCreationDto.Role = "User_LocationDeVoiture"; //role no right
        clientCreationDto.UserId = _userId;
        var content = JsonConvert.SerializeObject(clientCreationDto);

        var res = _httpClient.PostAsJsonAsync($"{_urlClient}{"PostClient"}", clientCreationDto);
        await res;
        var task = res.Result;
        if (!res.IsCompletedSuccessfully)
        {
            throw new Exception($"Cannot retrive tasks {content} -url : {res.Result}");
        }
        return task;
    }


    public async Task CancelReservation(int Id)
    {
        var route = "CancelReservation/" + Id;
        var response = await _httpClient.DeleteAsync($"{_urlClient}{route}");
        if (!response.IsSuccessStatusCode)
            ThrowException(response);
    }

    public async Task<Client> GetUserByUserId(string userId)
    {
        string route = "GetUserByUserId/" + userId;
        var httpResponse = await _httpClient.GetAsync($"{_urlClient}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<Client>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<HttpResponseMessage> EditClient(Client clientDto)
    {
        string path = "EditClient/"+ _userId;
        var content = JsonConvert.SerializeObject(clientDto);
        var res = _httpClient.PutAsJsonAsync($"{_urlClient}{path}", clientDto);
        await res;
        if (!res.IsCompletedSuccessfully)
        {
            throw new Exception($"Cannot retrive tasks {content} -url : {res.Result}");
        }
        return res.Result;

    }

    public async Task<HttpResponseMessage> PostReservation(ReservationCreationDto reservationCreationDto)
    {
        var path = "PostResrvation/";
        var content = JsonConvert.SerializeObject(reservationCreationDto);
        var res = _httpClient.PostAsJsonAsync($"{_urlClient}{path}", reservationCreationDto);
        await res;
        if (!res.IsCompletedSuccessfully)
        {
            throw new Exception($"Cannot retrive tasks {content} -url : {res.Result}");
        }
       
        return res.Result;
    }

    public async Task<CreditCard> GetCreditCard(string id)
    {
        string route = "GetCreditCard/" + id;
        var httpResponse = await _httpClient.GetAsync($"{_urlClient}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<CreditCard>(content);
        if (task == null) return new CreditCard();
        return task;
    }
    public async Task<HttpResponseMessage> PostCreditCard(CreditCard creditCard)
    {
        var path = "PostCreditCard/";
        var content = JsonConvert.SerializeObject(creditCard);
        var res = _httpClient.PostAsJsonAsync($"{_urlClient}{path}", creditCard);
        await res;
        if (!res.IsCompletedSuccessfully)
        {
            throw new Exception($"Cannot retrive tasks {content} -url : {res.Result}");
        }

        return res.Result;
    }
    
    public bool creditCardExist(CreditCard db, CreditCard newCC)
    {
        if(db.FirstName == newCC.FirstName 
            && db.LastName == newCC.LastName
            && db.CardNumber == newCC.CardNumber
            && db.CardType == newCC.CardType
            && db.CVV == newCC.CVV
            && db.DateValid == newCC.DateValid) 
            return true;
        return false; 
    }
}

