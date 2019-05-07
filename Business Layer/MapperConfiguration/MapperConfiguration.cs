using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Data_Access_Layer.Models;
using DAL.Models;
using Shared.DTos;

namespace Business_Layer.MyMapperConfiguration
{
    public class MyMapperConfiguration
    {
        static public IMapper GetConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightDTO>();
                cfg.CreateMap<Departure, DepartureDTO>();
                cfg.CreateMap<Pilot, PilotDTO>();
                cfg.CreateMap<Plane, PlaneDTO>();
                cfg.CreateMap<PlaneType, PlaneTypeDTO>();
                cfg.CreateMap<Stewardess, StewardessDTO>();
                cfg.CreateMap<Ticket, TicketDTO>();
				cfg.CreateMap<UserDTO, User>()
					.ForMember(c => c.Id, c => c.MapFrom(cd => cd.Id))
					.ForMember(c => c.Login, c => c.MapFrom(cd => cd.Login))
					.ForMember(c => c.Email, c => c.MapFrom(cd => cd.Email))
					.ForMember(c => c.Telephone, c => c.MapFrom(cd => cd.Telephone))
					.ForMember(c => c.IP, c => c.MapFrom(cd => cd.IP))
					.ForMember(c => c.Role, c => c.MapFrom(cd => cd.Role))
					.ForMember(c => c.PasswordHash, c => c.Ignore())
					.ForMember(c => c.PasswordSalt, c => c.Ignore());
				cfg.CreateMap<User, UserDTO>()
					.ForMember(c => c.Id, c => c.MapFrom(cd => cd.Id))
					.ForMember(c => c.Login, c => c.MapFrom(cd => cd.Login))
					.ForMember(c => c.Email, c => c.MapFrom(cd => cd.Email))
					.ForMember(c => c.Telephone, c => c.MapFrom(cd => cd.Telephone))
					.ForMember(c => c.Role, c => c.MapFrom(cd => cd.Role))
					.ForMember(c => c.IP, c => c.MapFrom(cd => cd.IP))
					.ForMember(c => c.Password, c => c.Ignore());
			}).CreateMapper();
        }
    }
}
