using JonBot.Helper;
using JonBot.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JonBot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            string name = context.Activity.From.Name;
            List<string> results = new List<string>() { "Olá " + name, "Oi " + name, "Olá " + name + " 😉!", "Oi " + name + " 😉!" };

            LuisModel luisModel = new LuisModel();

            luisModel = await luisModel.Get(results.SelectRandomdly());

            // return our reply to the user
            await context.PostAsync(results.SelectRandomdly());

            //context.Wait(MessageReceivedAsync);

            context.Done(new ResultDialog { Activity = activity });
        }
    }
}