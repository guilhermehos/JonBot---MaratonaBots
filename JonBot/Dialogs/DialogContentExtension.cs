using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JonBot.Dialogs
{
    public static class DialogContentExtension
    {
        public static async Task DontUnderstandMessage(this IDialogContext context)
        {
            await context.PostAsync("Desculpe, não entendi o que você quis dizer...");

            await context.PostAsync("Sou um robô muito novo ainda! Por favor tente falar de forma mais simples.");
        }

        public static async Task SendMessage(this IDialogContext context, Activity activity, string message)
        {
            var reply = activity.CreateReply(String.Empty);

            reply.Type = ActivityTypes.Typing;

            await context.PostAsync(reply);

            await context.PostAsync(message);
        }

        public static async Task DoSuggestedActions(this IDialogContext context, string title, string[] values, ResumeAfter<object> messageReceivedAsync, bool notInSkype = false)
        {
            if (!notInSkype && values != null && values.Count() > 0)
            {
                var reply = context.MakeMessage();

                reply.Text = title;

                reply.Type = ActivityTypes.Message;

                reply.TextFormat = TextFormatTypes.Plain;

                if (context.Activity.ChannelId != "skype")
                {
                    var actions = new List<CardAction>();

                    foreach (var item in values)
                    {
                        actions.Add(new CardAction() { Title = item, Type = ActionTypes.ImBack, DisplayText = item, Value = item });
                    }

                    reply.SuggestedActions = new SuggestedActions()
                    {
                        Actions = actions
                    };

                    await context.PostAsync(reply);
                }
                else
                {
                    PromptDialog.Choice(context, messageReceivedAsync, new PromptOptions<object>(title, "Não é uma opção válida", "...", values, 0, new PromptStyler(PromptStyle.Auto)));
                }
            }
        }
    }
}