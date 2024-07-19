using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using forceget.Services.Abstract;
using forceget.DataAccess.Models.Entities;
using forceget.DataAccess.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace forceget.API.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet("GetAllDimensions")]
        public async Task<ActionResult<ServiceResponse<List<Dimensions>>>> GetDimensions()
        {
            var response = await _offerService.GetAllDimensions();
            return response != null ? Ok(response) : BadRequest();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Offer>>>> GetAll()
        {
            var response = await _offerService.GetAll();
            return response != null ? Ok(response) : BadRequest();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ServiceResponse<List<Offer>>>> Create(Offer request)
        {
            var response = await _offerService.Create(request);
            return response != null ? Ok(response) : BadRequest();
        }
    }
}