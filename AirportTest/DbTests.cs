using Business_Layer.Services;
using Data_Access_Layer.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using AirportTests.Modules;
using Data_Access_Layer.DbInitializer;
using System.Threading.Tasks;

namespace AirportTests
{
    [TestFixture]
    class DbTests
    {
        AirportService _airportService;

        public DbTests()
        {
            var kernel = new StandardKernel(new AirPortServiceModule());

            _airportService = kernel.Get<AirportService>();
        }

        [SetUp]
        public async Task Init()
        {
            await _airportService.Seed();
        }

        [Test]
        public async Task CreateGetPilot_when_create_pilot_then_get_pilot_correct()
        {
            Pilot pilot = new Pilot() { Name = "name", Surname = "surname", Experience = 5 };

            await _airportService.Post(pilot);
            await _airportService.SaveChanges();
            var pilots = await _airportService.GetAll<Pilot>();
            var newPilot = pilots.Last();

            Assert.AreEqual(pilot.Name, newPilot.Name);
            Assert.AreEqual(pilot.Surname, newPilot.Surname);
            Assert.AreEqual(pilot.Experience, newPilot.Experience);
        }

        [Test]
        public async Task UpdatePilot_when_update_pilot_then_get_pilot_correct()
        {
            var pilots = await _airportService.GetAll<Pilot>();
            var pilot = pilots.Last();
            pilot.Surname = "updated";

            await _airportService.SaveChanges();

            var pilotsUpd = await _airportService.GetAll<Pilot>();
            var newPilot = pilotsUpd.FirstOrDefault(p => p.Surname == "updated");

            Assert.False(newPilot == null);
        }

        [Test]
        public async Task DeletePilot_when_delete_pilot_then_get_null()
        {
            var pilots = await _airportService.GetAll<Pilot>();
            var newPilot = pilots.Last();

            Assert.False(newPilot == null);

            await _airportService.Delete<Pilot>(newPilot.Id);
            await _airportService.SaveChanges();


            var deletedPilot = await _airportService.GetById<Pilot>(newPilot.Id);

            Assert.True(deletedPilot == null);

            await _airportService.Post(new Pilot()
            {
                Name = newPilot.Name,
                Surname = newPilot.Surname,
                Experience = newPilot.Experience
            });

            await _airportService.SaveChanges();

        }

        [Test]
        public async Task CreateGetFlight_when_create_flight_then_get_flight_correct()
        {
            Flight flight = new Flight()
            {
                ArrivalTime = new DateTime(2015, 10, 10),
                DepartureFrom = "TestDep",
                Destination = "TestDest",
                TimeOfDeparture = new DateTime(2015,10,9),
            };

            await _airportService.Post(flight);
            await _airportService.SaveChanges();
            var flights = await _airportService.GetAll<Flight>();
            var newFlight = flights.Last();

            Assert.AreEqual(flight.ArrivalTime, newFlight.ArrivalTime);
            Assert.AreEqual(flight.DepartureFrom, newFlight.DepartureFrom);
            Assert.AreEqual(flight.Destination, newFlight.Destination);
            await _airportService.SaveChanges();
        }

        [Test]
        public async Task UpdateFlight_when_update_flight_then_get_flight_correct()
        {
            var flights = await _airportService.GetAll<Flight>();
            var flight = flights.Last();

            flight.DepartureFrom = "updated";

            await _airportService.SaveChanges();

            var newFlights = await _airportService.GetAll<Flight>();
            var newFlight = newFlights.FirstOrDefault(p => p.DepartureFrom == "updated");

            Assert.False(newFlight == null);
        }

        [Test]
        public async Task DeleteFlight_when_delete_flight_then_get_null()
        {
            var flights = await _airportService.GetAll<Flight>();
            var flight = flights.Last();

            Assert.False(flight == null);

            await _airportService.Delete<Flight>(flight.Number);
            await _airportService.SaveChanges();


            var deletedFlight = await _airportService.GetById<Flight>(flight.Number);

            Assert.True(deletedFlight == null);

            await _airportService.Post(new Flight()
            {
                ArrivalTime = flight.ArrivalTime,
                DepartureFrom = flight.Destination,
                Destination = flight.Destination,
                TimeOfDeparture = flight.TimeOfDeparture
            });

            await _airportService.SaveChanges();
        }

        [Test]
        public async Task GetCorrectTickets_when_add_tickets_then_get_correct_tickets_in_flights()
        {
            Flight flight = new Flight()
            {
                ArrivalTime = new DateTime(2015, 10, 10),
                DepartureFrom = "TestDep2",
                Destination = "TestDest2",
                TimeOfDeparture = new DateTime(2015, 10, 9),
            };

            await _airportService.Post(flight);

            await _airportService.SaveChanges();

            var flights = await _airportService.GetAll<Flight>();
            var flightNum = flights.Last().Number;

            List<Ticket> tickets = new List<Ticket>()
            {
                new Ticket(){Price = 250, FlightNumber = flightNum},
                new Ticket(){Price = 350, FlightNumber = flightNum}
            };

            await _airportService.Post(tickets[0]);
            await _airportService.Post(tickets[1]);

            await _airportService.SaveChanges();

            var newFlight = await _airportService.GetAll<Flight>();

            Assert.AreEqual(newFlight.Last().Tickets.Count(), 2);
        }
        [Test]
        public async Task GetCorrectStewardesses_when_add_stewardesses_then_get_correct_stewardesses_in_crews()
        {
            Crew Crew = new Crew()
            {
                Pilot = new Pilot() { Name = "name", Surname = "sur" }
            };

            await _airportService.Post(Crew);

            await _airportService.SaveChanges();

            List<Stewardess> stewardesses = new List<Stewardess>()
            {
                new Stewardess(){Name = "nameSt", Surname = "surSt", CrewId = (await _airportService.GetAll<Crew>()).Last().Id},
                new Stewardess(){Name = "nameSt2", Surname = "surSt2", CrewId = (await _airportService.GetAll<Crew>()).Last().Id}
            };

            await _airportService.Post(stewardesses[0]);
            await _airportService.Post(stewardesses[1]);

            await _airportService.SaveChanges();

            Assert.AreEqual((await _airportService.GetAll<Crew>()).Last().Stewardesses.Count(), 2);
        }

        [Test]
        public async Task CreateGetStewardess_when_create_stewardess_then_get_stewardess_correct()
        {
            Stewardess stewardess = new Stewardess() { Name = "name", Surname = "surname", CrewId = (await _airportService.GetAll<Crew>()).First().Id };

            await _airportService.Post(stewardess);
            await _airportService.SaveChanges();
            var newStewardess = (await _airportService.GetAll<Stewardess>()).Last();

            Assert.AreEqual(stewardess.Name, newStewardess.Name);
            Assert.AreEqual(stewardess.Surname, stewardess.Surname);
        }

        [Test]
        public async Task DeletePlaneType_when_delete_PlaneType_then_get_null()
        {
            var planeType = (await _airportService.GetAll<PlaneType>()).Last();

            Assert.False(planeType == null);

            await _airportService.Delete<PlaneType>(planeType.Id);
            await _airportService.SaveChanges();


            var deletedPlaneType = await _airportService.GetById<PlaneType>(planeType.Id);

            Assert.True(deletedPlaneType == null);

            await _airportService.Post(new PlaneType()
            {
                NumberOfSeats = planeType.NumberOfSeats,
                LoadCapacity = planeType.LoadCapacity,
                Model = planeType.Model
            });

            await _airportService.SaveChanges();
        }
    }

}