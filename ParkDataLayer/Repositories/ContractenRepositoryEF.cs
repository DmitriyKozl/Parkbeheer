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
    public class ContractenRepositoryEF : IContractenRepository {
        private ParkDataContext ctx;

        public ContractenRepositoryEF(string connectionString) {
            this.ctx = new ParkDataContext(connectionString);
        }

        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }


        public void AnnuleerContract(Huurcontract contract) {
            try {
                ctx.Huurcontracten.Remove(new Model.Huurcontract()
                { Id = contract.Id });
                SaveAndClear();
            }
            catch (Exception ex) {
                throw new RepositoryException("ContractRepositoyEF - AnnuleerContract", ex);
            }
        }

        public Huurcontract GeefContract(string id) {
            try {
                return MapContract.MapToDomain(
                    ctx.Huurcontracten
                        .Where(x => x.Id == id)
                        .Include(x => x.Huis)
                        .ThenInclude(x => x.Park)
                        .Include(x => x.Huurder)
                        .AsNoTracking().FirstOrDefault());
            }
            catch (Exception ex) {
                throw new RepositoryException("ContractRepositoyEF - GeefContract(id)", ex);
            }
        }

        public List<Huurcontract> GeefContracten(DateTime dtBegin, DateTime? dtEinde) {
            try
            {
                if (dtEinde is null)
                {
                    return ctx.Huurcontracten.Where(h => h.StartDatum >= dtBegin)
                        .Include(x => x.Huis)
                        .Include(x => x.Huurder)
                        .Include(x => x.Huis.Park)
                        .Select(h => MapContract.MapToDomain(h))
                        .AsNoTracking()
                        .ToList();
                }
                else
                {


                    return ctx.Huurcontracten.Where(h => h.StartDatum >= dtBegin && h.StartDatum <= dtEinde)
                        .Include(x => x.Huis)
                        .Include(x => x.Huurder)
                        .Include(x => x.Huis.Park)
                        .Select(h => MapContract.MapToDomain(h))
                        .AsNoTracking()
                        .ToList();
                }
            }            catch (Exception ex) {
                throw new RepositoryException("ContractRepositoyEF - GeefContracten", ex);
            }
        }

        public bool HeeftContract(DateTime startDatum, int huurderid, int huisid) {
            try {
                return ctx.Huurcontracten.Any(x =>
                    x.StartDatum == startDatum && x.HuurderId == huurderid && x.HuisId == huisid);
            }
            catch (Exception ex) {
                throw new RepositoryException("ContractRepositoyEF - HeeftContract", ex);
            }
        }

        public bool HeeftContract(string id) {
            try {
                
                return ctx.Huurcontracten.Any(x => x.Id == id);
            }
            catch (Exception ex) {
                throw new RepositoryException("ContractRepositoyEF - HeeftContract", ex);
            }
        }

        public void UpdateContract(Huurcontract contract) {
            try {
                ctx.Huurcontracten.Update(MapContract.MapToData(contract, ctx));
                SaveAndClear();
            }
            catch (Exception ex) {
                throw new RepositoryException("ContractRepositoyEF - UpdateContract", ex);
            }
        }

        public void VoegContractToe(Huurcontract contract) {
            try {
                
                ctx.Huurcontracten.Add(MapContract.MapToData(contract, ctx));
                SaveAndClear();
            }
            catch (Exception ex) {
                throw new RepositoryException("ContractRepositoyEF - VoegContractToe", ex);
            }
        }
    }
}