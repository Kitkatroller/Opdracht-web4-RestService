using ReservatieBeheer.BL.Exceptions;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (datum.Minute != 0 && datum.Minute != 30)
            {
                throw ExceptionFactory.CreateInvalidReservationTimeException("Reservatietijdstip moet een exact uur of een half uur zijn.");
            }

            if (!IsTafelBeschikbaar(tafelNummer, datum, datum.AddHours(1.5)))
            {
                throw ExceptionFactory.CreateTableNotAvailableException("Tafel is niet beschikbaar voor de opgegeven tijd.");
            }

            if (!_reservatieRepo.DoesKlantExist(klantId))
            {
                throw ExceptionFactory.CreateCustomerNotFoundException(klantId);
            }

            if (!_reservatieRepo.DoesTafelExist(tafelNummer))
            {
                throw ExceptionFactory.CreateTableNotFoundException(tafelNummer);
            }

            if (datum < DateTime.Now)
            {
                throw ExceptionFactory.CreateInvalidNewDateException(datum);
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
            if (nieuweDatum.Minute != 0 && nieuweDatum.Minute != 30)
            {
                throw ExceptionFactory.CreateInvalidReservationTimeException("Reservatietijdstip moet een exact uur of een half uur zijn.");
            }

            if (!_reservatieRepo.DoesReservationExist(reservatieId))
            {
                throw ExceptionFactory.CreateReservationNotFoundException(reservatieId);
            }


            int tafelNummer = _reservatieRepo.TafelNummerFromReservatie(reservatieId);
            if (!IsTafelBeschikbaar(tafelNummer, nieuweDatum, nieuweDatum.AddHours(1.5)))
            {
                throw ExceptionFactory.CreateTableNotAvailableException("Tafel is niet beschikbaar voor de opgegeven tijd.");
            }

            if (nieuweDatum < DateTime.Now)
            {
                throw ExceptionFactory.CreateInvalidNewDateException(nieuweDatum);
            }

            if (nieuwAantalPlaatsen < 1)
            {
                throw ExceptionFactory.CreateInvalidNumberOfPlacesException(nieuwAantalPlaatsen);
            }

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
