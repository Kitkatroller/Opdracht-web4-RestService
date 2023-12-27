using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Models
{
    public class Reservatie
    {
        public int ID { get; set; }
                        
        public int KlantId{ get; set; }

        public Klant ContactPersoon { get; set; }
                
        public int AantalPlaatsen { get; set; }

        public DateTime Datum { get; set; }

        public int TafelId { get; set; }
        public Tafel Tafel { get; set; }
    }
}
