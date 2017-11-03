using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BotPoc.Dialogs
{
    [LuisModel("0c514d30-d08b-492c-bf7e-ed3e6346f362", "87e62426146c405b8271694020c12d4a", domain: "westus.api.cognitive.microsoft.com")]
    [Serializable]
    public class LuisSmartDialog : LuisDialog<object>
    {
        [LuisIntent("payments-information-invoice")]
        public async Task ShowInvoiceInformation(IDialogContext context, LuisResult result)
        {


            //var reply = activity.CreateReply("I have colors in mind, but need your help to choose the best one.");
            //reply.Type = ActivityTypes.Message;
            //reply.TextFormat = TextFormatTypes.Plain;

            //reply.SuggestedActions = new SuggestedActions()
            //{
            //    Actions = new List<CardAction>()
            //    {
            //        new CardAction(){ Title = "Blue", Type=ActionTypes.ImBack, Value="Blue" },
            //        new CardAction(){ Title = "Red", Type=ActionTypes.ImBack, Value="Red" },
            //        new CardAction(){ Title = "Green", Type=ActionTypes.ImBack, Value="Green" }
            //    }
            //};



            var invoice = new InvoiceInfo();
            invoice.InvoiceNumber = "123456";
            invoice.Amount = 400m;

            await context.PostAsync($"Information kring din faktura: {invoice.InvoiceNumber} med belopp {invoice.Amount}");
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {

            var qnaDialog = new BasicQnAMakerDialog();
            var messageToForward = result.Query;

            await context.Forward(qnaDialog, AfterFAQDialog, messageToForward, CancellationToken.None);

            //string message = $"Jag förstår inte vad du menar...";
            //await context.PostAsync(message);
            //context.Wait(MessageReceived);
        }

        private async Task AfterFAQDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var messageHandled = await result;
            }
            catch (Exception e)
            {

                await context.PostAsync($"Sorry, I wasn't sure what you wanted. message: {e.Message}");
                throw;
            }
            finally
            {
                context.Wait(MessageReceived);
            }
        }
    };

    public class InvoiceInfo
    {
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
    };
}