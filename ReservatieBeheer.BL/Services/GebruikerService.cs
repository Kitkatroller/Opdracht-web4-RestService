using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Services
{
    public class GebruikerService
    {
        IGebruikerRepo _gebruikerRepo;
        string Connectionstring;

        public GebruikerService(string connectionstring) { Connectionstring = connectionstring; }

        public void GebruikerRegistreren(string naam,
            string email,
            string telefoonNummer,
            Locatie locatie,
            ICollection<Reservatie> reservaties)

        {
            _gebruikerRepo.VoegGebruikerToe(new Klant(naam, email, telefoonNummer, locatie, reservaties));
        }
    }
}
