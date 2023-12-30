using Microsoft.AspNetCore.Mvc;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.Gebruiker.API.DTOs;
using ReservatieBeheer.BL.Services;

namespace ReservatieBeheer.Gebruiker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GebruikerController : ControllerBase
    {
        private readonly GebruikerService _gebruikerService;
        private readonly ILogger<GebruikerController> _logger;

        public GebruikerController(GebruikerService gebruikerService, ILogger<GebruikerController> logger)
        {
            _gebruikerService = gebruikerService;
            _logger = logger;
        }

        [HttpPost("registreren")]
        public IActionResult RegistreerGebruiker(GebruikerDto gebruikerDto)
        {
            _logger.LogInformation("RegistreerGebruiker actie aangeroepen");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _gebruikerService.GebruikerRegistreren(
                    gebruikerDto.Naam,
                    gebruikerDto.Email,
                    gebruikerDto.TelefoonNummer,
                    new Locatie
                    {
                        Gemeente = gebruikerDto.Locatie.Gemeente,
                        Huisnummerlabel = gebruikerDto.Locatie.Huisnummerlabel,
                        Postcode = gebruikerDto.Locatie.Postcode,
                        Straatnaam = gebruikerDto.Locatie.Straatnaam
                    });

                return Ok("Gebruiker succesvol geregistreerd");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het registreren van gebruiker: {Message}", ex.Message);
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
        }

        [HttpPut("update/{klantenNummer}")]
        public IActionResult UpdateGebruiker(int klantenNummer, [FromBody] GebruikerDto gebruikerDto)
        {
            _logger.LogInformation($"UpdateGebruiker actie aangeroepen voor klantnummer {klantenNummer}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _gebruikerService.UpdateGebruiker(klantenNummer, gebruikerDto.Naam, gebruikerDto.Email, gebruikerDto.TelefoonNummer,
                    new Locatie
                    {
                        Gemeente = gebruikerDto.Locatie.Gemeente,
                        Huisnummerlabel = gebruikerDto.Locatie.Huisnummerlabel,
                        Postcode = gebruikerDto.Locatie.Postcode,
                        Straatnaam = gebruikerDto.Locatie.Straatnaam
                    });
                return Ok("Gebruiker succesvol bijgewerkt");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Klant niet gevonden");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
        }

        [HttpPut("uitschrijven/{klantenNummer}")]
        public IActionResult UitschrijvenGebruiker(int klantenNummer)
        {
            _logger.LogInformation($"UitschrijvenGebruiker actie aangeroepen voor klantnummer {klantenNummer}");

            try
            {
                _gebruikerService.UitschrijvenGebruiker(klantenNummer);
                return Ok("Gebruiker succesvol uitgeschreven");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Klant niet gevonden");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
        }
    }
}
