﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Interfaces
{
    public interface IDbContextFactory
    {
        ReservatieBeheerContext CreateDbContext();
    }
}
