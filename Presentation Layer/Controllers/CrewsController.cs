using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AutoMapper;
using Business_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Shared.DTOs;
using Business_Layer.DTOValidation;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
    [Produces("application/json")]
    [Route("api/Crews")]
    public class CrewsController : Controller
    {
        private readonly AirportService _service;
        private readonly IMapper _mapper;
        CrewDTOValidator validator = new CrewDTOValidator();

        public CrewsController(IMapper mapper, AirportService service)
        {
            _service = service;
            _mapper = mapper;       
        }

        // GET api/crews/task3
        [HttpGet]
        [Route("task3")]
        public async Task<string> GetCrews()
        {
            return await _service.DownloadTenCrews();
        }

        // GET api/crews
        [HttpGet]
        public async Task<IEnumerable<CrewDTO>> Get()
        {
            return _mapper.Map<IEnumerable<Crew>, IEnumerable<CrewDTO>>(await _service.GetAll<Crew>());
        }

        // GET api/crews/id
        [HttpGet("{id}")]
        public async Task<CrewDTO> Get(int id)
        {
            return _mapper.Map<Crew, CrewDTO>(await _service.GetById<Crew>(id));
        }

        // POST api/crews
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]CrewDTO crew)
        {
            if(ModelState.IsValid && crew != null && validator.Validate(crew).IsValid)
            {
                await _service.Post<Crew>(_mapper.Map<CrewDTO, Crew>(crew));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST api/crews/id
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]CrewDTO crew)
        {
            if (ModelState.IsValid && crew != null && validator.Validate(crew).IsValid)
            {
                await _service.Update<Crew>(id, _mapper.Map<CrewDTO, Crew>(crew));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/crews/id
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete<Crew>(id);
            await _service.SaveChanges();
        }
    }
}