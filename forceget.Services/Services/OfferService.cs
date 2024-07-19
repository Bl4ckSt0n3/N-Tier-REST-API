using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forceget.Services.Abstract;
using forceget.DataAccess.Models.DbContexts;
using forceget.DataAccess.Models.Dtos;
using forceget.DataAccess.Models.Entities;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace forceget.Services.Services
{
    public class OfferService : IOfferService
    {
        private readonly DimensionsDbContext _dimensionsDbContext;
        private readonly OffersDbContext _offersDbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public OfferService(DimensionsDbContext dimensionsDbContext, IConfiguration configuration, IMapper mapper, OffersDbContext offersDbContext)
        {
            _dimensionsDbContext = dimensionsDbContext;
            _offersDbContext = offersDbContext;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<Offer>>> GetAll()
        {
            var response = new ServiceResponse<List<Offer>>();

            try 
            {
                var offers = await _offersDbContext.Offers.ToListAsync();
                response.Data = offers.Select(x => _mapper.Map<Offer>(x)).ToList();
                response.Message = "Success";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<Offer>();
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<Offer>> Create(Offer request)
        {
            var response = new ServiceResponse<Offer>();
            var dimensions = await _dimensionsDbContext.Dimensions.ToListAsync();
            try 
            {
                await _offersDbContext.Offers.AddAsync(_mapper.Map<Offer>(request));
                await _offersDbContext.SaveChangesAsync();
                
                response.Data = request;
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Data = new Offer();
                response.Success = false;
                response.Message = "Error";
            }
            return response;
        }


        public async Task<ServiceResponse<List<Dimensions>>> GetAllDimensions()
        {
            var response = new ServiceResponse<List<Dimensions>>();
            try
            {
                var dimensions = await _dimensionsDbContext.Dimensions.ToListAsync();
                
                response.Data = dimensions.Select(x => _mapper.Map<Dimensions>(x)).ToList();
                response.Message = "Success";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<Dimensions>();
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public double CalculateNumberOfBox(Dimensions obj)
        {
            // Carton to Box
            // double Calc = Math.Floor(BoxWidth/CartonWidth) * Math.Floor(BoxHeight/CartonHeight) * Math.Floor(BoxLength/CartonLength);

            return 0.2;
        }

        public double CalculateNumberOfPallet(Dimensions obj)
        {
            // Boxes to Pallet
            // double Calc = Math.Floor(PalletWidth/BoxWidth) * Math.Floor(PalletHeight/BoxHeight) * Math.Floor(PalletLength/BoxLength);
            return 0.2;
        }
    }
}