﻿using Microsoft.EntityFrameworkCore;
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
    public class ReservatieRepo : IReservatieRepo
    {
        private readonly IDbContextFactory<ReservatieBeheerContext> _dbContextFactory;

        public ReservatieRepo(IDbContextFactory<ReservatieBeheerContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void MaakReservatie(Reservatie reservatie)
        {
            try
            {
                using (var _context = _dbContextFactory.CreateDbContext())
                {
                    _context.Reservaties.Add(ReservatieMapper.MapToEfEntity(reservatie));
                    _context.SaveChanges();
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Klant met opgegeven ID bestaat niet.", ex);
            }
        }

        public bool IsTafelVrij(int tafelNummer, DateTime beginTijd, DateTime eindTijd)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                // Voer een query uit om te controleren of er reservaties zijn die overlappen
                // met de opgegeven tijden voor de specifieke tafel
                // Dit is een voorbeeldquery en moet worden aangepast aan je database schema

                return !_context.Reservaties.Any(r =>
                r.TafelNummer == tafelNummer &&
                ((r.Datum >= beginTijd && r.Datum < eindTijd) ||
                (r.Datum.AddHours(1.5) > beginTijd && r.Datum < eindTijd)));
            }
                
        }

        public bool PasReservatieAan(int reservatieId, DateTime nieuweDatum, int nieuwAantalPlaatsen)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var reservatie = _context.Reservaties.FirstOrDefault(r => r.ID == reservatieId);
                if (reservatie == null)
                {
                    // Reservatie niet gevonden
                    return false;
                }

                var eindTijd = nieuweDatum.AddHours(1.5);

                // Controleer of de tafel beschikbaar is op de nieuwe datum en tijd
                if (!_context.Reservaties.Any(r =>
                    r.TafelNummer == reservatie.TafelNummer &&
                    r.ID != reservatieId &&
                    ((r.Datum >= nieuweDatum && r.Datum < eindTijd) ||
                     (r.Datum.AddHours(1.5) > nieuweDatum && r.Datum < eindTijd))))
                {
                    // Update de reservatie
                    reservatie.Datum = nieuweDatum;
                    reservatie.AantalPlaatsen = nieuwAantalPlaatsen;

                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    // Tafel niet beschikbaar op de nieuwe datum en tijd
                    return false;
                }
            }
        }

        public bool AnnuleerReservatie(int reservatieId)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var reservatie = _context.Reservaties.FirstOrDefault(r => r.ID == reservatieId);
                if (reservatie == null || reservatie.Datum <= DateTime.Now)
                {
                    // Reservatie niet gevonden of is al verstreken
                    return false;
                }

                // Annuleer de reservatie (dit kan verwijderen of een statuswijziging zijn)
                _context.Reservaties.Remove(reservatie);
                _context.SaveChanges();

                return true;
            }
        }

        public IEnumerable<Reservatie> ZoekReservaties(int klantId, DateTime? beginDatum, DateTime? eindDatum)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var query = _context.Reservaties.AsQueryable();

                if (beginDatum.HasValue)
                {
                    query = query.Where(r => r.Datum >= beginDatum.Value);
                }

                if (eindDatum.HasValue)
                {
                    query = query.Where(r => r.Datum <= eindDatum.Value);
                }

                var reservatieEFs = query.Where(r => r.KlantID == klantId).ToList();

                // Map each ReservatieEF to Reservatie using the mapper
                return reservatieEFs.Select(r => ReservatieMapper.MapToBLModel(r)).ToList();
            }
        }


        public IEnumerable<Reservatie> ZoekReservatiesPerRestaurant(int restaurantId, DateTime? beginDatum, DateTime? eindDatum)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var query = _context.Reservaties.AsQueryable();

                query = query.Where(r => r.Tafel.RestaurantID == restaurantId);

                if (beginDatum.HasValue)
                {
                    query = query.Where(r => r.Datum >= beginDatum.Value);
                }

                if (eindDatum.HasValue)
                {
                    query = query.Where(r => r.Datum <= eindDatum.Value);
                }

                var reservatieEFs = query.ToList();

                // Map each ReservatieEF to Reservatie using the mapper
                return reservatieEFs.Select(r => ReservatieMapper.MapToBLModel(r)).ToList();
            }
        }
    }
}
