using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Share.Models
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        public int Cap { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Required")]
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
    }
}

