using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Mappers
{    
    public class TafelMapper
    {
        public static Tafel MapToBLModel(TafelEF efTafelEntry)
        {
            if (efTafelEntry == null) return null;

            return new Tafel(efTafelEntry.TafelNummer, efTafelEntry.Aantal);
        }

        public static TafelEF MapToEfEntity(Tafel TafelEntry)
        {
            if (TafelEntry == null) return null;

            return new TafelEF(TafelEntry.TafelNummer, TafelEntry.Aantal);
        }
    }
}
