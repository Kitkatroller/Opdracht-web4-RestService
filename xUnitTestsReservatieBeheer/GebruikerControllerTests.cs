using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.BL.Services;
using ReservatieBeheer.Gebruiker.API.Controllers;
using ReservatieBeheer.Gebruiker.API.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xUnitTestsReservatieBeheer
{
    public class GebruikerControllerTests
    {
        private readonly Mock<IGebruikerRepo> mockRepo;
        private readonly GebruikerService _gebruikerService;
        private readonly GebruikerController _controller;
        private readonly GebruikerDto _dummyGebruikerDto;
        private readonly GebruikerDto _dummyGebruikerInvalidDto;
        private readonly Mock<ILogger<GebruikerController>> mockLogger;

        public GebruikerControllerTests()
        {
            mockRepo = new Mock<IGebruikerRepo>();
            _gebruikerService = new GebruikerService(mockRepo.Object);
            _controller = new GebruikerController(_gebruikerService, mockLogger.Object);

            // Test data
            _dummyGebruikerDto = new GebruikerDto
            {
                Naam = "Jane Doe",
                Email = "jan@example.com",
                TelefoonNummer = "0123456789",
                Locatie = new LocatieDto
                {
                    Gemeente = "Amsterdam",
                    Huisnummerlabel = "123A",
                    Postcode = "1000",
                    Straatnaam = "Hoofdstraat"
                }
            };
            _dummyGebruikerInvalidDto = new GebruikerDto
            {
                Naam = "Jane Doe",
                Email = "jaaaaaom",
                TelefoonNummer = "aaaaa"
            };


        }

        // Test methods
        [Fact]
        public void RegistreerGebruiker_WithValidModel_ReturnsOk()
        {
            // Arrange
            _controller.ModelState.Clear();

            // Act
            var result = _controller.RegistreerGebruiker(_dummyGebruikerDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RegistreerGebruiker_WithInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var validationContext = new ValidationContext(_dummyGebruikerInvalidDto);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_dummyGebruikerInvalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            // Act
            var result = _controller.RegistreerGebruiker(_dummyGebruikerInvalidDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateGebruiker_WithValidModel_ReturnsOk()
        {
            // Arrange
            int klantenNummer = 1;
            _controller.ModelState.Clear();

            // Mock setup for an existing user
            var mockKlant = new Klant
            {
                KlantenNummer = klantenNummer,
                Naam = "Existing User",
                Email = "existing@example.com",
                TelefoonNummer = "0123456789",
                Locatie = new Locatie() // Initialize Locatie fields if needed
            };

            mockRepo.Setup(repo => repo.GetKlantById(klantenNummer))
                    .Returns(mockKlant);
            mockRepo.Setup(repo => repo.UpdateKlant(It.IsAny<Klant>()))
                    .Verifiable(); // Setup for UpdateKlant call

            // Act
            var result = _controller.UpdateGebruiker(klantenNummer, _dummyGebruikerDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockRepo.Verify(repo => repo.UpdateKlant(It.IsAny<Klant>()), Times.Once());
        }


        [Fact]
        public void UpdateGebruiker_WithInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            int klantenNummer = 1;
            var validationContext = new ValidationContext(_dummyGebruikerInvalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_dummyGebruikerInvalidDto, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            // Act
            var result = _controller.UpdateGebruiker(klantenNummer, _dummyGebruikerInvalidDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateGebruiker_WithNonExistingKlant_ReturnsNotFound()
        {
            // Arrange
            int klantenNummer = -1; // Non-existing klantenNummer
            _controller.ModelState.Clear();

            // Act
            var result = _controller.UpdateGebruiker(klantenNummer, _dummyGebruikerDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void UitschrijvenGebruiker_ExistingKlant_ReturnsOk()
        {
            // Arrange
            int klantenNummer = 1;

            // Mock setup for existing user
            var mockKlant = new Klant("Test Naam", "test@example.com", "0123456789", new Locatie { /* Initialize Locatie fields if needed */ });
            mockRepo.Setup(repo => repo.GetKlantById(klantenNummer))
                    .Returns(mockKlant);
            mockRepo.Setup(repo => repo.UitschrijvenGebruiker(klantenNummer))
                    .Verifiable();

            // Act
            var result = _controller.UitschrijvenGebruiker(klantenNummer);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockRepo.Verify(repo => repo.UitschrijvenGebruiker(klantenNummer), Times.Once());
        }



        [Fact]
        public void UitschrijvenGebruiker_NonExistingKlant_ReturnsNotFound()
        {
            // Arrange
            int klantenNummer = -1; // Non-existing klantenNummer

            // Mock setup for non-existing user
            mockRepo.Setup(repo => repo.GetKlantById(klantenNummer))
                    .Returns<Klant>(null); // Simulating user not found

            // Act
            var result = _controller.UitschrijvenGebruiker(klantenNummer);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }



    }

}
