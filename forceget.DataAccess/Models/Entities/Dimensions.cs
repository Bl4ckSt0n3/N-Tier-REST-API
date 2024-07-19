using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forceget.DataAccess.Models.Entities
{
    public class Dimensions
    {
        public long Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Width { get; set; } = 0;
        public int Length { get; set; } = 0;
        public int Height { get; set; } = 0;
    }
}