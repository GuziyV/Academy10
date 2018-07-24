using Data_Access_Layer.Contexts;
using Data_Access_Layer.DbInitializer;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class AirportUnitOfWork : IUnitOfWork
    {
        private AirportContext _airportContext;

        public AirportUnitOfWork(AirportContext airportContext)
        {
            _airportContext = airportContext;
        }

        public IRepository<T> GetRepository<T>() where T: class
        {
            if(typeof(T) == typeof(Flight))
            {
                return (IRepository<T>)FlightRepository;
            }
            else if (typeof(T) == typeof(Crew))
            {
                return (IRepository<T>)CrewRepository;
            }
            else if (typeof(T) == typeof(Departure))
            {
                return (IRepository<T>)DepartureRepository;
            }
            else if (typeof(T) == typeof(Pilot))
            {
                return (IRepository<T>)PilotRepository;
            }
            else if (typeof(T) == typeof(Plane))
            {
                return (IRepository<T>)PlaneRepository;
            }
            else if (typeof(T) == typeof(PlaneType))
            {
                return (IRepository<T>)PlaneTypeRepository;
            }
            else if (typeof(T) == typeof(Stewardess))
            {
                return (IRepository<T>)StewardessRepository;
            }
            else if (typeof(T) == typeof(Ticket))
            {
                return (IRepository<T>)TicketRepository;
            }
            else
            {
                throw new TypeAccessException("Wrong type of repo");
            }
        }

        public async Task SeedDB()
        {
            await AirportDbInitializer.Initialize(_airportContext);
        }

        public async Task DropDB()
        {
            await AirportDbInitializer.Drop(_airportContext);
        }

        private FlightRepository flightRepository;
        public IRepository<Flight> FlightRepository
        {
            get
            {
                if(flightRepository == null)
                {
                    flightRepository = new FlightRepository(_airportContext);
                }
                return flightRepository;
            }
        }

        private CrewRepository crewRepository;
        public IRepository<Crew> CrewRepository
        {
            get
            {
                if (crewRepository == null)
                {
                    crewRepository = new CrewRepository(_airportContext);
                }
                return crewRepository;
            }
        }

        private DepartureRepository departureRepository;
        public IRepository<Departure> DepartureRepository
        {
            get
            {
                if (departureRepository == null)
                {
                    departureRepository = new DepartureRepository(_airportContext);
                }
                return departureRepository;
            }
        }

        private PilotRepository pilotRepository;
        public IRepository<Pilot> PilotRepository
        {
            get
            {
                if (pilotRepository == null)
                {
                    pilotRepository = new PilotRepository(_airportContext);
                }
                return pilotRepository;
            }
        }

        private PlaneRepository planeRepository;
        public IRepository<Plane> PlaneRepository
        {
            get
            {
                if (planeRepository == null)
                {
                    planeRepository = new PlaneRepository(_airportContext);
                }
                return planeRepository;
            }
        }

        private PlaneTypeRepository planeTypeRepository;

        public IRepository<PlaneType> PlaneTypeRepository
        {
            get
            {
                if (planeTypeRepository == null)
                {
                    planeTypeRepository = new PlaneTypeRepository(_airportContext);
                }
                return planeTypeRepository;
            }
        }

        private StewardessRepository stewardessRepository;
        public IRepository<Stewardess> StewardessRepository
        {
            get
            {
                if (stewardessRepository == null)
                {
                    stewardessRepository = new StewardessRepository(_airportContext);
                }
                return stewardessRepository;
            }
        }

        private TicketRepository ticketRepository;
        public IRepository<Ticket> TicketRepository
        {
            get
            {
                if (ticketRepository == null)
                {
                    ticketRepository = new TicketRepository(_airportContext);
                }
                return ticketRepository;
            }
        }

        public async Task SaveChanges()  
        {
           await _airportContext.SaveChangesAsync();
        }
    }
}
