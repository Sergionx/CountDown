using CountDown.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountDown.Services
{
    public interface IMomentDataStore
    {
        Task<string> AddMomentAsync(Moment moment);
        Task<bool> UpdateMomentAsync(Moment moment);
        Task<Moment> GetMomentAsync(string id);
        Task<IList<Moment>> GetMomentsAsync();
        Task<IList<Moment>> DeleteMomentAsync(Moment moment);
    }
}