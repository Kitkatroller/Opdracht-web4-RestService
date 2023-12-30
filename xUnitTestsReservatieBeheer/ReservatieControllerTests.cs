using Microsoft.AspNetCore.Mvc;
using Moq;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Services;
using ReservatieBeheer.Gebruiker.API.Controllers;
using ReservatieBeheer.Gebruiker.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTestsReservatieBeheer
{
    public class ReservatieControllerTests
    {
        private readonly Mock<IReservatieRepo> mockRepo;
        private readonly ReservatieService _reservatieService;
        private readonly ReservatieController _controller;

        public ReservatieControllerTests() 
        {
            mockRepo = new Mock<IReservatieRepo>();
            _reservatieService = new ReservatieService(mockRepo.Object);
            _controller = new ReservatieController(_reservatieService);

        }
    }
}
