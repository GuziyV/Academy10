using Data_Access_Layer.Contexts;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DbInitializer
{
    static public class AirportDbInitializer
    {
        static public async Task Drop(AirportContext context)
        {
            await context.Database.EnsureDeletedAsync();
        }


        static public async Task Initialize(AirportContext context)
        {
            await context.Database.MigrateAsync(); 
            
            if(context.Flights.Any())
            { 
                return; //already seeded
            }
            
            List<Pilot> pilots = new List<Pilot>()
            {
                new Pilot(){Name = "PName1", Surname = "PSurname1", Experience = 3},
                new Pilot(){Name = "PName2", Surname = "PSurname2", Experience = 4},
                new Pilot(){Name = "PName3", Surname = "PSurname3", Experience = 9},
            };

            List<Stewardess> stewardesses = new List<Stewardess>()
            {
                new Stewardess(){Name = "SName1", Surname = "SSurname1", DateOfBirth = new DateTime(1992, 10, 11)},
                new Stewardess(){Name = "SName2", Surname = "SSurname2", DateOfBirth = new DateTime(1991, 10, 11)},
                new Stewardess(){Name = "SName3", Surname = "SSurname3", DateOfBirth = new DateTime(1993, 10, 11)},
                new Stewardess(){Name = "SName4", Surname = "SSurname4", DateOfBirth = new DateTime(1994, 10, 11)},
                new Stewardess(){Name = "SName5", Surname = "SSurname5", DateOfBirth = new DateTime(1993, 11, 15)}
            };

            await context.Pilots.AddRangeAsync(pilots);

            List<Crew> crews = new List<Crew>()
            {
                new Crew()
                {
                    Pilot = pilots[1],
                    Stewardesses = new List<Stewardess>
                    {
                        stewardesses[1],
                        stewardesses[3]
                    }
                },
                new Crew()
                {
                    Pilot = pilots[0],
                    Stewardesses = new List<Stewardess>
                    {
                        stewardesses[0],
                        stewardesses[2]
                    }
                },
                new Crew()
                {
                    Pilot = pilots[2],
                    Stewardesses = new List<Stewardess>
                    {
                        stewardesses[4],
                    }
                }
            };


            await context.Stewardesses.AddRangeAsync(stewardesses);

            await context.Crews.AddRangeAsync(crews);

            List<PlaneType> planeTypes = new List<PlaneType>()
            {
                new PlaneType()
                {
                    Model = "Model1",
                    NumberOfSeats = 150,
                    LoadCapacity = 1000
                },
                new PlaneType()
                {
                    Model = "Model2",
                    NumberOfSeats = 125,
                    LoadCapacity = 985
                },
                new PlaneType()
                {
                    Model = "Model1",
                    NumberOfSeats = 189,
                    LoadCapacity = 1010
                }
            };

            await context.PlaneTypes.AddRangeAsync(planeTypes);

            List<Plane> planes = new List<Plane>()
            {
                new Plane()
                {
                    PlaneType = planeTypes[1],
                    ReleaseDate = new DateTime(2008,11,18)
                },
                new Plane()
                {
                    PlaneType = planeTypes[2],
                    ReleaseDate = new DateTime(2012,2,25)
                },
                new Plane()
                {
                    PlaneType = planeTypes[1],
                    ReleaseDate = new DateTime(2011,5,10)
                },
                new Plane()
                {
                    PlaneType = planeTypes[0],
                    ReleaseDate = new DateTime(2010,8,7)
                }
            };

            await context.Planes.AddRangeAsync(planes);

            List<Ticket> tickets = new List<Ticket>()
            {
                new Ticket()
                {
                    FlightNumber = 2,
                    Price = 258
                },
                new Ticket()
                {
                    FlightNumber = 1,
                    Price = 257
                },
                new Ticket()
                {
                    FlightNumber = 2,
                    Price = 256
                },
                new Ticket()
                {
                    FlightNumber = 3,
                    Price = 255
                }
            };

            await context.Tickets.AddRangeAsync(tickets);

            List<Flight> flights = new List<Flight>()
            {
                new Flight()
                {
                    Tickets = new List<Ticket>()
                    {
                        tickets[1]
                    },
                    DepartureFrom = "Kyiv",
                    Destination = "Tokio",
                    ArrivalTime = new DateTime(2018, 02, 03),
                    TimeOfDeparture = new DateTime(2018,02,02),
                },

                new Flight()
                {
                    Tickets = new List<Ticket>()
                    {
                        tickets[0],
                        tickets[2]
                    },
                    DepartureFrom = "Dublin",
                    Destination = "Paris",
                    ArrivalTime = new DateTime(2017, 02, 03),
                    TimeOfDeparture = new DateTime(2017,02,02),
                },

                new Flight()
                {
                    Tickets = new List<Ticket>()
                    {
                        tickets[3]
                    },
                    DepartureFrom = "Odesa",
                    Destination = "Ternopil",
                    ArrivalTime = new DateTime(2016, 02, 03),
                    TimeOfDeparture = new DateTime(2016,02,02),
                },
            };

            await context.Flights.AddRangeAsync(flights);

            List<Departure> departures = new List<Departure>()
            {
                new Departure()
                {
                    Crew = crews[1],
                    Flight = flights[1],
                    Plane = planes[2],
                    TimeOfDeparture = new DateTime(2017, 02, 03)
                },
                new Departure()
                {
                    Crew = crews[2],
                    Flight = flights[0],
                    Plane = planes[1],
                    TimeOfDeparture = new DateTime(2018, 02, 03)
                },
                new Departure()
                {
                    Crew = crews[0],
                    Flight = flights[2],
                    Plane = planes[0],
                    TimeOfDeparture = new DateTime(2016, 02, 03)
                }
            };
            await context.Departures.AddRangeAsync(departures);

            await context.SaveChangesAsync();

        }
    }
}
