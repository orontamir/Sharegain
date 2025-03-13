using Base.Models;
using Base.Services;
using DALService.DAL.Sql;
using DALService.DAL.Sql.Entities;
using DALService.Interfaces;
using DALService.Models;
using DALService.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DALService.Services
{
    public class SignalService : DalService, ISignalService
    {
        public SignalService(RepositoryBase repo) : base(repo)
        {
        }

        public async Task<ActionResult<bool>> AddSignal(SignalDbModel signal)
        {
            if (signal == null) 
            {
                return false;
            }
            try
            {
                var signalEntity = signal.ToEntity();
                await Repository.CreateOrUpdateAsync(signalEntity);
                return true;
            }
            catch (Exception ex) 
            {
                LogService.LogError($"Exception When Add signal to data base, error message: {ex.Message}");
                return false;
            }
            
        }

        public async Task<List<SignalDbModel>> GetErrorList(int number)
        {
            List<SignalDbModel> list = new List<SignalDbModel>();
             var signalEntities =  await Repository.GetErrorSignal(number);
            foreach (var signalEntity in signalEntities) 
            {
                list.Add(signalEntity.ToModel());
            }

            return list;
        }

        public async Task<List<SignalDbModel>> GetSinList(int number)
        {
            List<SignalDbModel> list = new List<SignalDbModel>();
            var sinList = await Repository.GetSin(number);
            foreach (var sin in sinList)
            {
                list.Add(sin.ToModel());
            }

            return list;
        }

        public async Task<List<SignalDbModel>> GetStateList(int number)
        {
            List<SignalDbModel> list = new List<SignalDbModel>();
            var stateList = await Repository.GetState(number);
            foreach (var state in stateList)
            {
                list.Add(state.ToModel());
            }
            return list;
        }
    }
}
