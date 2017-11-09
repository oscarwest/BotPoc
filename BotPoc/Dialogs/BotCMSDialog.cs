using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using BotPoc.Dialogs.Payments;
using System.Threading;
using BotPoc.Services;
using BotPoc.Helpers;
using System.Collections.Generic;

namespace BotPoc.Dialogs
{
    [Serializable]
    public class BotCMSDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            //_botService = new BotCMSService();


            await context.PostAsync($"BOT CMS Dialog");
            var initialNodeId = "465";
            await context.PostAsync(await BotCMSService.GetBotSaysAsync(initialNodeId));

            var msg = await MessageActivityHelper.CreateCardActionMessage(context, await BotCMSService.GetYouSaysAsync(initialNodeId));
            await context.PostAsync(msg);

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            string nodeId;
            try
            {
                nodeId = message.Text;
            }
            catch (Exception)
            {
                await context.PostAsync("Kunde inte läsa av meddelandetexten..");
                context.Done(this);
                return;
            }

            // Show next stuff
            await context.PostAsync(await BotCMSService.GetBotSaysAsync(nodeId));

            var msg = await MessageActivityHelper.CreateCardActionMessage(context, await BotCMSService.GetYouSaysAsync(nodeId));
            await context.PostAsync(msg);

            if (msg.SuggestedActions.Actions.Count <= 0)
            {
                await context.PostAsync("Vill du veta något mer?");
                context.Done(this);
            }
            else
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}