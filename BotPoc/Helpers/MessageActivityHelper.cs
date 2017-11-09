using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BotPoc.Helpers
{
    public static class MessageActivityHelper
    {
        public static async Task<IMessageActivity> CreateCardActionMessage(IDialogContext context, List<CardAction> cardActions)
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