using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.EFModels
{
    public class RestaurantEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Naam is required")]
        public string Naam { get; set; }

        public int LocatieID { get; set; }
        [ForeignKey("LocatieID")]
        public LocatieEF Locatie { get; set; }

        [Required(ErrorMessage = "Keuken is required")]
        public string Keuken { get; set; } 

        [Required(ErrorMessage = "Telefoon is required")]
        public string Telefoon { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Geen geldig Email Address")]
        public string Email { get; set; }
    }
}
