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
    public class NoneDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(NoneMessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task NoneMessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.DontUnderstandMessage();

            context.Done(new ResultDialog { Activity = activity, Wait = true });
        }
    }
}