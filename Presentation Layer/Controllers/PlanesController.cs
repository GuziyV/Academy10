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
    [Route("api/Planes")]
    public class PlanesController : Controller
    {
        private readonly AirportService _service;
        private readonly IMapper _mapper;
        PlaneDTOValidator validator = new PlaneDTOValidator();

        public PlanesController(IMapper mapper, AirportService service)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/planes
        [HttpGet]
        public async Task<IEnumerable<PlaneDTO>> Get()
        {
            return _mapper.Map<IEnumerable<Plane>, IEnumerable<PlaneDTO>>(await _service.GetAll<Plane>());
        }

        // GET api/planes/id
        [HttpGet("{id}")]
        public async Task<PlaneDTO> Get(int id)
        {
            return _mapper.Map<Plane, PlaneDTO>(await _service.GetById<Plane>(id));
        }


        // POST api/planes
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]PlaneDTO plane)
        {
            if (ModelState.IsValid && plane != null && validator.Validate(plane).IsValid)
            {
                await _service.Post<Plane>(_mapper.Map<PlaneDTO, Plane>(plane));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST api/planes/id
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]PlaneDTO plane)
        {
            if (ModelState.IsValid && plane != null && validator.Validate(plane).IsValid)
            {
                await _service.Update<Plane>(id, _mapper.Map<PlaneDTO, Plane>(plane));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/planes/id
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete<Plane>(id);
            await _service.SaveChanges();
        }
    }
}