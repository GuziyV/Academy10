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
    [Route("api/Stewardesses")]
    public class StewardessesController : Controller
    {
        private readonly AirportService _service;
        private readonly IMapper _mapper;
        StewardessDTOValidator validator = new StewardessDTOValidator();

        public StewardessesController(IMapper mapper, AirportService service)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/stewardesses
        [HttpGet]
        public async Task<IEnumerable<StewardessDTO>> Get()
        {
            return Mapper.Map<IEnumerable<Stewardess>, IEnumerable<StewardessDTO>>(await _service.GetAll<Stewardess>());
        }

        // GET api/stewardesses/id
        [HttpGet("{id}")]
        public async Task<StewardessDTO> Get(int id)
        {
            return Mapper.Map<Stewardess, StewardessDTO>(await _service.GetById<Stewardess>(id));
        }

        // POST api/stewardesses
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]StewardessDTO stewardess)
        {
            if (ModelState.IsValid && stewardess != null && validator.Validate(stewardess).IsValid)
            {
                await _service.Post<Stewardess>(Mapper.Map<StewardessDTO, Stewardess>(stewardess));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST api/stewardesses/id
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]StewardessDTO stewardess)
        {
            if (ModelState.IsValid && stewardess != null && validator.Validate(stewardess).IsValid)
            {
                await _service.Update<Stewardess>(id, Mapper.Map<StewardessDTO, Stewardess>(stewardess));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/stewardesses/id
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete<Stewardess>(id);
            await _service.SaveChanges();
        }
    }
}