
using ReservatieBeheer.DL;
using ReservatieBeheer.DL.EFModels;
using ReservatieBeheer.DL.Repositories;

namespace ReservatieBeheer.Beheerder.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = @"Data Source=RAZER-LAPTOP-EP\SQLEXPRESS;Initial Catalog=ReservatieBeheer;Integrated Security=True;TrustServerCertificate=True";

            //Database opzetten
            ReservatieBeheerContext ctx = new ReservatieBeheerContext(connectionString);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}