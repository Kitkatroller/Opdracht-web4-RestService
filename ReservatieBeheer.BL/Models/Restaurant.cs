using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Naam { get; set; }

        public int LocatieID { get; set; }
        public Locatie Locatie { get; set; }

        public string Keuken { get; set; }

        public string Telefoon { get; set; }

        public string Email { get; set; }
    }
}
