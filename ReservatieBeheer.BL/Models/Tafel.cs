using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Models
{
    public class Tafel
    {
        public int TafelNummer { get; set; }
        
        public int Aantal { get; set; }

        //Constructors
        public Tafel(int tafelNummer, int aantal)
        {
            TafelNummer = tafelNummer;
            Aantal = aantal;
        }
    }
}
