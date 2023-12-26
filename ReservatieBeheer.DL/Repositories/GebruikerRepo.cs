using Microsoft.EntityFrameworkCore;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using ReservatieBeheer.DL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Repositories
{
    public class GebruikerRepo : IGebruikerRepo
    {
        private readonly ReservatieBeheerContext _context;

        // Constructor met dependency injection voor de DbContext
        public GebruikerRepo(ReservatieBeheerContext context)
        {
            _context = context;
        }

        public void VoegGebruikerToe(Klant klant)
        {
            try
            {
                KlantEF efKlant = KlantMapper.MapToEfEntity(klant);
                _context.Locaties.Add(efKlant.Locatie);
                _context.SaveChanges();

                // Update de locatie ID op de klant
                efKlant.LocatieID = klant.Locatie.ID;

                _context.Klanten.Add(efKlant);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Overweeg een specifiekere foutafhandeling of log de fout
                throw new Exception("Fout bij het toevoegen van de gebruiker: " + ex.Message);
            }
        }

        public Klant GetKlantById(int klantenNummer)
        {
            // Zoek de klant op basis van ID
            return KlantMapper.MapToBLModel(_context.Klanten.FirstOrDefault(k => k.KlantenNummer == klantenNummer && !k.IsUitgeschreven));
        }

        public void UpdateKlant(Klant klant)
        {
            // Update de klant in de context
            _context.Klanten.Update(KlantMapper.MapToEfEntity(klant));
            _context.SaveChanges();
        }

        public void UitschrijvenGebruiker(int klantenNummer)
        {
            var klant = _context.Klanten.Find(klantenNummer);
            if (klant != null)
            {
                klant.IsUitgeschreven = true;
                _context.SaveChanges();
            }
        }
    }
}
