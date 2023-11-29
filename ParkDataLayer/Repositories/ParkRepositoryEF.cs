using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.DataContext;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;
using ParkEF = ParkDataLayer.Model.Park;

namespace ParkDataLayer.Repositories {
    public class ParkRepositoryEF : IParkRepository {
        private ParkDataContext ctx;

        public ParkRepositoryEF(string conn) {
            this.ctx = new ParkDataContext(conn);
        }

        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public Park GeefPark(string id) {
            try {
                return ParkMap.MapToDomain(
                    ctx.Parken
                        .Include(h => h.Huizen)
                        .Where(x => x.Id == id)
                        .AsNoTracking()
                        .FirstOrDefault());
            }
            catch (Exception ex) {
                throw new RepositoryException("GeefPark", ex);
            }
        }

        public void UpdatePark(Park p) {
            try {
                ctx.Parken.Update(ParkMap.MapToData(p));
                SaveAndClear();
            }
            catch (Exception ex) {
                throw new RepositoryException("UpdatePark", ex);
            }
        }

        public void VoegParkToe(Park p) {
            try {
                ParkEF park = ParkMap.MapToData(p);
                ctx.Parken.Add(park);
                SaveAndClear();
            }
            catch (Exception ex) {
                throw new RepositoryException("VoegParkToe", ex);
            }
        }
    }
}