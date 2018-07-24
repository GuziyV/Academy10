using System;
using System.Collections.Generic;
using AirportTests.Modules;
using Business_Layer.MyMapperConfiguration;
using Business_Layer.Services;
using Ninject;
using NUnit.Framework;
using Presentation_Layer.Controllers;
using Shared.DTOs;
using System.Net;
using System.Net.Http;
using AirportTests.Fakes;
using System.Threading.Tasks;

namespace AirportTests
{
    [TestFixture]
    class PilotsControllerTests
    {
        PilotsController _controller;
        public PilotsControllerTests()
        {
            var mapper = MyMapperConfiguration.GetConfiguration().CreateMapper();
            AirportService service = new AirportService(new FakeUnitOfWork());
            _controller = new PilotsController(mapper, service);
        }

        [Test]
        public async Task PostPilotTestGoodResult_when_post_correct_then_HttpOK()
        {
            PilotDTO flight = new PilotDTO()
            {
                Name = "Name", Experience = 2, Surname = "Surname"
            };

            Assert.AreEqual(new HttpResponseMessage(HttpStatusCode.OK).StatusCode,
                (await _controller.Post(flight)).StatusCode);
        }

        [Test]
        public async Task PostPilotTestBadResult_when_post_Bad_then_HttpBAD()
        {
            PilotDTO pilot = new PilotDTO()
            {
                Name = "Name2",
                Experience = 2,
                Surname = ""
            };


            Assert.AreEqual(new HttpResponseMessage(HttpStatusCode.BadRequest).StatusCode,
                (await _controller.Post(pilot)).StatusCode);
        }
    }
}
