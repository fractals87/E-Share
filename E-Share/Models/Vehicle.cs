using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "Required")]
        public string Code { get; set; }

        [Display(Name = "Latitude")]
        [Required(ErrorMessage = "Required")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Required(ErrorMessage = "Required")]
        public double Longitude { get; set; }

        [Display(Name = "Battery residue")]
        [Required(ErrorMessage = "Required")]
        public int BatteryResidue { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Required")]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Required")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Required")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
    }

    public class Status
    {
        public int Id { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }

        [Display(Name = "Euro/min")]
        [Required(ErrorMessage = "Required")]
        public double Cost { get; set; }

        //[Display(Name = "End service")]
        //[DataType(DataType.Time)]
        //public TimeSpan EndService { get; set; }

        //[Display(Name = "Start service")]
        //[DataType(DataType.Time)]
        //public TimeSpan StartService { get; set; }

        public ICollection<Avalailable> Avalailable { get; set; }
    }

    public class Avalailable
    {
        public int Id { get; set; }

        [Display(Name = "Day of week")]
        public DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "End service")]
        [DataType(DataType.Time)]
        public TimeSpan EndService { get; set; }

        [Display(Name = "Start service")]
        [DataType(DataType.Time)]
        public TimeSpan StartService { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Required")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Required")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
