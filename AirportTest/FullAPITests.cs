using AirportTests.Modules;
using Business_Layer.MyMapperConfiguration;
using Business_Layer.Services;
using Ninject;
using NUnit.Framework;
using Presentation_Layer.Controllers;
using Shared.DTOs;
using System;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportTests
{
    [TestFixture]
    class FullAPITests
    {
        FlightsController _flightController;
        TicketsController _ticketController;

        public FullAPITests()
        {
            var kernel = new StandardKernel(new AirPortServiceModule());
            var mapper = MyMapperConfiguration.GetConfiguration().CreateMapper();
            _flightController = new FlightsController(mapper, kernel.Get<AirportService>());
            _ticketController = new TicketsController(mapper, kernel.Get<AirportService>());
        }


        [Test]
        public async Task GetLastFlightGood_whet_post_last_flight_then_get_correct_flight()
        {
            FlightDTO flight = new FlightDTO()
            {
                ArrivalTime = new DateTime(2018, 10, 10),
                DepartureFrom = "CityTest",
                Destination = "City2",
                TimeOfDeparture = new DateTime(2018, 10, 10)
            };

            await _flightController.Post(flight);

            var newFlight = (await _flightController.Get()).Last();

            Assert.AreEqual(flight.ArrivalTime, newFlight.ArrivalTime);
            Assert.AreEqual(flight.DepartureFrom, newFlight.DepartureFrom);
            Assert.AreEqual(flight.Destination, newFlight.Destination);
        }

        [Test]
        public async Task DeleteFlight_whet_delete_flight_then_get_null_flight()
        {
            FlightDTO flight = new FlightDTO()
            {
                ArrivalTime = new DateTime(2018, 10, 10),
                DepartureFrom = "CityTest",
                Destination = "City2",
                TimeOfDeparture = new DateTime(2018, 10, 10)
            };

            await _flightController.Post(flight);

            var newFlight = (await _flightController.Get()).Last();

            Assert.AreEqual(flight.ArrivalTime, newFlight.ArrivalTime);
            Assert.AreEqual(flight.DepartureFrom, newFlight.DepartureFrom);
            Assert.AreEqual(flight.Destination, newFlight.Destination);

            await _flightController.Delete(newFlight.Number);

            var deletedFlight = await _flightController.Get(newFlight.Number);

            Assert.IsNull(deletedFlight);
        }

        [Test]
        public async Task GetTicketsTest_when_post_flight_with_tickets_then_get_correct_tickets()
        {
            FlightDTO flight = new FlightDTO()
            {
                ArrivalTime = new DateTime(2018, 10, 10),
                DepartureFrom = "CityTest",
                Destination = "City2",
                TimeOfDeparture = new DateTime(2018, 10, 10),
                Tickets = new List<TicketDTO>()
                {
                    new TicketDTO(){Price = 123456}
                }
            };

            await _flightController.Post(flight);

            var ticket = (await _ticketController.Get()).Last();

            Assert.AreEqual(ticket.Price, 123456);
        }


        [Test]
        public async Task PostFlightTestGoodResult_when_post_correct_then_HttpOK()
        {
            FlightDTO flight = new FlightDTO()
            {
                ArrivalTime = new DateTime(2018, 10, 10),
                DepartureFrom = "City",
                Destination = "City2",
                TimeOfDeparture = new DateTime(2018, 10, 10)
            };

            Assert.AreEqual(new HttpResponseMessage(HttpStatusCode.OK).StatusCode,
                (await _flightController.Post(flight)).StatusCode);
        }

        [Test]
        public async Task PostFlightTestBadResult_when_post_Bad_then_HttpBAD()
        {
            FlightDTO flight = new FlightDTO()
            {
                DepartureFrom = "",
                Destination = "City2",
                TimeOfDeparture = new DateTime(2018, 10, 10)
            };

            Assert.AreEqual(new HttpResponseMessage(HttpStatusCode.BadRequest).StatusCode,
                (await _flightController.Post(flight)).StatusCode);
        }
    }
}
