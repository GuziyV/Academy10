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
    [Route("api/Pilots")]
    public class PilotsController : Controller
    {
        private readonly AirportService _service;
        private readonly IMapper _mapper;
        PilotDTOValidator validator = new PilotDTOValidator();

        public PilotsController(IMapper mapper, AirportService service)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/pilots
        [HttpGet]
        public async Task<IEnumerable<PilotDTO>> Get()
        {
            return Mapper.Map<IEnumerable<Pilot>, IEnumerable<PilotDTO>>(await _service.GetAll<Pilot>());
        }

        // GET api/pilots/id
        [HttpGet("{id}")]
        public async Task<PilotDTO> Get(int id)
        {
            return Mapper.Map<Pilot, PilotDTO>(await _service.GetById<Pilot>(id));
        }

        // POST api/pilots
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]PilotDTO pilot)
        {
            if (ModelState.IsValid && pilot != null && validator.Validate(pilot).IsValid)
            {
                await _service.Post<Pilot>(_mapper.Map<PilotDTO, Pilot>(pilot));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST api/pilots/id
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]PilotDTO pilot)
        {
            if (ModelState.IsValid && pilot != null && validator.Validate(pilot).IsValid)
            {
                await _service.Update<Pilot>(id, _mapper.Map<PilotDTO, Pilot>(pilot));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/pilots/id
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete<Pilot>(id);
            await _service.SaveChanges();
        }
    }
}