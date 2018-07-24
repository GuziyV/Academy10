using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Business_Layer.DTOValidation;
using Business_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Presentation_Layer.Controllers
{
    [Produces("application/json")]
    [Route("api/Flights")]
    public class FlightsController : Controller
    { 
        private readonly AirportService _service;
        private readonly IMapper _mapper;
        FlightDTOValidator validator = new FlightDTOValidator();

        public FlightsController(IMapper mapper, AirportService service)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/flights
        [HttpGet]
        public async Task<IEnumerable<FlightDTO>> Get()
        {
            return  _mapper.Map<IEnumerable<Flight>, IEnumerable<FlightDTO>>(await _service.GetAll<Flight>());
        }

        // GET api/flights/id
        [HttpGet("{number}")]
        public async Task<FlightDTO> Get(int number)
        {
            return _mapper.Map<Flight, FlightDTO>(await _service.GetById<Flight>(number));
        }

        // POST api/flights
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]FlightDTO flight)
        {
            if (ModelState.IsValid && validator.Validate(flight).IsValid && flight != null)
            {
                await _service.Post<Flight>(_mapper.Map<FlightDTO, Flight>(flight));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST api/flights/number
        [HttpPut("{number}")]
        public async Task<HttpResponseMessage> Put(int number, [FromBody]FlightDTO flight)
        {
            if (ModelState.IsValid && flight != null)
            {
                await _service.Update<Flight>(number, _mapper.Map<FlightDTO, Flight>(flight));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/flights/number
        [HttpDelete("{number}")]
        public async Task Delete(int number)
        {
            await _service.Delete<Flight>(number);
            await _service.SaveChanges();
        }
    }
}