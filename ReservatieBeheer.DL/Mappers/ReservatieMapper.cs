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



            return new Reservatie();
        }

        public static ReservatieEF MapToEfEntity(Reservatie ReservatieEntry)
        {
            if (ReservatieEntry == null) return null;


            return new ReservatieEF();
        }
    }
}
