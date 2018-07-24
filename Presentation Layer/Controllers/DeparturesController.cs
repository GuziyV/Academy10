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
    [Route("api/Departures")]
    public class DeparturesController : Controller
    {
        private readonly AirportService _service;
        private readonly IMapper _mapper;
        DepartureDTOValidator validator = new DepartureDTOValidator();

        public DeparturesController(IMapper mapper, AirportService service)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/departures
        [HttpGet]
        public async Task<IEnumerable<DepartureDTO>> Get()
        {
            return Mapper.Map<IEnumerable<Departure>, IEnumerable<DepartureDTO>>(await _service.GetAll<Departure>());
        }

        // GET api/departures/id
        [HttpGet("{id}")]
        public async Task<DepartureDTO> Get(int id)
        {
            return Mapper.Map<Departure, DepartureDTO>(await _service.GetById<Departure>(id));
        }

        // POST api/departures
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]DepartureDTO departure)
        {
            if (ModelState.IsValid && departure != null && validator.Validate(departure).IsValid)
            {
                await _service.Post<Departure>(Mapper.Map<DepartureDTO, Departure>(departure));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST api/departures/id
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]DepartureDTO departure)
        {
            if (ModelState.IsValid && departure != null && validator.Validate(departure).IsValid)
            {
                await _service.Update<Departure>(id, Mapper.Map<DepartureDTO, Departure>(departure));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/departures/id
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete<Departure>(id);
            await _service.SaveChanges();
        }
    }
}