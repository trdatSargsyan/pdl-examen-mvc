using WebApi.Entities;

namespace WebApi.Interface;

public interface IClient
{
    Task<List<ClientDto>> GetAllClients();
    Task<ClientDto> GetClientById(int Id);
    Task<ClientDto> GetClientByUserId(string userId);
    Task CancelReservation(int Id);
    Task<List<ReservationUI>> GetReservationUI(string userId = null);
    Task EditClient(ClientDto clientDto);
    Task PostCreateClient(ClientCreationDto clientCreationDto);
    Task<ClientViewModelDto> DisplayClientInfo(int Id);
    Task PostReservation(ReservationCreationDto reservationCreationDto);
    Task<CreditCardDto> GetCreditCardByIdUser(string Id);
    Task PostCreditCard(CreditCardCreationDto creditCardCreationDto);
}
