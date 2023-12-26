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

        public GebruikerController(GebruikerService gebruikerService)
        {
            _gebruikerService = gebruikerService;
        }

        [HttpPost("registreren")]
        public IActionResult RegistreerGebruiker(GebruikerDto gebruikerDto)
        {
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
                // Hier kun je logging toevoegen
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
        }

        // PUT: api/gebruiker/update/{klantenNummer}
        [HttpPut("update/{klantenNummer}")]
        public IActionResult UpdateGebruiker(int klantenNummer, [FromBody] GebruikerDto gebruikerDto)
        {
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
                // Voeg logica voor foutafhandeling toe
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
        }

        [HttpPut("uitschrijven/{klantenNummer}")]
        public IActionResult UitschrijvenGebruiker(int klantenNummer)
        {
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
                // Logica voor foutafhandeling
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
        }
    }
}
