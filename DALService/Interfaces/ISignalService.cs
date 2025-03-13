using Base.Models;
using DALService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DALService.Interfaces
{
    public interface ISignalService
    {

        Task<List<SignalDbModel>> GetSinList(int number);
        Task<List<SignalDbModel>> GetStateList(int number);
        Task<List<SignalDbModel>> GetErrorList(int number);

        Task<ActionResult<bool>> AddSignal(SignalDbModel signal);
    }
}
