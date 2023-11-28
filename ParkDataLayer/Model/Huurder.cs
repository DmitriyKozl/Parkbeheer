using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.Model {
    public class Huurder {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Naam { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Telefoon { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Adres { get; set; }
 
        public Huurder() { }

        public Huurder(string naam, string telefoon, string email, string adres) {
            Naam = naam;
            Telefoon = telefoon;
            Email = email;
            Adres = adres;
        }

        public Huurder(int id, string naam, string telefoon, string email, string adres) {
            Id = id;
            Naam = naam;
            Telefoon = telefoon;
            Email = email;
            Adres = adres;
        }
    }
}