using AutoMapper;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Halper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //Client
        CreateMap<ClientDto, Client>().ReverseMap();
        CreateMap<ClientCreationDto, Client>().
            ForMember(a => a.Reservations, y => y.Ignore()).
            ReverseMap();

        //------------------------------------------------------------------
        //CAR
        //Gearbox POST
        CreateMap<GearboxCreationDto, Models.Gearbox>();
        //Gearbox GET
        CreateMap<Models.Gearbox, GearboxDto>();
        //Motor POST
        CreateMap<MotorCreationDto, Motor>();
        //Motor GET
        CreateMap<Motor, MotorDto>();

        //GetCar
        CreateMap<Car, CarEditModel>().
            ForSourceMember(x => x.Aeroport, o => o.DoNotValidate()).
            ForSourceMember(x => x.Reservations, o => o.DoNotValidate()).
            ForMember(x => x.MotorId, i => i.MapFrom(y => y.Motor.Id)).
            ForMember(x => x.GearboxId, i => i.MapFrom(y => y.Gearbox.Id)).
            ForMember(x => x.TypeOfCarId, i => i.MapFrom(y => y.TypeOfCar.TypeOfCarId));

        //PostCreationCar
        CreateMap<CarCreationDto, Car>()
            .ForMember(x => x.Reservations, i => i.Ignore())
            .ForMember(x => x.Aeroport, i => i.Ignore())
            .ForMember(x => x.TypeOfCar, i => i.Ignore())
            .ForMember(x => x.Gearbox, i => i.Ignore())
            .ForMember(x => x.Motor, i => i.Ignore());
        
        //GetCar
       // CreateMap<Car,CarCreationDto>()
            
        //GetCarByAeroport(Id)
        CreateMap<Car, CarDto>().  
            ForSourceMember(x => x.Aeroport, o => o.DoNotValidate()).         
            ForSourceMember(x => x.Reservations, o => o.DoNotValidate()).
            ForMember(x => x.Prices, i => i.MapFrom(y => 
                new PricesDto
                {
                    CarId = y.Price.CarId,
                    PriceDay = y.Price.PriceDay,
                    PriceWeek = y.Price.PriceWeek
                })).
            ForMember(x => x.TypeOfCarDto, i => i.MapFrom(y => 
                new TypeOfCarDto
                {
                    TypeOfCarId =  y.TypeOfCar.TypeOfCarId,
                    Type = y.TypeOfCar.Type
                }))
            .ForMember(x => x.MotorDto, i => i.MapFrom(y => 
                new MotorDto {
                    Id = y.Motor.Id, Type = y.Motor.Type
                }))
           .ForMember(x => x.GearboxDto, i => i.MapFrom(y =>
                new GearboxDto
                {
                    Id = y.Gearbox.Id,
                    Type = y.Gearbox.Type
                }))
           .ForMember(x => x.AeroportDto, i => i.MapFrom(y =>           
               new AeroportDto
               {
                   AeroportId = y.Aeroport.AeroportId,
                   Address = y.Aeroport.Address,
                   Email = y.Aeroport.Email,
                   Name = y.Aeroport.Name,
                   Phone = y.Aeroport.Phone
               }));


        //Cars display on Client Index page : GetCarsRes(int Id)
        CreateMap<Car, ReservationIndexDto>()
            .ForSourceMember(x => x.Reservations, i => i.DoNotValidate())
            .ForSourceMember(x => x.TypeOfCar, i => i.DoNotValidate())
            .ForSourceMember(x => x.Motor, i => i.DoNotValidate())
            .ForSourceMember(x => x.Gearbox, i => i.DoNotValidate())
            .ForMember(x => x.PriceDto, i => i.MapFrom(o => o.Price));
        //GetCarAllInfoById 
        CreateMap<Car, ReservationDetailsDto>()      
            .ForMember(x => x.TypeOfCarDto, i => i.MapFrom(o => o.TypeOfCar))
            .ForMember(x => x.MotorDto, i => i.MapFrom(o => o.Motor))
            .ForMember(x => x.GearboxDto,i => i.MapFrom(o => o.Gearbox))
            .ForMember(x => x.PriceDto, i => i.MapFrom(o => o.Price))
            .ForMember(x => x.AeroportId, i => i.MapFrom(o => o.Aeroport.AeroportId));

        //
        CreateMap<Reservation, ResDates>()
            .ForSourceMember(x => x.Bill, y => y.DoNotValidate())
            .ForSourceMember(x => x.Car, y => y.DoNotValidate())
            .ForSourceMember(x => x.Client, y => y.DoNotValidate())
            .ForSourceMember(x => x.Start_Km, y => y.DoNotValidate())
            .ForSourceMember(x => x.Id, y => y.DoNotValidate());

        //------------------------------------------------------------------
        //Country
        CreateMap<CountryDto, Country>().ReverseMap();

        //GetCountries
        CreateMap<Country, CountryDto>().
            ForSourceMember(x => x.Aeroports, o => o.DoNotValidate());
        //PostCountry
        CreateMap<CountryCreationDto, Country>().
           ForMember(x => x.Aeroports, option => option.Ignore());

        CreateMap<AeroportDto, CountryViewModelDto>()
            .ReverseMap();

        //------------------------------------------------------------------
        //Aeroport
        //Create Aeroport
        CreateMap<AeroportCreationDto, Aeroport>()
            //.ForMember(x => x.Country, o => o.MapFrom(x => x.Country))
            .ForMember(x => x.Cars, o => o.Ignore())
            .ForMember(x => x.Prices, o => o.Ignore());
        //GetAeroportInfo
        CreateMap<Aeroport, AeroportDto>().
            ForSourceMember(x => x.Prices, o => o.DoNotValidate()).
            ForSourceMember(x => x.Cars, o => o.DoNotValidate()).
            ForSourceMember(x => x.Country, o => o.DoNotValidate());

        CreateMap<AeroportDto, Aeroport>().
           ForMember(x => x.Country, o => o.Ignore()).
           ForMember(x => x.Cars, o => o.Ignore()).
           ForMember(x => x.Prices, o => o.Ignore()).
           ForSourceMember(x => x.AeroportId, o => o.DoNotValidate());
        //----------------------------------------------------
        //----------------------------------------------------
        //TypeOfCar
        //POST
        CreateMap<TypeOfCarCreationDto, TypeOfCar>().
            ForMember(w => w.Cars, x => x.Ignore()).
            ReverseMap();
        //Edit Get
        CreateMap<TypeOfCar, TypeOfCarDto>().
            ForSourceMember(x => x.Cars, o => o.DoNotValidate());
        //Edit POST
        CreateMap<TypeOfCarDto, TypeOfCar>()
            .ForMember(x => x.Cars, i => i.Ignore());
        //----------------------------------------------------
        //Formule Price
        //PostPrice
        CreateMap<PriceCreationDto, FormulePrice>().
            ForMember(x => x.Aeroport, i => i.Ignore()).
            ForMember(x => x.Car, i => i.Ignore());

        ////GetPrice / GET:EditPrice
        CreateMap<FormulePrice, PriceDto>()
            .ForSourceMember(x => x.Aeroport, o => o.DoNotValidate())
            .ForSourceMember(x => x.Car, o => o.DoNotValidate());

        //POST:EditPrice
        CreateMap<PriceDto, FormulePrice>().
            ForMember(x => x.Aeroport, o => o.Ignore()).
            ForMember(x => x.Car, o => o.Ignore());

        //---------------------------------------------------------
        //---------------------------------------------------------
        //CreditCard
        CreateMap<CreditCard, CreditCardDto>()
            .ForMember(x => x.CardType, y => y.Ignore())
            .ForSourceMember(x => x.Client, y => y.DoNotValidate())      
            .ForMember(x => x.CardTypeName, y => y.MapFrom(z => z.CardType));

        CreateMap<CreditCardCreationDto, CreditCard>()
            .ForMember(x => x.Client, y => y.Ignore())
            .ForMember(x => x.CardType, y => y.MapFrom(z => z.CreditTypeName));
    }
}
