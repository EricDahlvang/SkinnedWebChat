using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using System.Configuration;

namespace SkinnedWebChat.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        const string YouCanAskMeMessage = "You can ask me to change the theme to default, theme 2, or theme 3 (Unless you're in the iframed chat window).";

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                Activity reply = null;

                string text = activity.Text;
                int length = (text ?? string.Empty).Length;
                
                if (length > 0)
                {
                    string lowerText = text.ToLower();
                    if (lowerText.Contains("help"))
                    {
                        reply = activity.CreateReply(YouCanAskMeMessage);
                    }
                    else if (lowerText.Contains("theme") || lowerText.Contains("default"))
                    {
                        // return our reply to the direct channel
                        if (lowerText.Contains("theme 1"))
                            lowerText = "default";

                        reply = activity.CreateReply();
                        reply.Type = ActivityTypes.Event;
                        reply.Name = "changetheme";
                        reply.Value = lowerText.Replace(" ","") ;
                    }
                }
                else
                {
                    reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters. " + YouCanAskMeMessage);
                }

                // return our reply to the user
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
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
                IConversationUpdateActivity conversationupdate = message;
               
                if (conversationupdate.MembersAdded.Any() && (message.MembersAdded.Any(o => o.Id != ConfigurationManager.AppSettings["BotId"])))
                {
                    ConnectorClient connector = new ConnectorClient(new System.Uri(message.ServiceUrl));
                    Activity reply = message.CreateReply("Welcome.  I'm a sample Themed Bot. " + YouCanAskMeMessage );
                    connector.Conversations.ReplyToActivityAsync(reply);
                    message.Type = ActivityTypes.Message;
                }
                
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