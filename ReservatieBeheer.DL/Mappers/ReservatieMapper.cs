using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Mappers
{
    public class ReservatieMapper
    {
        public static Reservatie MapToBLModel(ReservatieEF efReservatieEntry)
        {
            if (efReservatieEntry == null) return null;

            var blReservatie = new Reservatie
            {
                ID = efReservatieEntry.ID,
                ContactPersoon = KlantMapper.MapToBLModel(efReservatieEntry.Klant),
                AantalPlaatsen = efReservatieEntry.AantalPlaatsen,
                Datum = efReservatieEntry.Datum,
                Tafel = TafelMapper.MapToBLModel(efReservatieEntry.Tafel)
            };

            return blReservatie;
        }

        public static ReservatieEF MapToEfEntity(Reservatie reservatieEntry)
        {
            if (reservatieEntry == null) return null;

            var efReservatie = new ReservatieEF
            {
                ID = reservatieEntry.ID,
                KlantID = reservatieEntry.KlantId,
                AantalPlaatsen = reservatieEntry.AantalPlaatsen,
                Datum = reservatieEntry.Datum,
                TafelNummer = reservatieEntry.TafelId
            };

            return efReservatie;
        }
    }
}
