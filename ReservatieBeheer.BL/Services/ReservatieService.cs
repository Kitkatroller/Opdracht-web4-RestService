using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Services
{
    public class ReservatieService
    {
        private readonly IReservatieRepo _reservatieRepo;

        public ReservatieService(IReservatieRepo reservatieRepo)
        {
            _reservatieRepo = reservatieRepo;
        }

        public void MaakReservatie(int klantId, int aantalPlaatsen, DateTime datum, int tafelNummer )
        {
            // Controleer of de datum een exact uur of half uur is
            if (datum.Minute != 0 && datum.Minute != 30)
            {
                throw new ArgumentException("Reservatietijdstip moet een exact uur of een half uur zijn.");
            }

            // Bereken het eindtijdstip van de reservatie (1.5 uur na het begin)
            var eindTijd = datum.AddHours(1.5);

            // Controleer de beschikbaarheid van de tafel
            if (!IsTafelBeschikbaar(tafelNummer, datum, eindTijd))
            {
                throw new InvalidOperationException("Tafel is niet beschikbaar voor de opgegeven tijd.");
            }

            try
            {
                var nieuweReservatie = new Reservatie
                {
                    KlantId = klantId,
                    AantalPlaatsen = aantalPlaatsen,
                    Datum = datum,
                    TafelId = tafelNummer
                };

                _reservatieRepo.MaakReservatie(nieuweReservatie);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Klant met opgegeven ID bestaat niet.", ex);
            }
        }
        private bool IsTafelBeschikbaar(int tafelNummer, DateTime beginTijd, DateTime eindTijd)
        {
            return _reservatieRepo.IsTafelVrij(tafelNummer, beginTijd, eindTijd);
        }
        public bool PasReservatieAan(int reservatieId, DateTime nieuweDatum, int nieuwAantalPlaatsen)
        {
            return _reservatieRepo.PasReservatieAan(reservatieId, nieuweDatum, nieuwAantalPlaatsen);
        }

        public bool AnnuleerReservatie(int reservatieId)
        {
            return _reservatieRepo.AnnuleerReservatie(reservatieId);
        }

        public IEnumerable<Reservatie> ZoekReservaties(int klantId, DateTime? beginDatum, DateTime? eindDatum)
        {
            return _reservatieRepo.ZoekReservaties(klantId, beginDatum, eindDatum);
        }
        public IEnumerable<Reservatie> ZoekReservatiesPerRestaurant(int restaurantId, DateTime? beginDatum, DateTime? eindDatum)
        {
            return _reservatieRepo.ZoekReservatiesPerRestaurant(restaurantId, beginDatum, eindDatum);
        }


    }

}
