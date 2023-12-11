using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Mappers
{
    public class KlantMapper
    {
        public static Klant MapToBLModel(KlantEF efKlantEntry)
        {
            if (efKlantEntry == null) return null;

            ICollection<Reservatie> reservaties = null;

            foreach (var item in efKlantEntry.Reservaties)
            {
                reservaties.Add(ReservatieMapper.MapToBLModel(item));
            }


            return new Klant(efKlantEntry.Naam,
                efKlantEntry.Email,
                efKlantEntry.TelefoonNummer,
                LocatieMapper.MapToBLModel( efKlantEntry.Locatie),
                reservaties);
        }

        public static KlantEF MapToEfEntity(Klant KlantEntry)
        {
            if (KlantEntry == null) return null;

            ICollection<ReservatieEF> reservaties = null;

            foreach (var item in KlantEntry.Reservaties)
            {
                reservaties.Add(ReservatieMapper.MapToEfEntity(item));
            }

            return new KlantEF(KlantEntry.Naam,
                 KlantEntry.Email,
                 KlantEntry.TelefoonNummer,
                 LocatieMapper.MapToEfEntity(KlantEntry.Locatie),
                 reservaties);
        }
    }
}
