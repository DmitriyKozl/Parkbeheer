using System;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;

using ParkEF = ParkDataLayer.Model.Park;
using HuisEF = ParkDataLayer.Model.Huis;
using Huis = ParkBusinessLayer.Model.Huis;
using Park = ParkBusinessLayer.Model;
namespace ParkDataLayer.Mappers {
    public class ParkMap {


        public static Park.Park MapToDomain(ParkEF p)
        {
            try
            {
                Park.Park park = new Park.Park(p.Id, p.Naam, p.Locatie);
                if (p.Huizen.Count > 0)
                {
                    foreach (var h in p.Huizen)
                    {
                        Huis huis = new(h.Straat, h.Nummer, park);
                        huis.ZetId(h.Id);
                        park.VoegHuisToe(huis);
                    }
                }
                return park;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapToDomain - Park - failed", ex);
            }
        }


        public static ParkEF MapToData(Park.Park p) {
            try {
                return new ParkEF(p.Id, p.Naam, p.Locatie);
            }
            catch (Exception ex) {
                throw new MapperException("MapToDB - ParkEF - gefaald", ex);
            }
        }
    }
}