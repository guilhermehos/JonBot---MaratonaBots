using JonBot.Helper;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JonBot.Dialogs
{
    [Serializable]
    public class FinishDialog : IDialog<object>
    {
        private static string[] _thanks = new string[] { "Nos vemos em breve!!", "Até mais!!" , "See you Later." };

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(ThanksMessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task ThanksMessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(_thanks.SelectRandomdly());

            context.Done(new ResultDialog { Wait = true });
        }
    }
}