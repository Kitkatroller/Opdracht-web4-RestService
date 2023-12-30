using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Models
{
    public class Klant
    {
        public int KlantenNummer { get; set; }

        public string Naam { get; set; }

        public string Email { get; set; }

        public string TelefoonNummer { get; set; }

        public Locatie Locatie { get; set; }

        public ICollection<Reservatie> Reservaties { get; set; }
        public Klant()
        {
            Reservaties = new List<Reservatie>();
        }
        public Klant(string naam, string email, string telefoonNummer, Locatie locatie)
            : this()
        {
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Locatie = locatie;
        }
    }
}
