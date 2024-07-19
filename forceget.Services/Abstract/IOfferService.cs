using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forceget.DataAccess.Models.Dtos;
using forceget.DataAccess.Models.Entities;

namespace forceget.Services.Abstract
{
    public interface IOfferService
    {
        Task<ServiceResponse<List<Dimensions>>> GetAllDimensions();
        Task<ServiceResponse<List<Offer>>> GetAll();
        Task<ServiceResponse<Offer>> Create(Offer request);
    }
}