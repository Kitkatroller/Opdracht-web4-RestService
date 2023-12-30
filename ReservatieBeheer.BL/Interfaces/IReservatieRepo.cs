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
        bool AnnuleerReservatie(int reservatieId);
        IEnumerable<Reservatie> ZoekReservaties(int klantId, DateTime? beginDatum, DateTime? eindDatum);
        IEnumerable<Reservatie> ZoekReservatiesPerRestaurant(int restaurantId, DateTime? beginDatum, DateTime? eindDatum);
        bool DoesKlantExist(int klantenNummer);
        bool DoesTafelExist(int tafelNummer);
        bool DoesReservationExist(int reservatieId);
        int TafelNummerFromReservatie(int reservatieId);
    }
}
