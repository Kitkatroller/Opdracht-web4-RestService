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
            // Logica om de beschikbaarheid van de tafel te controleren

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

            // Creëer een nieuwe Reservatie en vul de details in
            var nieuweReservatie = new Reservatie
            {
                KlantId = klantId,
                AantalPlaatsen = aantalPlaatsen,
                Datum = datum,
                TafelId = tafelNummer
            };

            _reservatieRepo.MaakReservatie(nieuweReservatie);
        }
        private bool IsTafelBeschikbaar(int tafelNummer, DateTime beginTijd, DateTime eindTijd)
        {
            // Implementeer logica om te controleren of de tafel beschikbaar is
            // Dit kan betekenen dat je moet controleren in je databank of er al reservaties zijn
            // voor de opgegeven tafel tussen de beginTijd en eindTijd
            return _reservatieRepo.IsTafelVrij(tafelNummer, beginTijd, eindTijd);
        }
    }

}
