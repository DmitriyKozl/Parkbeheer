using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParkDataLayer.DataContext;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;

namespace ParkDataLayer.Repositories {
    public class HuurderRepositoryEF : IHuurderRepository {
        private ParkDataContext ctx;

        public HuurderRepositoryEF(string conn) {
            this.ctx = new ParkDataContext(conn);
        }

        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public Huurder GeefHuurder(int id) {
            try {
                return HuurdersMap.MapToDomain(ctx.Huurders
                    .Where(x => x.Id == id)
                    .AsNoTracking()
                    .FirstOrDefault());
            }
            catch (Exception e) {
                throw new RepositoryException("GeefHuurders met id", e);
            }
        }

        public List<Huurder> GeefHuurders(string naam) {
            try {
                return ctx.Huurders.Select(p => HuurdersMap.MapToDomain(p)).ToList();
            }
            catch (Exception e) {
                throw new RepositoryException("GeefHuurders met naam", e);
            }
        }

        public bool HeeftHuurder(string naam, Contactgegevens contact) {
            try {
                return ctx.Huurders
                    .Any(
                        h => h.Naam == naam
                             && h.Telefoon == contact.Tel
                             && h.Email == contact.Email
                             && h.Adres == contact.Adres);
            }
            catch (Exception e) {
                throw new RepositoryException("HeeftHuurder naam", e);
            }
        }

        public bool HeeftHuurder(int id) {
            try {
                return ctx.Huizen.Any(h => h.Id == id);
            }
            catch (Exception e) {
                throw new RepositoryException("HeeftHuurder id", e);
            }
        }

        public void UpdateHuurder(Huurder huurder) {
            try {
                ctx.Huurders.Update(HuurdersMap.MapToData(huurder));
                SaveAndClear();
            }
            catch (Exception e) {
                throw new RepositoryException("UpdateHuurder", e);
            }
        }

        public Huurder VoegHuurderToe(Huurder huurder) {
            try {
                ctx.Huurders.Add(HuurdersMap.MapToData(huurder));
                SaveAndClear();
                return huurder;
            }
            catch (Exception e) {
                throw new RepositoryException("GeefHuurders", e);
            }
        }
    }
}