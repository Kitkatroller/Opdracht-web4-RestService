using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL
{
    public class DbContextFactory : IDbContextFactory<ReservatieBeheerContext>
    {
        private readonly DbContextOptions<ReservatieBeheerContext> _options;

        public DbContextFactory(DbContextOptions<ReservatieBeheerContext> options)
        {
            _options = options;
        }

        public ReservatieBeheerContext CreateDbContext()
        {
            return new ReservatieBeheerContext(_options);
        }
    }
}
