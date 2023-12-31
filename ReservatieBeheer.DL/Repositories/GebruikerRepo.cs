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
        private readonly IDbContextFactory<ReservatieBeheerContext> _dbContextFactory;

        public GebruikerRepo(IDbContextFactory<ReservatieBeheerContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void VoegGebruikerToe(Klant klant)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                try
                {
                    KlantEF efKlant = KlantMapper.MapToEfEntity(klant);
                    _context.Locaties.Add(efKlant.Locatie);
                    _context.SaveChanges();

                    efKlant.LocatieID = klant.Locatie.ID;

                    _context.Klanten.Add(efKlant);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Fout bij het toevoegen van de gebruiker: " + ex.Message);
                }
            }
        }

        public Klant GetKlantById(int klantenNummer)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var klantEF = _context.Klanten
                    .Include(k => k.Locatie)
                    .FirstOrDefault(k => k.KlantenNummer == klantenNummer && !k.IsUitgeschreven);

                // Controleer of de klant bestaat in de database
                if (klantEF == null)
                {
                    // Geen klant gevonden, retourneer null
                    return null;
                }

                // Klant gevonden, retourneer de gemapte klant
                return KlantMapper.MapToBLModel(klantEF);
            }
        }

        public void UpdateKlant(Klant klant)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var klantEF = KlantMapper.MapToEfEntity(klant);

                var bestaandeKlant = _context.Klanten
                    .Include(k => k.Locatie)
                    .FirstOrDefault(k => k.KlantenNummer == klantEF.KlantenNummer);

                if (bestaandeKlant != null)
                {
                    _context.Entry(bestaandeKlant).CurrentValues.SetValues(klantEF);

                    if (klantEF.Locatie != null && bestaandeKlant.Locatie != null)
                    {
                        _context.Entry(bestaandeKlant.Locatie).CurrentValues.SetValues(klantEF.Locatie);
                    }

                    _context.SaveChanges();
                }
            }
        }

        public void UitschrijvenGebruiker(int klantenNummer)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
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
}
