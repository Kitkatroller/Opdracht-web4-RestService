using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.EFModels
{
    public class LocatieEF
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Postcode must be 4 digits")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Gemeentenaam is required")]
        public string Gemeente { get; set; }

        public string Straatnaam { get; set; }

        public string Huisnummerlabel { get; set; }
    }
}
