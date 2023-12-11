using Microsoft.EntityFrameworkCore;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Repositories
{
    public class GebruikerRepo : IGebruikerRepo
    {
        private readonly ReservatieBeheerContext _context;

        public GebruikerRepo(string connectionString)
        {
            var options = new DbContextOptionsBuilder<ReservatieBeheerContext>()
                              .UseSqlServer(connectionString)
                              .Options;
            _context = new ReservatieBeheerContext(options);
        }

        public void VoegGebruikerToe(Klant klant)
        {
            try
            {
                _context.Klanten.Add(KlantMapper.MapToEfEntity(klant));
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Geef huis");
            }
        }
    }
}
