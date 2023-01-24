using MVC.Controllers;
using MVC.Interface;
using MVC.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;

namespace MVC.Service;

public class CarService : ICar
{
    private string _urlCar;
    private string _urlAdmin;
    private readonly HttpClient _httpClient;
    private string access_token = HomeController.access_Token;
    public CarService(HttpClient httpClient, IConfiguration config)
	{
        _httpClient = httpClient;
        _urlAdmin = config["urlAdmin:Admin"];
        _urlCar = config["urlCar:Car"];
    }

    public void ThrowException(HttpResponseMessage httpResponse)
    {
        var err = JsonConvert.DeserializeObject<dynamic>(httpResponse.Content.ReadAsStringAsync().Result);
        throw new Exception($"{err.message} ,  {err.statusCode}");
    }

    //---------------------Typ of Car---------------------
    public async Task<List<CarDto>> GetCars()
    {
        var route = "GetCars";
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<CarDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }
    public async Task<List<CarDto>> GetCarsByIdCountry(int Id)
    {
        var route = "GetCarsByIdCountry/" + Id;
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
        ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<CarDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<List<CarDto>> GetCarsByType(int Id)
    {
        var route = $"GetCarsByType/{Id}";
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<CarDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<HttpResponseMessage> PostTypeOfCar(TypeOfCarCreationDto notorieteDto)
    {
        var path = "PostTypeOfCar/";
        var httpResponseMessage = await _httpClient.PostAsJsonAsync($"{_urlAdmin}{path}", notorieteDto);
        if (!httpResponseMessage.IsSuccessStatusCode)
            ThrowException(httpResponseMessage);
        return httpResponseMessage;
    }

    public async Task<List<TypeOfCarDto>> GetTypesOfCars()
    {
        var route = "GetTypesOfCars";
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<TypeOfCarDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<TypeOfCarDto> EditTypeOfCar(int Id)
    {
        var route = "EditTypeOfCar/" + Id;
        var httpResponse = await _httpClient.GetAsync($"{_urlAdmin}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<TypeOfCarDto>(content);
        if (task == null) throw new Exception();
        return task;
    }
    public async Task<HttpResponseMessage> EditTypeOfCar(TypeOfCarDto typeOfCar)
    {
        var path = "EditTypeOfCar/" + typeOfCar.TypeOfCarId;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var res = await _httpClient.PutAsJsonAsync($"{_urlAdmin}{path}", typeOfCar);
        if (!res.IsSuccessStatusCode)
            ThrowException(res);
        return res;
    }

    public async Task DeleteTypeOfCar(int Id)
    {
        string path = "DeleteTypeOfCat/" + Id;
        var httpRessponseMessage = await _httpClient.DeleteAsync($"{_urlAdmin}{path}");
        if (!httpRessponseMessage.IsSuccessStatusCode) 
            ThrowException(httpRessponseMessage);
    }


    //---------------------MOTOR---------------------
    public async Task<HttpResponseMessage> PostMotor(MotorCreationDto motorDto)
    {
        var path = "PostMotor/";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponseMessage = await _httpClient.PostAsJsonAsync($"{_urlCar}{path}", motorDto);
        if (!httpResponseMessage.IsSuccessStatusCode)
            ThrowException(httpResponseMessage);
        return httpResponseMessage;
    }

    public async Task<List<MotorDto>> GetMotors()
    {
        var route = "GetMotors";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponseMessage = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponseMessage.IsSuccessStatusCode)
            ThrowException(httpResponseMessage);
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<MotorDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<MotorDto> EditMotor(int Id)
    {
        var route = $"EditMotor/{Id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<MotorDto>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<HttpResponseMessage> EditMotor(int Id, MotorCreationDto motorDto)
    {
        var path = $"EditMotor/{Id}";
        var httpResponseMessage = await _httpClient.PutAsJsonAsync($"{_urlCar}{path}", motorDto);
        if (!httpResponseMessage.IsSuccessStatusCode)
            ThrowException(httpResponseMessage);
        return httpResponseMessage;
    }

    public async Task DeleteMotor(int Id)
    {
        string path = $"DeleteMotor/{Id}";
        var httpResponseMessage = await _httpClient.DeleteAsync($"{_urlCar}{path}");
        if (!httpResponseMessage.IsSuccessStatusCode)
            ThrowException(httpResponseMessage);
            
    }


    //---------------------Car---------------------
    public async Task<List<CarDto>> GetCarsByAeroportId(int Id)
    {
        var route = "GetCarsByAeroportId/" + Id;
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<CarDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<CarEditDto> GetCar(int Id)
    {
        var route = $"GetCar/{Id}";
        var response = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!response.IsSuccessStatusCode)
            ThrowException(response);
        var content = await response.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<CarEditDto>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<HttpResponseMessage> PostCar(CarCreationDto car)
    {
        if (car.Picture == null)
            throw new ArgumentNullException("Picture field is missing in car object");

        var apiUrl = "PostCar/";
        var picturePath = "C:\\Users\\Trdat\\Desktop\\photo\\" + car.Picture.FileName;
        var formData = new MultipartFormDataContent();
        foreach (var prop in car.GetType().GetProperties())
        {
            formData.Add(new StringContent(prop.GetValue(car)?.ToString()), name: prop.Name);
        }
        var pictureStream = new StreamContent(File.OpenRead(picturePath));
        pictureStream.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        formData.Add(pictureStream, name: "Picture", fileName: car.Picture.FileName);

        var response = await _httpClient.PostAsync($"{_urlCar}{apiUrl}", formData);

        if (!response.IsSuccessStatusCode)
            ThrowException(response);
        return response;
    }

    public async Task<HttpResponseMessage> EditCar(int Id, CarEditDto car)
    {
        var path = "EditCar/" + Id;
        //var multipartFormContent = new MultipartFormDataContent();
        //var filePath = "C:\\Users\\Trdat\\Desktop\\photo\\" + car.Picture.FileName;
        //var fileStreamContent = new StreamContent(File.OpenRead(filePath));
        //fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        //multipartFormContent.Add(fileStreamContent, name: "Picture", fileName: car.Picture.FileName);

        var response = _httpClient.PutAsJsonAsync($"{_urlCar}{path}", car);
        await response;
        if (!response.IsCompletedSuccessfully)
        {
            throw new Exception($"Cannot retrive tasks {car} -url : {response.Result}");
        }
        return response.Result;
    }

    public async Task DeleteCar(int Id)
    {
        string path = "DeleteCarById/" + Id;
        var httpResponseMessage = await _httpClient.DeleteAsync($"{_urlCar}{path}");
        if (httpResponseMessage.IsSuccessStatusCode)
            ThrowException(httpResponseMessage);
    }

    //---------------------Gearbox---------------------

    public async Task<List<CarDto>> GetCarsByMotor(int Id)
    {
        var route = $"GetCarsByMotor/{Id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<CarDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }
    public async Task<HttpResponseMessage> PostGearbox(GearboxCreationDto gearboxDto)
    {
        var path = "PostGearbox/";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var res = await _httpClient.PostAsJsonAsync($"{_urlCar}{path}", gearboxDto);
        if (!res.IsSuccessStatusCode)
            ThrowException(res);
        return res;
    }

    public async Task<List<GearboxDto>> GetGearboxes()
    {
        var route = "GetGearboxes";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<GearboxDto>>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<GearboxDto> EditGearbox(int Id)
    {
        var route = "EditGearbox/" + Id;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<GearboxDto>(content);
        if (task == null) throw new Exception();
        return task;
    }

    public async Task<HttpResponseMessage> EditGearbox(GearboxDto gearboxDto)
    {
        var path = "EditGearbox/";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var res =await _httpClient.PutAsJsonAsync($"{_urlCar}{path}", gearboxDto);
        if (!res.IsSuccessStatusCode)
            ThrowException(res);
        return res;
    }

    //DeleteGearBox

    //---------------------Reservation Dates By Car---------------------
    public async Task<List<ResDates>> GetCarsReservationDates()
    {
        string route = "GetCarsResDates/";
        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<ResDates>>(content);
        if (task == null) return new List<ResDates>();
        return task;
    }

    public async Task<string[]> GetCarsResDates()
    {
        var dates = await GetCarsReservationDates();
        List<string> allDates = new List<string>();
        for (int i = 0; i < dates.Count; i++)
        {
            DateTime startDate = dates[i].Start_Date;
            DateTime endDate = dates[i].End_Date;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                string s = date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                allDates.Add(s);
            }
        }
        var x = allDates.ToArray();
        return x;
    }

    public async Task<List<ReservationsForAdminUI>> ReservationsForAdminUI(int Id)
    {
        var route = "GetReservationByIdCar/" + Id;
        var httpResponse = await _httpClient.GetAsync($"{_urlCar}{route}");
        if (!httpResponse.IsSuccessStatusCode)
            ThrowException(httpResponse);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var task = JsonConvert.DeserializeObject<List<ReservationsForAdminUI>>(content);
        if (task == null) throw new Exception();
        return task;
    }
}
