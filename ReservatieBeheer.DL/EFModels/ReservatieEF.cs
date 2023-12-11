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
    public class ReservatieEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(1, int.MaxValue, ErrorMessage = "Reservatienummer moet groter dan 0 zijn")]
        public int ID { get; set; }


        public int RestaurantID { get; set; }
        [ForeignKey("RestaurantID")]
        public RestaurantEF Restaurant { get; set; }

        public int KlantID { get; set; }
        [ForeignKey("KlantID")]
        public KlantEF Klant { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Aantal plaatsen moet minstens 1 zijn")]
        public int AantalPlaatsen { get; set; }

        [Required]
        public DateTime Datum { get; set; }

        [Required]
        public int Uur { get; set; }

        public int TafelNummer { get; set; }
        [ForeignKey("TafelNummer")]
        public TafelEF Tafel { get; set; }
    }
}
