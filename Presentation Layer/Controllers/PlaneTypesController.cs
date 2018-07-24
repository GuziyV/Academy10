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
    [Route("api/PlaneTypes")]
    public class PlaneTypesController : Controller
    {
        private readonly AirportService _service;
        private readonly IMapper _mapper;
        PlaneTypeDTOValidator validator = new PlaneTypeDTOValidator();

        public PlaneTypesController(IMapper mapper, AirportService service)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/planetypes
        [HttpGet]
        public async Task<IEnumerable<PlaneTypeDTO>> Get()
        {
            return Mapper.Map<IEnumerable<PlaneType>, IEnumerable<PlaneTypeDTO>>(await _service.GetAll<PlaneType>());
        }

        // GET api/planestype/id
        [HttpGet("{id}")]
        public async Task<PlaneTypeDTO> Get(int id)
        {
            return Mapper.Map<PlaneType, PlaneTypeDTO>(await _service.GetById<PlaneType>(id));
        }

        // POST api/planetypes
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]PlaneTypeDTO planeType)
        {
            if (ModelState.IsValid && planeType != null && validator.Validate(planeType).IsValid)
            {
                await _service.Post<PlaneType>(Mapper.Map<PlaneTypeDTO, PlaneType>(planeType));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST api/planettypes/id
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]PlaneTypeDTO planeType)
        {
            if (ModelState.IsValid && planeType != null && validator.Validate(planeType).IsValid)
            {
                await _service.Update<PlaneType>(id, Mapper.Map<PlaneTypeDTO, PlaneType>(planeType));
                await _service.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/planettypes/id
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete<PlaneType>(id);
            await _service.SaveChanges();
        }
    }
}