using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkDataLayer.Model {
    public class Park {
        [Key]
        [StringLength(20)]
        public string Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Naam { get; set; }

        [StringLength(500)]
        public string Locatie { get; set; } 
        
        // een park kan meerdere huizen hebben
        
        public List<Huis> Huizen { get; set; } = new List<Huis>();
        
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