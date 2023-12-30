using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Mappers
{
    public class LocatieMapper
    {
        public static Locatie MapToBLModel(LocatieEF efLocatieEntry)
        {
            if (efLocatieEntry == null) return null;

            return new Locatie
            {
                ID = efLocatieEntry.ID,
                Postcode = efLocatieEntry.Postcode,
                Gemeente = efLocatieEntry.Gemeente,
                Straatnaam = efLocatieEntry.Straatnaam,
                Huisnummerlabel = efLocatieEntry.Huisnummerlabel
            };
        }

        public static LocatieEF MapToEfEntity(Locatie LocatieEntry)
        {
            if (LocatieEntry == null) return null;

            return new LocatieEF
            {
                ID = LocatieEntry.ID, 
                Postcode = LocatieEntry.Postcode,
                Gemeente = LocatieEntry.Gemeente,
                Straatnaam = LocatieEntry.Straatnaam,
                Huisnummerlabel = LocatieEntry.Huisnummerlabel
            };
        }
    }

}
