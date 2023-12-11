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



            return new Locatie();
        }

        public static LocatieEF MapToEfEntity(Locatie LocatieEntry)
        {
            if (LocatieEntry == null) return null;


            return new LocatieEF();
        }
    }
}
