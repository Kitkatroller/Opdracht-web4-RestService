using Microsoft.EntityFrameworkCore;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using ReservatieBeheer.DL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Repositories
{
    public class RestaurantRepo : IRestaurantRepo
    {
        private readonly ReservatieBeheerContext _context;

        public RestaurantRepo(string connectionString)
        {
            var options = new DbContextOptionsBuilder<ReservatieBeheerContext>()
                              .UseSqlServer(connectionString)
                              .Options;
            _context = new ReservatieBeheerContext(options);
        }


        public void VoegTafelToe(Tafel tafel)
        {
            try
            {
                _context.Tafels.Add(TafelMapper.MapToEfEntity(tafel));
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Geef huis");
            }
        }
    }
}
