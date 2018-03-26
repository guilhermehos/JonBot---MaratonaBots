﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace JonBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            try
            {
                if (activity.Type == ActivityTypes.Message)
                {
                    await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
                }
                else
                {
                    HandleSystemMessage(activity);
                }

                var response = Request.CreateResponse(HttpStatusCode.OK);

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
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
                IConversationUpdateActivity update = message;

                var client = new ConnectorClient(new Uri(message.ServiceUrl), new MicrosoftAppCredentials());

                if (update.MembersAdded != null && update.MembersAdded.Any(x => x.Name != "Bot"))
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));

                    Activity reply = message.CreateReply("🚘");

                    reply.Type = ActivityTypes.Message;

                    reply.TextFormat = TextFormatTypes.Plain;

                    reply.SuggestedActions = new SuggestedActions()
                    {
                        Actions = new List<CardAction>()
                        {
                            new CardAction(){ Title = "Começar", Type=ActionTypes.ImBack, Value="Começar" }
                        }
                    };

                    connector.Conversations.ReplyToActivityAsync(reply);
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