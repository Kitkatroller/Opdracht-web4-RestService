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
        private readonly GebruikerDto validGebruikerDto; 
        private readonly GebruikerDto invalidGebruikerDto;
        private readonly Mock<ILogger<GebruikerController>> mockLogger;
        private readonly Klant geldigeKlant;

        public GebruikerControllerTests()
        {
            mockRepo = new Mock<IGebruikerRepo>();
            mockLogger = new Mock<ILogger<GebruikerController>>();
            _gebruikerService = new GebruikerService(mockRepo.Object);
            _controller = new GebruikerController(_gebruikerService, mockLogger.Object);

            var validGebruikerDto = new GebruikerDto
            {
                Naam = "Jan Jansen",
                Email = "janjansen@example.com",
                TelefoonNummer = "0123456789",
                Locatie = new LocatieDto
                {
                    Gemeente = "Gent",
                    Huisnummerlabel = "12B",
                    Postcode = "9000",
                    Straatnaam = "Hoofdstraat"
                }
            };

            var invalidGebruikerDto = new GebruikerDto
            {
                Naam = "Jan Jansen",
                Email = "jan.jansen@invalid",
                TelefoonNummer = "abcde12345",
                Locatie = new LocatieDto
                {
                    Gemeente = "Gent",
                    Huisnummerlabel = "12B",
                    Postcode = "9000",
                    Straatnaam = "Hoofdstraat"
                }
            };

            _dummyGebruikerInvalidDto = new GebruikerDto
            {
                Naam = "Jan Jansen",
                Email = "jan.jansen@invalid",
                TelefoonNummer = "abcde12345",
                Locatie = new LocatieDto
                {
                    Gemeente = "Gent",
                    Huisnummerlabel = "12B",
                    Postcode = "9000",
                    Straatnaam = "Hoofdstraat"
                }
            };
        }


        [Fact]
        public void RegistreerGebruiker_WithValidKlant_ReturnsOk()
        {
            Klant geldigeKlant = new Klant(
           naam: "Jan Jansen",
           email: "jan@example.com",
           telefoonNummer: "0123456789",
           locatie: new Locatie
           {
               Gemeente = "Gent",
               Straatnaam = "Hoofdstraat",
               Huisnummerlabel = "1A",
               Postcode = "9000"
           }
            );


            // Arrange
            _controller.ModelState.Clear();
            mockRepo.Setup(repo => repo.VoegGebruikerToe(It.Is<Klant>(klant =>
                klant.Naam == geldigeKlant.Naam &&
                klant.Email == geldigeKlant.Email &&
                klant.TelefoonNummer == geldigeKlant.TelefoonNummer &&
                klant.Locatie == geldigeKlant.Locatie)))
                .Verifiable();

            // Act
            var result = _controller.RegistreerGebruiker(MapToGebruikerDto(geldigeKlant));

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockRepo.Verify(repo => repo.VoegGebruikerToe(It.IsAny<Klant>()), Times.Once());
        }

        private GebruikerDto MapToGebruikerDto(Klant klant)
        {
            return new GebruikerDto
            {
                Naam = klant.Naam,
                Email = klant.Email,
                TelefoonNummer = klant.TelefoonNummer,
                Locatie = new LocatieDto
                {
                    Gemeente = klant.Locatie.Gemeente,
                    Straatnaam = klant.Locatie.Straatnaam,
                    Huisnummerlabel = klant.Locatie.Huisnummerlabel,
                    Postcode = klant.Locatie.Postcode
                }
            };
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

            var result = _controller.RegistreerGebruiker(_dummyGebruikerInvalidDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateGebruiker_WithValidModel_ReturnsOk()
        {
            var validGebruikerDto = new GebruikerDto
            {
                Naam = "Jan Jansen",
                Email = "janjansen@example.com",
                TelefoonNummer = "0123456789",
                Locatie = new LocatieDto
                {
                    Gemeente = "Gent",
                    Huisnummerlabel = "12B",
                    Postcode = "9000",
                    Straatnaam = "Hoofdstraat"
                }
            };

            int klantenNummer = 1;
            _controller.ModelState.Clear();

            var mockKlant = new Klant
            {
                KlantenNummer = klantenNummer,
                Naam = "Existing User",
                Email = "existing@example.com",
                TelefoonNummer = "0123456789",
                Locatie = new Locatie()
            };

            mockRepo.Setup(repo => repo.GetKlantById(klantenNummer))
                    .Returns(mockKlant);
            mockRepo.Setup(repo => repo.UpdateKlant(It.IsAny<Klant>()))
                    .Verifiable();

            var result = _controller.UpdateGebruiker(klantenNummer, validGebruikerDto);

            Assert.IsType<OkObjectResult>(result);
            mockRepo.Verify(repo => repo.UpdateKlant(It.IsAny<Klant>()), Times.Once());
        }

        [Fact]
        public void UpdateGebruiker_WithInvalidModel_ReturnsBadRequest()
        {
            int klantenNummer = 1;
            var validationContext = new ValidationContext(_dummyGebruikerInvalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_dummyGebruikerInvalidDto, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            var result = _controller.UpdateGebruiker(klantenNummer, _dummyGebruikerInvalidDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateGebruiker_WithNonExistingKlant_ReturnsNotFound()
        {
            var validGebruikerDto = new GebruikerDto
            {
                Naam = "Jan Jansen",
                Email = "janjansen@example.com",
                TelefoonNummer = "0123456789",
                Locatie = new LocatieDto
                {
                    Gemeente = "Gent",
                    Huisnummerlabel = "12B",
                    Postcode = "9000",
                    Straatnaam = "Hoofdstraat"
                }
            };

            int klantenNummer = -1;
            mockRepo.Setup(repo => repo.GetKlantById(klantenNummer)).Returns((Klant)null);
            _controller.ModelState.Clear();

            var result = _controller.UpdateGebruiker(klantenNummer, validGebruikerDto);

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
            int klantenNummer = -1;

            mockRepo.Setup(repo => repo.GetKlantById(klantenNummer))
                    .Returns<Klant>(null);

            var result = _controller.UitschrijvenGebruiker(klantenNummer);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
