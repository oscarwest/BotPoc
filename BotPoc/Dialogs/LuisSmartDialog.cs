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
        public override async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"BOT AI Dialog");

            context.Wait(MessageReceived);
        }

        [LuisIntent("payments-information-invoice")]
        public async Task ShowInvoiceInformation(IDialogContext context, LuisResult result)
        {
            var invoice = new InvoiceInfo();
            invoice.InvoiceNumber = "123456";
            invoice.Amount = 400m;

            await context.PostAsync($"Information kring din faktura: {invoice.InvoiceNumber} med belopp {invoice.Amount}");

            context.Wait(MessageReceived);
        }

        [LuisIntent("payments-pay-invoice")]
        public async Task PayInvoice(IDialogContext context, LuisResult result)
        {
            var invoice = new InvoiceInfo();
            invoice.InvoiceNumber = "123456";
            invoice.Amount = 400m;

            await context.PostAsync($"Visa guidat flöde för att betala din faktura (FormFlow)");

            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {

            //var qnaDialog = new BasicQnAMakerDialog();
            var messageToForward = result.Query;

            //await context.Forward(qnaDialog, AfterFAQDialog, messageToForward, CancellationToken.None);

            string message = $"Jag förstår inte vad du menar...";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
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