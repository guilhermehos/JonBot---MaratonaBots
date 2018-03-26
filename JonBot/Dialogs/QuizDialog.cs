using JonBot.Database;
using JonBot.Dialogs.EnumDialog;
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
    public class QuizDialog : IDialog<object>
    {
        private DbQuizDialog _db = new DbQuizDialog();

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(QuizMessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task QuizMessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var activity = await result as Activity;

                if (activity == null)
                {
                    activity = context.Activity as Activity;
                }

                ConnectDB connect = new ConnectDB();
                await connect.CreateDocumentIfNotExists( "Message", activity);
                
                await connect.SaveMessage("Message", activity);

                if (_db.ConversationStatus == (int)QuizEnum.Init)
                {
                    await ConversationInit(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta1)
                {
                    await ConversationPergunta1(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta2)
                {
                    await ConversationPergunta2(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta3)
                {
                    await ConversationPergunta3(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta4)
                {
                    await ConversationPergunta4(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta5)
                {
                    await ConversationPergunta5(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta6)
                {
                    await ConversationPergunta6(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta7)
                {
                    await ConversationPergunta7(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta8)
                {
                    await ConversationPergunta8(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta9)
                {
                    await ConversationPergunta9(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Pergunta10)
                {
                    await ConversationFinal(activity, context);
                }
                else if (_db.ConversationStatus == (int)QuizEnum.Fim)
                {
                    await ConversationConclusao(activity, context);
                }
            }
            catch (Exception ex)
            {
                context.Done(new ResultDialog { Activity = context.Activity as Activity });
            }
        }

        private async Task ConversationInit(Activity activity, IDialogContext context)
        {
            await context.SendMessage(activity, "Então vamos jogar:");

            await context.DoSuggestedActions("A metade do dobro de uma dúzia é igual a:".Trim(), _db.Answers1, QuizMessageReceivedAsync);

            _db.Points = 0;

            _db.ConversationStatus = (int)QuizEnum.Pergunta1;
        }

        private async Task ConversationPergunta1(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers1.Any(x => x.ToLower() == message))
            {
                if (message == ("12").Trim().ToLower())
                {
                    _db.Points += 1;
                }

                await context.DoSuggestedActions("Um homem viu uma toupeira. A toupeira, que também olhou para ele, viu o que?", _db.Answers2, QuizMessageReceivedAsync);

                _db.ConversationStatus = (int)QuizEnum.Pergunta2;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationPergunta2(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers2.Any(x => x.ToLower() == message))
            {
                if (message == ("Nenhuma das respostas acima").Trim().ToLower())
                {
                    _db.Points += 1;
                }

                await context.DoSuggestedActions("O lago Vitória fica em que lugar?", _db.Answers3, QuizMessageReceivedAsync);

                _db.ConversationStatus = (int)QuizEnum.Pergunta3;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationPergunta3(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers3.Any(x => x.ToLower() == message))
            {
                if (message == ("Na África").Trim().ToLower())
                {
                    _db.Points += 1;
                }

                await context.DoSuggestedActions("Uma das respostas abaixo está CORRETA. Qual?", _db.Answers4, QuizMessageReceivedAsync);

                _db.ConversationStatus = (int)QuizEnum.Pergunta4;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationPergunta4(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers4.Any(x => x.ToLower() == message))
            {
                if (message == ("Golfinhos são mamíferos").Trim().ToLower())
                {
                    _db.Points += 1;
                }

                await context.DoSuggestedActions("O avião que ultrapassa a velocidade do som é:", _db.Answers5, QuizMessageReceivedAsync);

                _db.ConversationStatus = (int)QuizEnum.Pergunta5;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationPergunta5(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers5.Any(x => x.ToLower() == message))
            {
                if (message == ("Supersônico").Trim().ToLower())
                {
                    _db.Points += 1;
                }

                await context.DoSuggestedActions("Em que lugar o Papel foi inventado há mais de 2000 anos atrás?", _db.Answers6, QuizMessageReceivedAsync);

                _db.ConversationStatus = (int)QuizEnum.Pergunta6;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationPergunta6(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers6.Any(x => x.ToLower() == message))
            {
                if (message == ("Na China").Trim().ToLower())
                {
                    _db.Points += 1;
                }

                await context.DoSuggestedActions("Qual é o nome do carro conhecido por fazer parte dos filmes de espião 007?", _db.Answers7, QuizMessageReceivedAsync);

                _db.ConversationStatus = (int)QuizEnum.Pergunta7;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationPergunta7(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers7.Any(x => x.ToLower() == message))
            {
                if (message == ("aston martin db5").Trim().ToLower())
                {
                    _db.Points += 1;
                }
                await context.DoSuggestedActions("O Réptil predador pré-histórico mais feroz que existiu, foi:", _db.Answers8, QuizMessageReceivedAsync);

                _db.ConversationStatus = (int)QuizEnum.Pergunta8;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationPergunta8(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers8.Any(x => x.ToLower() == message))
            {
                if (message == ("O Tiranossauro Rex").Trim().ToLower())
                {
                    _db.Points += 1;
                }
                await context.DoSuggestedActions("Famoso pensador que primeiro escreveu sobre a existência da lendária Atlântida?", _db.Answers9, QuizMessageReceivedAsync);

                _db.ConversationStatus = (int)QuizEnum.Pergunta9;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationPergunta9(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers9.Any(x => x.ToLower() == message))
            {
                if (message == ("Platão").Trim().ToLower())
                {
                    _db.Points += 1;
                }
                await context.DoSuggestedActions("Qual das montadoras de automóveis abaixo é brasileira?", _db.Answers10, QuizMessageReceivedAsync);

                _db.ConversationStatus = (int)QuizEnum.Pergunta10;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationFinal(Activity activity, IDialogContext context)
        {
            string message = activity.Text.ToLower();

            if (_db.Answers10.Any(x => x.ToLower() == message))
            {
                if (message == ("agrale").Trim().ToLower())
                {
                    _db.Points += 1;
                }
                await context.SendMessage(activity, "Calculando seu resultado...");

                if (_db.Points > 7)
                {
                    await context.SendMessage(activity, "Você fez " + _db.Points + " pontos de 10");

                    await context.DoSuggestedActions("🎈", _db.ConversationOption, QuizMessageReceivedAsync);
                }
                else
                {
                    await context.DoSuggestedActions("Você fez " + _db.Points + " pontos de 10", _db.ConversationOption, QuizMessageReceivedAsync);
                }

                _db.ConversationStatus = (int)QuizEnum.Fim;
            }
            else
            {
                context.Done(new ResultDialog { Activity = activity });
            }
        }

        private async Task ConversationConclusao(Activity activity, IDialogContext context)
        {
            var message = activity.Text;

            if (message == "Responder de novo")
            {
                await ConversationInit(activity, context);
            }
            else
            {
                await context.SendMessage(activity, "Ok, vamos!!");
                context.Done(new ResultDialog { Activity = activity });
            }
        }
    }
}