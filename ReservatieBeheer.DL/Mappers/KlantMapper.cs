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

            var klant = new Klant
            {
                KlantenNummer = efKlantEntry.KlantenNummer,
                Naam = efKlantEntry.Naam,
                Email = efKlantEntry.Email,
                TelefoonNummer = efKlantEntry.TelefoonNummer,
                
                Locatie = LocatieMapper.MapToBLModel(efKlantEntry.Locatie),
                Reservaties = new List<Reservatie>()
            };

            if (efKlantEntry.Reservaties != null)
            {
                foreach (var item in efKlantEntry.Reservaties)
                {
                    klant.Reservaties.Add(ReservatieMapper.MapToBLModel(item));
                }
            }
            

            return klant;
        }

        public static KlantEF MapToEfEntity(Klant klantEntry)
        {
            if (klantEntry == null) return null;

            var klantEF = new KlantEF
            {
                KlantenNummer = klantEntry.KlantenNummer,
                Naam = klantEntry.Naam,
                Email = klantEntry.Email,
                TelefoonNummer = klantEntry.TelefoonNummer,
                LocatieID = klantEntry.Locatie.ID,
                Locatie = LocatieMapper.MapToEfEntity(klantEntry.Locatie),
                Reservaties = new List<ReservatieEF>()
            };

            foreach (var item in klantEntry.Reservaties)
            {
                klantEF.Reservaties.Add(ReservatieMapper.MapToEfEntity(item));
            }

            return klantEF;
        }
    }

}
