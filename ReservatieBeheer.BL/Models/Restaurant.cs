using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Models
{
    public class Restaurant
    {
        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public Locatie Locatie { get; set; }
        public int KlantenNummer { get; set; }
    }
}
