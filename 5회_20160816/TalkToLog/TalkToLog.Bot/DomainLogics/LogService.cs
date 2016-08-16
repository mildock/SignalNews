using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TalkToLog.Bot.Data;

namespace TalkToLog.Bot.DomainLogics
{
    public class LogService : ILogService
    {
        private LogDbContext LogDbContext;
        public LogService()
        {
            LogDbContext = new LogDbContext();
        }
        public async Task<long> GetErrorCountAsync(DateTime date)
        {
            var cnt = await LogDbContext.HitErrorRatioPerIPAndHour1.Where(x => DbFunctions.TruncateTime(x.st) == DbFunctions.TruncateTime(date)).SumAsync(x => x.Errors);
            return (cnt.HasValue ? cnt.Value : 0);
        }

        public async Task<long> GetPageViewCountAsync(DateTime date)
        {
            return await LogDbContext.SparkSummaries.Where(x => DbFunctions.TruncateTime(x.st) == DbFunctions.TruncateTime(date)).Select(x => x.TotalRequest).FirstOrDefaultAsync();
        }

        public async Task<long> GetUniqueVisitorCountAsync(DateTime date)
        {
            return await LogDbContext.SparkSummaries.Where(x => DbFunctions.TruncateTime(x.st) == DbFunctions.TruncateTime(date)).Select(x => x.TotalUniqueClients).FirstOrDefaultAsync();
        }

        public async Task<HealthSignal> GetErrorSignal(DateTime date)
        {
            var errorCnt = await GetErrorCountAsync(date);
            if (errorCnt >=0 && errorCnt < 1000)
            {
                return HealthSignal.Green;
            }
            else if (errorCnt >= 1000 && errorCnt < 10000)
            {
                return HealthSignal.Yellow;
            }
            else
            {
                return HealthSignal.Red;
            }
        }
    }
}