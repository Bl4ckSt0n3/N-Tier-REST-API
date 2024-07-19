using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forceget.DataAccess.Models.Dtos
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; } = null;
    }
}