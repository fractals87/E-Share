using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.Models
{
    public class Ride
    {
        public int Id { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "Required")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Vehicle")]
        [Required(ErrorMessage = "Required")]
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Date start")]
        public DateTime DateStart { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Date stop")]
        public DateTime DateStop { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public Double Price { get; set; }

        [NotMapped]
        [DataType(DataType.Currency)]
        public Double CalcPrice
        {
            get
            {
                return Vehicle.Category.Cost * ((int)(DateTime.Now - DateStart).TotalMinutes + ((DateTime.Now - DateStart).Seconds >= 30 ? 1 : 0));
            }
        }
    }
}
