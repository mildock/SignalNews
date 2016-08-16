using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using TalkToLog.Bot.DomainLogics;

namespace TalkToLog.Bot.Dialogs
{
    [Serializable]

    public class LogDialog : IDialog<object>
    {
        protected DateTime CurrentDate;
        private LogService LogService;

        public async Task StartAsync(IDialogContext context)
        {
            LogService = new LogService();

            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            if (message.Text.ToLower().StartsWith("what is going on yesterday"))
            {
                var uv = await LogService.GetUniqueVisitorCountAsync(new DateTime(2016, 7, 25));
                var pv = await LogService.GetPageViewCountAsync(new DateTime(2016, 7, 25));
                var signal = await LogService.GetErrorSignal(new DateTime(2016, 7, 25));

                await context.PostAsync($"Here is summary of your request./unique visitor: {DialogHelper.ConvertHumanReadable(uv)}/ Total Page View: {DialogHelper.ConvertHumanReadable(pv)}/ Error Status Sign: {DialogHelper.CovertSignalToImoticon(signal)}");
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                await context.PostAsync("I can't understand. Sorry");
                context.Wait(MessageReceivedAsync);
            }
        }

    }
}