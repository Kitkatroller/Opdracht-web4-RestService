using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Models
{
    public class Locatie
    {
        public int ID { get; set; }

        public string Postcode { get; set; }

        public string Gemeente { get; set; }

        public string Straatnaam { get; set; }

        public string Huisnummerlabel { get; set; }
    }
}
