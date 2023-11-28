using System;
using ParkDataLayer.DataContext;
using ParkDataLayer.Exceptions;
using ParkBusinessLayer.Model;
using ParkEF = ParkDataLayer.Model.Park;
using HuurderEF = ParkDataLayer.Model.Huurder;
using Huurder = ParkBusinessLayer.Model.Huurder;
using Park = ParkBusinessLayer.Model;

namespace ParkDataLayer.Mappers {
    public class HuurdersMap {
        public static Huurder MapToDomain(HuurderEF model) {
            try {
                return new Huurder(
                    model.Id,
                    model.Naam,
                    new Contactgegevens(
                        model.Email,
                        model.Telefoon,
                        model.Adres
                    )
                );
            }
            catch (Exception e) {
                throw new MapperException("MapToDomain - Huurder - failed", e);
            }
        }

        public static HuurderEF MapToData(Huurder model, ParkDataContext ctx) {
            try {
                return new HuurderEF(
                    model.Id,
                    model.Naam,
                    model.Contactgegevens.Tel,
                    model.Contactgegevens.Email,
                    model.Contactgegevens.Adres
                );
            }
            catch (Exception e) {
                throw new MapperException("MapToData - Huurder - failed", e);
            }
        }
    }
}