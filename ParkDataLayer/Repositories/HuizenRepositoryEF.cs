using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParkDataLayer.DataContext;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;

namespace ParkDataLayer.Repositories {
    public class HuizenRepositoryEF : IHuizenRepository {
        private ParkDataContext ctx;

        public HuizenRepositoryEF(string conn) {
            this.ctx = new ParkDataContext(conn);
        }

        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public Huis GeefHuis(int id) {
            try {
                return HuisMap.MapToDomain
                (
                    ctx.Huizen.Where(p => p.Id == id)
                        .Include(p => p.Huurcontracten)
                        .ThenInclude(p => p.Huurder)
                        .Include(p => p.Park)
                        .AsNoTracking()
                        .FirstOrDefault()
                );
            }
            catch (Exception ex) {
                throw new RepositoryException("HuizenRepository - GeefHuis", ex);
            }
        }

        public bool HeeftHuis(string straat, int nummer, Park park) {
            try {
                if (ctx.Huizen
                        .Where(h => h.Straat == straat
                                    && h.Nummer == nummer
                                    && h.Park == ParkMap.MapToData(park))
                        .AsNoTracking()
                        .FirstOrDefault() != null)
                    return true;
                else
                    return false;
            }

            catch (Exception ex) {
                throw new RepositoryException("HuizenRepository - HeeftHuis(string straat, int nummer, Park park)", ex);
            }
        }

        public bool HeeftHuis(int id) {
            try {
     
                return ctx.Huizen.Any(h => h.Id == id);
            }

            catch (Exception ex) {
                throw new RepositoryException("HuizenRepository - HeeftHuis(int id)", ex);
            }
     
        }

        public void UpdateHuis(Huis huis) {
            try {
                ctx.Huizen.Update(HuisMap.MapToData(huis, ctx));
                SaveAndClear();
            }
            catch (Exception ex) {
                throw new RepositoryException("HuizenRepository - UpdateHuis", ex);
            }
        }

        public Huis VoegHuisToe(Huis h) {
            try {
                ctx.Huizen.Add(HuisMap.MapToData(h, ctx));
                SaveAndClear();
                return h;
            }
            catch (Exception ex) {
                throw new RepositoryException("HuizenRepository - GeefHuis", ex);
            }
        }
    }
}