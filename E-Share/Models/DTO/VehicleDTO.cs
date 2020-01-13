using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.Models.DTO
{
    public class VehicleDTO
    {
        public string code { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public int batteryresidue { get; set; }

        public string category { get; set; }
    }
}
