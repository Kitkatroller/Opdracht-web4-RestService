﻿using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Interfaces
{
    public interface IGebruikerRepo
    {
        public void VoegGebruikerToe(Klant klant);
        Klant GetKlantById(int klantenNummer);
        void UpdateKlant(Klant klant);
        void UitschrijvenGebruiker(int klantenNummer);
    }
}
