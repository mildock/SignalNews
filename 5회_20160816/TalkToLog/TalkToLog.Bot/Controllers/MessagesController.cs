using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Data.Entity;
using TalkToLog.Bot.Data;
using TalkToLog.Bot.DomainLogics;
using System.Configuration;
using Microsoft.PowerBI.Security;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs;
using TalkToLog.Bot.Dialogs;

namespace TalkToLog.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private LogService LogService;
        public MessagesController()
        {
            LogService = new LogService();
        }
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            //if (activity.Type == ActivityTypes.Message)
            //{
            //    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            //    // calculate something for us to return
            //    int length = (activity.Text ?? string.Empty).Length;

            //    var uv = await LogService.GetUniqueVisitorCountAsync(new DateTime(2016, 7, 25));
            //    var pv = await LogService.GetPageViewCountAsync(new DateTime(2016, 7, 25));
            //    var signal = await LogService.GetErrorSignal(new DateTime(2016, 7, 25));

            //    // return our reply to the user
            //    Activity reply = activity.CreateReply($"Here is summary of your request.\n unique visitor: {ConvertHumanReadable(uv)}\n Total Page View: {ConvertHumanReadable(pv)}\n Error Status Sign: {CovertSignalToImoticon(signal)}");

            //    //reply.Attachments = new List<Attachment>();
            //    //reply.Attachments.Add(new Attachment()
            //    //{
            //    //    ContentUrl = "https://upload.wikimedia.org/wikipedia/en/a/a6/Bender_Rodriguez.png",
            //    //    ContentType = "image/png",
            //    //    Name = "Bender_Rodriguez.png"
            //    //});

            //    //reply.Attachments.Add(new Attachment()
            //    //{
            //    //    ContentUrl = "https://www.google.com",
            //    //    ContentType = "text/html",
            //    //    Name = "test html"
            //    //});

            //    //using (var client = this.CreatePowerBIClient())
            //    //{
            //    //    var reportsResponse = await client.Reports.GetReportsAsync(this.workspaceCollection, this.workspaceId);
            //    //    var report = reportsResponse.Value.FirstOrDefault(r => r.Id == reportId);
            //    //    var embedToken = PowerBIToken.CreateReportEmbedToken(this.workspaceCollection, this.workspaceId, report.Id);
            //    //}

            //    await connector.Conversations.ReplyToActivityAsync(reply);
            //}
            //else
            //{
            //    HandleSystemMessage(activity);
            //}
            //var response = Request.CreateResponse(HttpStatusCode.OK);
            //return response;

            // check if activity is of type message

            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                //if (activity.Text.ToLower().Equals("hello"))
                //{
                //    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //    Activity reply = activity.CreateReply($"Hello! I'm TalkToLog. What can I do for you?");

                //    reply.Attachments = new List<Attachment>();
                //    reply.Attachments.Add(new Attachment()
                //    {
                //        ContentUrl = "https://upload.wikimedia.org/wikipedia/en/a/a6/Bender_Rodriguez.png",
                //        ContentType = "image/png",
                //        Name = "Bender_Rodriguez.png"
                //    });
                //    await connector.Conversations.ReplyToActivityAsync(reply);
                //}
                //else
                //{
                    await Conversation.SendAsync(activity, () => new LuisLogDialog());
                //}
                
            }
            else
            {
                HandleSystemMessage(activity);
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }

        private IDisposable CreatePowerBIClient()
        {
            throw new NotImplementedException();
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}