using DAL2Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace DAL2Service.Interfaces
{
    public interface ISignalService
    {

        Task<List<SignalDbModel>> GetSinList(int number);
        Task<List<SignalDbModel>> GetStateList(int number);
        Task<List<SignalDbModel>> GetErrorList(int number);

        Task<ActionResult<bool>> AddSignal(SignalDbModel signal);
    }
}
