using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Data_Access_Layer.Models;
using Shared.DTOs;

namespace Business_Layer.MyMapperConfiguration
{
    public class MyMapperConfiguration
    {
        static public MapperConfiguration GetConfiguration()
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
            });
        }
    }
}
