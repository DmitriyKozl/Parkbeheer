using System;
using System.ComponentModel.DataAnnotations;

namespace ParkDataLayer.Model {
    public class Huurcontract {
        [Key] [StringLength(25)] public string Id { get; set; }

        [Required] public DateTime StartDatum { get; set; }

        [Required] public DateTime EindDatum { get; set; }

        [Required] public int TermijnVerblijf { get; set; }

        [Required] public int HuisId { get; set; }
        
        // een huurcontract heeft 1 huis

        public Huis Huis { get; set; }
        
        // een huurcontract heeft 1 huurder

        [Required] public int HuurderId { get; set; }

        public Huurder Huurder { get; set; }

        public Huurcontract() { }

        public Huurcontract(string id, DateTime startDatum, DateTime eindDatum, int termijnVerblijf) {
            Id = id;
            StartDatum = startDatum;
            EindDatum = eindDatum;
            TermijnVerblijf = termijnVerblijf;
            
            
        }
    }
}