using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Interfaces
{
    public interface IReservatieRepo
    {
        void MaakReservatie(Reservatie reservatie);
        bool IsTafelVrij(int tafelNummer, DateTime beginTijd, DateTime eindTijd);
        bool PasReservatieAan(int reservatieId, DateTime nieuweDatum, int nieuwAantalPlaatsen);
    }
}
