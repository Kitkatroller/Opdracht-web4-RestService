using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ReservatieBeheer.DL.EFModels
{
    public class KlantEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KlantenNummer { get; set; }

        [Required(ErrorMessage = "Naam is required")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Geen geldig Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Telefoonnummer moet numeric zijn")]
        public string TelefoonNummer { get; set; }
        public bool IsUitgeschreven { get; set; }

        public int LocatieID { get; set; }
        [ForeignKey("LocatieID")]
        public LocatieEF Locatie { get; set; }

        public ICollection<ReservatieEF> Reservaties { get; set; }

        //Constructors
        public KlantEF() { }

        public KlantEF(string naam, string email, string telefoonNummer, LocatieEF locatie, ICollection<ReservatieEF> reservaties) 
        {
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Locatie = locatie;
            Reservaties = reservaties;
        }
    }
}
