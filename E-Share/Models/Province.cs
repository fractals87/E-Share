using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Share.Models
{
    public class Province
    {
        public int Id { get; set; }

        [Display(Name = "Prov")]
        [Required(ErrorMessage = "Required")]
        public string Abbreviation { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
    }
}
