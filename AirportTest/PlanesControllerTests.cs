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
    class PlanesControllerTests
    {
        PlanesController _controller;
        public PlanesControllerTests()
        {
            var mapper = MyMapperConfiguration.GetConfiguration().CreateMapper();
            AirportService service = new AirportService(new FakeUnitOfWork());
            _controller = new PlanesController(mapper, service);
        }

        [Test]
        public async Task PostPlaneTestGoodResult_when_post_correct_then_HttpOK()
        {
            PlaneDTO plane = new PlaneDTO()
            {
                ReleaseDate = new DateTime(2018, 10, 9),
                PlaneType = new PlaneTypeDTO()
                {
                    LoadCapacity = 1000,
                    NumberOfSeats = 500,
                    Model = "Model"
                }
                
            };

            Assert.AreEqual(new HttpResponseMessage(System.Net.HttpStatusCode.OK).StatusCode,
                (await _controller.Post(plane)).StatusCode);
        }

        [Test]
        public async Task PostPlaneTestBadResult_when_post_Bad_then_HttpBAD()
        {
            PlaneDTO plane = new PlaneDTO()
            {
                ReleaseDate = new DateTime(2018, 9, 9),
            };

            Assert.AreEqual(new HttpResponseMessage(HttpStatusCode.BadRequest).StatusCode,
                (await _controller.Post(plane)).StatusCode);
        }
    }
}
