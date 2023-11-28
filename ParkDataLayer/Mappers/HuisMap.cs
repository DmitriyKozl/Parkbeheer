using System;
using System.Collections.Generic;
using System.Linq;
using ParkBusinessLayer.Model;
using ParkDataLayer.DataContext;
using ParkDataLayer.Exceptions;
using HuurcontractEF = ParkDataLayer.Model.Huurcontract;
using HuisEF = ParkDataLayer.Model.Huis;
using HuurderEF = ParkDataLayer.Model.Huurder;
using ParkEF = ParkDataLayer.Model.Park;

namespace ParkDataLayer.Mappers {
    public class HuisMap {
        public static Huis MapToDomain(HuisEF model) {
            try
            {
                Dictionary<Huurder, List<Huurcontract>> Contracten = new Dictionary<Huurder, List<Huurcontract>>();

                //Map over the dictionary
                foreach (var huurcontract in model.Huurcontracten)
                {
                    //Herken alle waarden
                    Huurder h = new Huurder(huurcontract.HuurderId, huurcontract.Huurder.Naam,
                        new Contactgegevens(huurcontract.Huurder.Email, huurcontract.Huurder.Telefoon,
                            huurcontract.Huurder.Adres));

                    var contract = new Huurcontract(huurcontract.Id, new Huurperiode(
                        huurcontract.StartDatum, huurcontract.TermijnVerblijf), h, new Huis(
                        huurcontract.Huis.Id, huurcontract.Huis.Straat, huurcontract.Huis.Nummer, huurcontract.Huis.Actief, new Park(
                            huurcontract.Huis.Park.Id, huurcontract.Huis.Park.Naam, huurcontract.Huis.Park.Locatie)));

                    if (!Contracten.ContainsKey(h))
                    {
                        Contracten.Add(h, new List<Huurcontract>() { contract });
                    }
                    else
                    {
                        Contracten[h].Add(contract);
                    }
                }
                //Aangezien huis er nog niet is, voeg toe!
                return new Huis(model.Id, model.Straat, model.Nummer, model.Actief, ParkMap.MapToDomain(model.Park), Contracten);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapToDomain - huis - failed", ex);
            }
        }

        public static HuisEF MapToData(Huis model, ParkDataContext ctx) {
            try {
                ParkEF park = ctx.Parken.FirstOrDefault(x => x.Id == model.Park.Id);
                if (park == null) ParkMap.MapToData(model.Park);
                return new HuisEF(
                    model.Id,
                    model.Straat,
                    model.Nr,
                    model.Actief,
                    park
                );
            }
            catch (Exception e) {
                throw new MapperException("MapHuis - MapToData", e);
            }
        }
    }
}