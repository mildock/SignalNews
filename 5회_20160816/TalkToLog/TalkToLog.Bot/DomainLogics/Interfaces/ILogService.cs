using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkToLog.Bot.Data;

namespace TalkToLog.Bot.DomainLogics
{
    public interface ILogService
    {
        Task<HealthSignal> GetErrorSignal(DateTime date); 

        Task<long> GetUniqueVisitorCountAsync(DateTime date);

        Task<long> GetPageViewCountAsync(DateTime date);

        Task<long> GetErrorCountAsync(DateTime date);
    }
}
