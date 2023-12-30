using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;

namespace ReservatieBeheer.BL.Services
{
    public class GebruikerService
    {
        private readonly IGebruikerRepo _gebruikerRepo;
        public GebruikerService(IGebruikerRepo gebruikerRepo)
        {
            _gebruikerRepo = gebruikerRepo;
        }

        public void GebruikerRegistreren(string naam, string email, string telefoonNummer, Locatie locatie)
        {
            var klant = new Klant
            {
                Naam = naam,
                Email = email,
                TelefoonNummer = telefoonNummer,
                Locatie = locatie
            };

            _gebruikerRepo.VoegGebruikerToe(klant);
        }
        public void UpdateGebruiker(int klantenNummer, string naam, string email, string telefoonNummer, Locatie locatie)
        {
            var klant = _gebruikerRepo.GetKlantById(klantenNummer);
            if (klant == null)
            {
                throw new KeyNotFoundException("Klant niet gevonden");
            }

            locatie.ID = klant.Locatie.ID;

            klant.KlantenNummer = klantenNummer;
            klant.Naam = naam;
            klant.Email = email;
            klant.TelefoonNummer = telefoonNummer;
            klant.Locatie = locatie;

            _gebruikerRepo.UpdateKlant(klant);
        }
        public void UitschrijvenGebruiker(int klantenNummer)
        {
            if (_gebruikerRepo.GetKlantById(klantenNummer) == null)
            {
                throw new KeyNotFoundException("Klant niet gevonden");
            }
            else { _gebruikerRepo.UitschrijvenGebruiker(klantenNummer); }
            
        }

    }
}
