using System;
using System.Linq;
using ParkBusinessLayer.Model;
using ParkDataLayer.DataContext;
using ParkDataLayer.Exceptions;
using HuurcontractEF = ParkDataLayer.Model.Huurcontract;
using HuisEF = ParkDataLayer.Model.Huis;
using HuurderEF = ParkDataLayer.Model.Huurder;
using ParkEF = ParkDataLayer.Model.Park;

namespace ParkDataLayer.Mappers {
    public class MapContract {
        public static Huurcontract MapToDomain(HuurcontractEF model) {
            try {
                return new Huurcontract(
                    model.Id,
                    new Huurperiode
                    (
                        model.StartDatum,
                        model.TermijnVerblijf
                    ),
                    HuurdersMap.MapToDomain(model.Huurder),
                    HuisMap.MapToDomain(model.Huis)
                );
            }
            catch (Exception e) {
                throw new MapperException("MapContract - MapToDomain", e);
            }
        }

        public static HuurcontractEF MapToData(Huurcontract model, ParkDataContext ctx) {
            try {
                HuurderEF huurder =
                    ctx
                        .Huurders
                        .FirstOrDefault(x => x.Naam == model.Huurder.Naam);

                HuisEF huis = ctx
                    .Huizen
                    .FirstOrDefault(x => x.Nummer == model.Huis.Nr && x.Park.Id == model.Huis.Park.Id);
                ParkEF park = ctx.Parken.FirstOrDefault(x => x.Id == model.Huis.Park.Id);

                if (huis == null) huis = HuisMap.MapToData(model.Huis, ctx);
                if (huurder == null) huurder = HuurdersMap.MapToData(model.Huurder, ctx);
                if (park == null) park = ParkMap.MapToData(model.Huis.Park);

                huis.Park = park;

                return new HuurcontractEF(
                    model.Id,
                    model.Huurperiode.StartDatum,
                    model.Huurperiode.EindDatum,
                    model.Huurperiode.Aantaldagen,
                    huis.Id,
                    huis,
                    huurder.Id,
                    huurder
                );
            }
            catch (Exception e) {
                throw new MapperException("MapContract - MapToData", e);
            }
        }
    }
}