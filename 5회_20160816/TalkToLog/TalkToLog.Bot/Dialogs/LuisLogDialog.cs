using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TalkToLog.Bot.DomainLogics;

namespace TalkToLog.Bot.Dialogs
{
    [Serializable]
    // luis.ai 에서 AppID과 Key를 입력
    [LuisModel("", "")]
    public class LuisLogDialog : LuisDialog<object>
    {
        private DateTime CurrentDate;
        [field: NonSerialized]
        private LogService LogService;

        private const string Entity_Current_Date = "builtin.datetime.date";

        public LuisLogDialog()
        {
            LogService = new LogService();
        }

        private ResultDate TryFindDate(LuisResult result)
        {
            ResultDate resultdate = new ResultDate();
            EntityRecommendation title;

            if (result.TryFindEntity(Entity_Current_Date, out title))
            {
                resultdate.Title = title.Entity;
                resultdate.Date = (title.Resolution != null && title.Resolution.Count > 0 ? DateTime.Parse(title.Resolution["date"]) : yesterday);
            }
            else
            {
                resultdate.Title = "yesterday";
            }

            return resultdate;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Hello")]
        public async Task Hellow(IDialogContext context, LuisResult result)
        {
            string message = $"Hello! I'm 'TalkToLog'. What can I do for you? :)";
            var msg = context.MakeMessage();
            msg.Text = message;
            msg.Attachments = new List<Attachment>();
            msg.Attachments.Add(new Attachment()
            {
                ContentUrl = "https://upload.wikimedia.org/wikipedia/en/a/a6/Bender_Rodriguez.png",
                ContentType = "image/png",
                Name = "Bender_Rodriguez.png"
            });
            await context.PostAsync(msg);
            context.Wait(MessageReceived);
        }

        [LuisIntent("PageViewCount")]
        public async Task PageView(IDialogContext context, LuisResult result)
        {
            var resultdate = TryFindDate(result);
            var pv = await LogService.GetPageViewCountAsync(resultdate.Date);

            CurrentDate = resultdate.Date;

            string message = $"Total Page View of {resultdate.Title} is {DialogHelper.ConvertHumanReadable(pv)}";
            var msg = context.MakeMessage();
            msg.Text = message;
            msg.Attachments = new List<Attachment>();
            msg.Attachments.Add(new Attachment()
            {
                ContentUrl = "http://talktologweb.azurewebsites.net/images/mvcproducts/Page%20View/" + pv.ToString() + "/default.png",
                //ContentUrl = "http://localhost:3979/images/mvcproducts/Page%20View/" + pv.ToString() + "/default.png",
                ContentType = "image/png",
                Name = "Bender_Rodriguez.png"
            });
            await context.PostAsync(msg);

            context.Wait(MessageReceived);
        }

        [LuisIntent("UniqueViewCount")]
        public async Task UniqueView(IDialogContext context, LuisResult result)
        {
            var resultdate = TryFindDate(result);
            var uv = await LogService.GetUniqueVisitorCountAsync(resultdate.Date);

            CurrentDate = resultdate.Date;

            string message = $"Total Unique View Count of {resultdate.Title} is {DialogHelper.ConvertHumanReadable(uv)}";
            var msg = context.MakeMessage();
            msg.Text = message;
            msg.Attachments = new List<Attachment>();
            msg.Attachments.Add(new Attachment()
            {
                ContentUrl = "http://talktologweb.azurewebsites.net/images/mvcproducts/Unique%20Visitor/" + uv.ToString() + "/default.png",
                //ContentUrl = "http://localhost:3979/images/mvcproducts/Unique%20Visitor/" + uv.ToString() + "/default.png",
                ContentType = "image/png",
                Name = "Bender_Rodriguez.png"
            });
            await context.PostAsync(msg);
            context.Wait(MessageReceived);
        }
    }

    public class ResultDate
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}