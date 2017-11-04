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
            var msg = await this.CreateCardActionMessage(context, new List<CardAction>()
            {
                new CardAction() { Title = "Mina fakturor", Type = ActionTypes.PostBack, Value = "Mina fakturor" },
                new CardAction() { Title = "Ränta och avgifter", Type = ActionTypes.ImBack, Value = "Ränta och avgifter" },
                new CardAction() { Title = "Leverans och retur", Type = ActionTypes.ImBack, Value = "Leverans och retur" },
                new CardAction() { Title = "Kreditupplysningar", Type = ActionTypes.ImBack, Value = "Kreditupplysningar" },
                new CardAction() { Title = "Bedrägeri", Type = ActionTypes.ImBack, Value = "Bedrägeri" }
            });

            await context.PostAsync(msg);

            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var messageText = message.Text;
            IMessageActivity msg = null;

            switch (messageText)
            {
                case "Mina fakturor":
                    msg = await this.CreateCardActionMessage(context, new List<CardAction>()
                    {
                        new CardAction() { Title = "Fakturaleverans", Type = ActionTypes.PostBack, Value = "Fakturaleverans", DisplayText = "Fakturaleverans info displaytext", Text = "Fakturaleverans info text" }
                    });
                    await context.PostAsync(msg);
                    break;
                case "Fakturaleverans":
                    msg = await this.CreateCardActionMessage(context, new List<CardAction>()
                    {
                        new CardAction() { Title = "Hur skickas min faktura?", Type = ActionTypes.PostBack, Value = "Hur skickas min faktura?" },
                        new CardAction() { Title = "Varför ser jag inte min faktura?", Type = ActionTypes.PostBack, Value = "Varför ser jag inte min faktura?" },
                        new CardAction() { Title = "Samma faktura på flera sätt", Type = ActionTypes.PostBack, Value = "Samma faktura på flera sätt" },
                        new CardAction() { Title = "Varför har jag fått en faktura från Collector?", Type = ActionTypes.PostBack, Value = "Varför har jag fått en faktura från Collector?" }
                    });
                    await context.PostAsync(msg);
                    break;

                default:
                    break;
            }


            //await context.PostAsync("paymentDialog received");

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task<IMessageActivity> CreateCardActionMessage(IDialogContext context, List<CardAction> cardActions)
        {
            var msg = context.MakeMessage();
            msg.Type = ActivityTypes.Message;
            msg.TextFormat = TextFormatTypes.Plain;
            msg.SuggestedActions = new SuggestedActions()
            {
                Actions = cardActions
            };

            return await Task.FromResult(msg);
        }
    }
}