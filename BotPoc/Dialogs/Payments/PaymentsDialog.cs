using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BotPoc.Dialogs.Payments
{
    [Serializable]
    public class PaymentsDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var msg = context.MakeMessage();
            msg.Type = ActivityTypes.Message;
            msg.TextFormat = TextFormatTypes.Plain;
            msg.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                     new CardAction() { Title = "Mina fakturor", Type = ActionTypes.ImBack, Value = "someValue" },
                     new CardAction() { Title = "Ränta och avgifter", Type = ActionTypes.ImBack, Value = "someValue" },
                     new CardAction() { Title = "Leverans och retur", Type = ActionTypes.ImBack, Value = "someValue" },
                     new CardAction() { Title = "Kreditupplysningar", Type = ActionTypes.ImBack, Value = "someValue" },
                     new CardAction() { Title = "Bedrägeri", Type = ActionTypes.ImBack, Value = "someValue" }
                }
            };

            await context.PostAsync(msg);

            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            await context.PostAsync("paymentDialog received");

            context.Done(this);
        }
    }
}