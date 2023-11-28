using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.Model {
    public class Park {
        [Key]
        [Column(TypeName = "nvarchar(20)")]
        public string Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Naam { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Locatie { get; set; } 
        
        // een park kan meerdere huizen hebben
        
        public ICollection<Huis> Huizen { get; set; } = new List<Huis>();
        
        public Park() { }
        
        public Park(string id, string naam, string locatie) {
            Id = id;
            Naam = naam;
            Locatie = locatie;
        }
        
        public override string ToString() {
            return $"{Id} - {Naam} - {Locatie}";
        }
    }
}