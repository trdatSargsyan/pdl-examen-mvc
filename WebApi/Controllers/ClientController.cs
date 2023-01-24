using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebApi.AppDbContext;
using WebApi.Entities;
using WebApi.Interface;

namespace WebApi.Controllers;

public class ClientController : BaseApiController
{
	private  IClient _client;
    private string _domain;
    public ClientController(IClient client, IConfiguration configuration)
	{
        _client =  client;
        _domain = configuration["Auth0:Domain"];
	}

    /// <summary>
    /// Get All Clients 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAllClients")]
    public async Task<ActionResult<List<ClientDto>>> GetAllClients()
    {
        //A modifier
        return await _client.GetAllClients();
    }

    /// <summary>
    /// Get Client By Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
	[HttpGet]
    [Route("GetClient/{Id}")]
    public async Task<ActionResult<ClientDto>> GetClient(int Id)
	{
        return await _client.GetClientById(Id);   
	}

    [HttpGet]
    [Route("GetUserByUserId/{userId}")]
    public async Task<ActionResult<ClientDto>> GetUserByUserId(string userId)
    {
        return await _client.GetClientByUserId(userId);
    }

    //[Authorize(Policy = "Client")] //OK
    [HttpGet]
    [Route("GetReservationsForUserUI/{Id?}")]
    public async Task<List<ReservationUI>> GetReservationsForUserUI(string Id = null )
    {
        return await _client.GetReservationUI(Id);
    }

    [HttpDelete]
    [Route("CancelReservation/{Id}")]
    public async Task CancelReservation(int Id)
    {
        await _client.CancelReservation(Id);
    }

    /// <summary>
    /// Create a client
    /// </summary>
    /// <param name="clientCreationDto"></param>
    /// <returns></returns>
	[HttpPost]
    [Route("PostClient")]
    [Produces("application/json")]
    public async Task<ActionResult> PostClient([FromBody] ClientCreationDto clientCreationDto)
	{
        //A Modifier
        await _client.PostCreateClient(clientCreationDto);
        return NoContent();
	}

    /// <summary>
    /// Display All info by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("DisplayClientAllInfoById")]
    public async Task<ActionResult<ClientViewModelDto>> DisplayClientAllInfoById(int id)
    {
        //A modifier
        return await _client.DisplayClientInfo(id);
    }

    /// <summary>
    /// Create a reservation
    /// </summary>
    /// <param name="reservationCreationDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("PostResrvation")]
    public async Task<ActionResult> CreateReservation(ReservationCreationDto reservationCreationDto)
    {
        await _client.PostReservation(reservationCreationDto);
        return NoContent();
    }

    [HttpPut]
    [Route("EditClient/{userId}")]
    public async Task<ActionResult> EditClient(string userId, [FromBody] ClientDto clientDto)
    {
        await _client.EditClient( clientDto);
        return NoContent();
    }

    //OK
    [HttpPost]
    [Route("AssignRoleToUser")]
    public void AssignRoleToUser(ClientAuthDto clientAuthDto)
    {
        var roleId = "rol_qwpQCuZY5k0QV0rI"; //User_LocationDeVoiture for all new users
        var userBody = "{ \"users\": [ \"" + clientAuthDto.UserId + "\" ] }";
        var resource = $"https://{_domain}/api/v2/roles/{roleId}/users";
        var client = new RestClient(resource);
        var request = new RestRequest(resource, Method.Post);
        request.AddHeader("content-type", "application/json");
        request.AddHeader("authorization", $"Bearer {clientAuthDto.AccessToken}");
        request.AddHeader("cache-control", "no-cache");
        request.AddParameter("application/json",userBody , ParameterType.RequestBody);
        client.Execute(request);

    }

    //CreditCard
    [HttpGet]
    [Route("GetCreditCard/{Id}")]
    public async Task<ActionResult<CreditCardDto>> GetCreditCard(string Id)
    {
        return await _client.GetCreditCardByIdUser(Id);        
    }

    [HttpPost]
    [Route("PostCreditCard")]
    public async Task<ActionResult> PostCreditCard(CreditCardCreationDto creationDto)
    {
        await _client.PostCreditCard(creationDto);
        return NoContent();
    }

}


