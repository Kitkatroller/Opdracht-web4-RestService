using Moq;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.Gebruiker.API.Controllers;
using ReservatieBeheer.Gebruiker.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;
using ReservatieBeheer.BL.Services;
using Microsoft.Extensions.Logging;
using Moq;


namespace xUnitTestsReservatieBeheer
{
    public class RestaurantControllerTests
    {
        private readonly Mock<IRestaurantRepo> mockRepo;
        private readonly RestaurantService _restaurantService;
        private readonly RestaurantController _controller;
        private readonly Mock<ILogger<RestaurantController>> mockLogger;

        public RestaurantControllerTests()
        {
            mockRepo = new Mock<IRestaurantRepo>();
            mockLogger = new Mock<ILogger<RestaurantController>>();
            _restaurantService = new RestaurantService(mockRepo.Object);
            _controller = new RestaurantController(_restaurantService, mockLogger.Object);
        }

        [Fact]
        public void ZoekRestaurants_SuccessfulSearch_ReturnsOkWithRestaurants()
        {
            // Arrange
            string postcode = "1000";
            string keuken = "Italian";
            var mockRestaurants = new List<Restaurant>
    {
        new Restaurant
        {
            ID = 1,
            Naam = "Test Restaurant 1",
            Keuken = "Italian",
            Telefoon = "0123456789",
            Email = "info@test1.com",
            Locatie = new Locatie
            {
                ID = 1,
                Postcode = "1000",
                Gemeente = "Gemeente1",
                Straatnaam = "Straat 1",
                Huisnummerlabel = "1"
            }
        },
        new Restaurant
        {
            ID = 2,
            Naam = "Test Restaurant 2",
            Keuken = "Italian",
            Telefoon = "9876543210",
            Email = "info@test2.com",
            Locatie = new Locatie
            {
                ID = 2,
                Postcode = "1000",
                Gemeente = "Gemeente2",
                Straatnaam = "Straat 2",
                Huisnummerlabel = "2"
            }
        }
    };

            mockRepo.Setup(repo => repo.ZoekRestaurants(postcode, keuken))
                    .Returns(mockRestaurants);

            // Act
            var result = _controller.ZoekRestaurants(postcode, keuken);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDtos = Assert.IsAssignableFrom<IEnumerable<RestaurantDto>>(okResult.Value);
            Assert.NotEmpty(returnedDtos);
            Assert.Equal(mockRestaurants.Count, returnedDtos.Count());
        }



    }
}
