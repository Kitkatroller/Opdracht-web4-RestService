using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.EFModels
{
    public class TafelEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TafelNummer { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Aantal plaatsen moet minstens 1 zijn")]
        public int Aantal { get; set; }

        //Constructor
        public TafelEF(int tafelNummer, int aantal)
        { 
            Aantal = aantal;
        }
    }
}
