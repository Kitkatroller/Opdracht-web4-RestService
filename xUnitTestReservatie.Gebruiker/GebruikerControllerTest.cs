using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTestReservatie.Gebruiker
{
    class GebruikerControllerTest
    {
        private readonly Mock<GebruikerService> _mockGebruikerService;
        private readonly GebruikerController _controller;
        private readonly GebruikerDto _dummyGebruikerDto;

        public GebruikerControllerTest()
        {
            // Mock the GebruikerService
            _mockGebruikerService = new Mock<GebruikerService>();
            // Create an instance of the GebruikerController with the mocked service
            _controller = new GebruikerController(_mockGebruikerService.Object);
			// Initialize a dummy GebruikerDto for testing
			// Initialize with test data
			_dummyGebruikerDto = new GebruikerDto
			{
				Naam = "Jan Jansen",
				Email = "jan@example.com",
				TelefoonNummer = "0123456789",
				Locatie = new LocatieDto
				{
					Gemeente = "Amsterdam",
					Huisnummerlabel = "123A",
					Postcode = "1000 AA",
					Straatnaam = "Hoofdstraat"
				}
			};
		}

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
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = _controller.RegistreerGebruiker(_dummyGebruikerDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
