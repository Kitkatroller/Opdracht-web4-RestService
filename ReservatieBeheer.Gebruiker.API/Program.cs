using Microsoft.EntityFrameworkCore;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.DL.Repositories;
using ReservatieBeheer.DL;
using Microsoft.OpenApi.Models;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.BL.Services;

namespace ReservatieBeheer.Gebruiker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IDbContextFactory<ReservatieBeheerContext>>(serviceProvider =>
            {
                var options = new DbContextOptionsBuilder<ReservatieBeheerContext>()
                    .UseSqlServer(builder.Configuration.GetConnectionString("ReservatieBeheerDatabase"))
                    .Options;

                return new DbContextFactory(options);
            });

            builder.Services.AddScoped<IGebruikerRepo, GebruikerRepo>();
            builder.Services.AddScoped<GebruikerService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                //// Ensure the database is deleted and created
                //using (var scope = app.Services.CreateScope())
                //{
                //    var services = scope.ServiceProvider;
                //    var dbContext = services.GetRequiredService<ReservatieBeheerContext>();
                //    dbContext.Database.EnsureDeleted();
                //    dbContext.Database.EnsureCreated();
                //}
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
