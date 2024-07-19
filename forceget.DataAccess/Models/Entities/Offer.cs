using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace forceget.DataAccess.Models.Entities
{
    public class Offer
    {
        [Key]
        public long Id { get; set; }
        public string Mode { get; set; }
        public string MovementType { get; set; }
        public string Incoterms { get; set; }
        public string CountriesCities { get; set; }
        public string PackageType { get; set; }
        public string Unit1 { get; set; }
        public string Unit2 { get; set; }
        public string Currency { get; set; }
    }
}