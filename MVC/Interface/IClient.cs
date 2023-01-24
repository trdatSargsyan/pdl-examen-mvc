using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Interface;

public interface IClient
{
    Task<List<ReservationUI>> GetReservationsForUserUI();
    Task CancelReservation(int Id);
    Task<HttpResponseMessage> PostClient(ClientCreationDto clientCreationDto);
    Task<Client> GetUserByUserId(string userId);
    Task<HttpResponseMessage> EditClient(Client clientDto);
    Task<HttpResponseMessage> PostReservation(ReservationCreationDto reservationCreationDto);

    //CreditCard
    Task<CreditCard> GetCreditCard(string id);//User AuthId
    Task<HttpResponseMessage> PostCreditCard(CreditCard creditCard);

    bool creditCardExist(CreditCard db, CreditCard newCC);
}
