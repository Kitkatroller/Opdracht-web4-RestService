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
        public void VoegRestaurantToe_WithValidData_ReturnsOk()
        {
            // Arrange
            var validRestaurantDto = new RestaurantDto
            {
                Naam = "Test Restaurant",
                // Vul andere vereiste velden in
            };

            // Act
            var result = _controller.VoegRestaurantToe(validRestaurantDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateRestaurant_ValidRestaurant_ReturnsOk()
        {
            // Arrange
            var validRestaurantDto = new RestaurantDto
            {
                Naam = "Test Restaurant",
                // Vul andere vereiste velden in
            };
            int restaurantId = 1;

            // Mock setup
            mockRepo.Setup(r => r.DoesRestaurantExist(restaurantId)).Returns(true);

            // Act
            var result = _controller.UpdateRestaurant(restaurantId, validRestaurantDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void VerwijderRestaurant_ExistingRestaurant_ReturnsOk()
        {
            // Arrange
            int restaurantId = 1;
            mockRepo.Setup(r => r.DoesRestaurantExist(restaurantId)).Returns(true);

            // Act
            var result = _controller.VerwijderRestaurant(restaurantId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void VerwijderRestaurant_NonExistingRestaurant_ReturnsNotFound()
        {
            // Arrange
            int restaurantId = -1;
            mockRepo.Setup(r => r.DoesRestaurantExist(restaurantId)).Returns(false);

            // Act
            var result = _controller.VerwijderRestaurant(restaurantId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
