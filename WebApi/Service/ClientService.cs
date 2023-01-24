using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.AppDbContext;
using WebApi.Entities;
using WebApi.Halper;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Service;

public class ClientService : IClient
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private static string _userId;
    public ClientService(IMapper mapper, ApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<List<ClientDto>> GetAllClients()
    {
        var clients = await _dbContext.Clients.ToListAsync();
        if (clients.Count == 0) 
            clients = new List<Client>();
        return _mapper.Map<List<ClientDto>>(clients);
    }
    //user Database Id
    public async Task<ClientDto> GetClientById(int Id)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == Id);
        return _mapper.Map<ClientDto>(client);
       
    }
    //user Auth0 Id
    public async Task<ClientDto> GetClientByUserId(string userId)
    {
        _userId = userId;
        var client = await _dbContext.Clients.Include(x => x.Reservations).FirstOrDefaultAsync(x => x.UserId == userId);
        return _mapper.Map<ClientDto>(client);
    }
    
    public async Task CancelReservation(int Id)
    {
        var res = await _dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == Id);
        _dbContext.Reservations.Remove(res);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ReservationUI>> GetReservationUI(string userId = null) //empty
    {
        int clientId;
        if (userId == null)
        {
            clientId = await _dbContext.Clients.Where(x => x.UserId == _userId).Select(x => x.Id).FirstOrDefaultAsync();
        } else
        {
            clientId = await _dbContext.Clients.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefaultAsync();
        }

        //clientId = await _dbContext.Clients.Where(x => x.UserId == _userId).Select(x => x.Id).FirstOrDefaultAsync();
        var res = await _dbContext.Reservations.Include(x => x.Bill).Include(x => x.Car).Where(x => x.Client.Id == clientId).ToListAsync();
        List<ReservationUI> lstUI = new();
       
        for(int i = 0; i < res.Count; i++)
        {
            ReservationUI ui = new();
            ui.ReservationId = res[i].Id;
            ui.UserId = _userId; //don't need
            ui.Start_Date = res[i].Start_Date;
            ui.End_Date = res[i].End_Date;
            ui.CarId = res[i].Car.Id;
            ui.Brand = res[i].Car.Brand;
            ui.Model = res[i].Car.Model;
            ui.Solde = res[i].Bill.Solde;
            lstUI.Add(ui);
        }

        return lstUI;
    }

    public async Task EditClient(ClientDto clientDto)
    {
        
        var client = _mapper.Map<Client>(clientDto);
        _dbContext.Clients.Update(client);
        await _dbContext.SaveChangesAsync();
    }
    public async Task PostCreateClient(ClientCreationDto clientCreationDto)
    {
        var client = _mapper.Map<Client>(clientCreationDto);
        _dbContext.Clients.Add(client);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<ClientViewModelDto> DisplayClientInfo(int Id)
    {
        var res = await _dbContext.Reservations.
           // Include(x => x.Bill).
            Include(y => y.Client).
            Where(x => x.Client.Id == Id).
            FirstOrDefaultAsync();
        if(res == null)
        {
            var data = GetClientById(Id);
            ClientViewModelDto cv = new ClientViewModelDto();
            //cv.reservationViewModelDtos = null;
            cv.FirstName = data.Result.FirstName;
            cv.LastName = data.Result.LastName;
            cv.Phone = data.Result.Phone;
            cv.Email = data.Result.Email;
            return cv;
        }
        return _mapper.Map<ClientViewModelDto>(res);
    }
    public async Task PostReservation(ReservationCreationDto reservationCreationDto)
    {
        //var res = _mapper.Map<Reservation>(reservationCreationDto); does not work
        Reservation newRes = new();
        var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.UserId == reservationCreationDto.UserId);
        var car = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == reservationCreationDto.carId);
        newRes.Start_Date = reservationCreationDto.sDate;
        newRes.End_Date = reservationCreationDto.eDate;
        newRes.Start_Km = Convert.ToInt32(car.Km); //:( f..k
        newRes.Car = car;
        newRes.Client = client;
        _dbContext.Reservations.Add(newRes);
        await _dbContext.SaveChangesAsync();

        //Bill
        Bill bill = new();
        bill.DistanceTraveled = 0;
        bill.Solde = reservationCreationDto.Total;
        bill.Reservation = newRes;
        _dbContext.Bill.Add(bill);
        await _dbContext.SaveChangesAsync();
    }
    //CreditCard
    //GetCreditCard
    public async Task<CreditCardDto> GetCreditCardByIdUser(string Id)
    {
        var cc = await _dbContext.CreditCards.FirstOrDefaultAsync(x => x.Client.UserId == Id);
        if(cc == null) return new CreditCardDto();
        var arrayCardType = Enum.GetValues(typeof(CardType)).Cast<CardType>().Select(z => z.ToString()).ToList();
        var card = _mapper.Map<CreditCardDto>(cc);
        for (int i = 0; i < arrayCardType.Count; i++)
        {
            if (arrayCardType[i] == card.CardTypeName)
            {
                card.CardType = i; 
                break;
            }
        }
        return card;
    }
    public async Task PostCreditCard(CreditCardCreationDto creditCardCreationDto)
    {
        var lstCreditType = Enum.GetValues(typeof(CardType)).Cast<CardType>().Select(z => z.ToString()).ToList();
        var creditCardType = lstCreditType[creditCardCreationDto.CardType];
        //credit card double
        var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.UserId == creditCardCreationDto.UserId);

        creditCardCreationDto.CreditTypeName = creditCardType;


        var creditCatd = _mapper.Map<CreditCard>(creditCardCreationDto);
        creditCatd.Client = client;
        _dbContext.CreditCards.Add(creditCatd);
        await _dbContext.SaveChangesAsync();
    }


}
