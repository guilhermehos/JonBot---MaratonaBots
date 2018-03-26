using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using JonBot.Database;
using JonBot.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace JonBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var activity = await result as Activity;

                LuisModel luisModel = new LuisModel();

                luisModel = await luisModel.Get(activity.Text);

                if (luisModel.topScoringIntent.intent == "Greeting")
                {
                    await context.Forward(new GreetingDialog(), ResumeAfterCompleted, activity, CancellationToken.None);
                }
                else if (luisModel.topScoringIntent.intent == "Quiz")
                {
                    await context.Forward(new QuizDialog(), ResumeAfterCompleted, activity, CancellationToken.None);
                }
                else if (luisModel.topScoringIntent.intent == "Finish")
                {
                    await context.Forward(new FinishDialog(), ResumeAfterCompleted, activity, CancellationToken.None);
                }
                else
                {
                    await context.Forward(new NoneDialog(), ResumeAfterCompleted, activity, CancellationToken.None);
                }
                
            }
            catch (TooManyAttemptsException ex)
            {
                context.Done(new ResultDialog { Activity = context.Activity as Activity });
            }
        }

        public async Task ResumeAfterCompleted(IDialogContext context, IAwaitable<object> result)
        {
            var resultDialog = await result as ResultDialog;

            if (resultDialog.Wait)
            {
                context.Wait(MessageReceivedAsync);

                return;
            }


            //await new IntentFactory().GetIntent(ResumeAfterCompleted, resultDialog.Activity, context, resultDialog.Obj);
        }
    }
}