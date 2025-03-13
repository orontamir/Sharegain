using Base.Services;
using DAL2Service.DAL.Mongo.Entities;
using DAL2Service.DAL.Sql;
using DAL2Service.Interfaces;
using DAL2Service.Models;
using DAL2Service.Models.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DAL2Service.Services
{
    public class SignalService : DalService, ISignalService
    {
        private ISignalRepository SignalRepository { get; set; }
        public SignalService(RepositoryBase repo, ISignalRepository signalRepository) : base(repo)
        {
            SignalRepository = signalRepository;
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
                MongoSignalEntity mongosignalEntity = new MongoSignalEntity();
                await SignalRepository.InsertAsync(mongosignalEntity);
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
            var signalEntities = await Repository.GetErrorSignal(number);
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
