using AirportTests.Fakes;
using Business_Layer.MyMapperConfiguration;
using Business_Layer.Services;
using NUnit.Framework;
using Presentation_Layer.Controllers;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AirportTests
{
    [TestFixture]
    class CrewsControllerTests
    {
        CrewsController _controller;
        public CrewsControllerTests()
        {
            var mapper = MyMapperConfiguration.GetConfiguration().CreateMapper();
            AirportService service = new AirportService(new FakeUnitOfWork());
            _controller = new CrewsController(mapper, service);
        }

        [Test]
        public async Task PostCrewTestGoodResult_when_post_correct_then_HttpOK()
        {
            CrewDTO crew = new CrewDTO()
            {
                Pilot = new PilotDTO() { Name = "Name", Experience = 5, Surname = "Sur"},
                Stewardesses = new List<StewardessDTO>()
                {
                    new StewardessDTO()
                    {
                        DateOfBirth = new DateTime(1992, 10, 9),
                        Surname = "stSur",
                        Name = "stName",
                    }
                }
            };

            Assert.AreEqual(new HttpResponseMessage(System.Net.HttpStatusCode.OK).StatusCode,
                (await _controller.Post(crew)).StatusCode);
        }

        [Test]
        public async Task PostCrewTestBadResult_when_post_Bad_then_HttpBAD()
        {
            CrewDTO crew = new CrewDTO()
            {
                Pilot = new PilotDTO() { Name = "Name", Surname = "Surname", Experience = 2}
            };

            Assert.AreEqual(new HttpResponseMessage(HttpStatusCode.BadRequest).StatusCode,
                (await _controller.Post(crew)).StatusCode);
        }
    }
}
