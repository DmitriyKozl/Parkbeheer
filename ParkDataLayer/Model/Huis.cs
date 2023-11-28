using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.Model {
    public class Huis {
        // EF model moet een lege constructor hebben
        public Huis()
        {
        }

        public Huis(int id, string straat, int nummer, bool actief, Park park)
        {
            Id = id;
            Straat = straat;
            Nummer = nummer;
            Actief = actief;
            Park = park;
        }
        
        public Huis(int id, string straat, int nummer, bool actief, Park park, List<Huurcontract> huurcontracten) : this(id, straat, nummer, actief, park)
        {
            Huurcontracten = huurcontracten;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Straat { get; set; } 

        [Required]
        public int Nummer { get; set; }

        [Required]
        [Column(TypeName = "bit")]

        public bool Actief { get; set; }
        public Park Park { get; set; }

        
        // een huis kan meerdere huurcontracten hebben
        public ICollection<Huurcontract> Huurcontracten { get; set; } = new List<Huurcontract>();
        
     
    }
}