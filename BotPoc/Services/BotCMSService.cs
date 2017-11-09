using BotPoc.Services.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BotPoc.Services
{
    public static class BotCMSService
    {
        private static HttpClient _client = new HttpClient(new HttpClientHandler(), disposeHandler: false);
        private static NodesWrapper _nodes;


        public static async Task<string> GetBotSaysAsync(string id)
        {
            _nodes = await GetNodesAsync();
            var node = await GetNodeById(id);
            var text = node.Attributes.BotSays.FirstOrDefault().Attributes.Text;

            return text;
        }

        public static async Task<List<CardAction>> GetYouSaysAsync(string id)
        {
            _nodes = await GetNodesAsync();
            var list = new List<CardAction>();
            var node = await GetNodeById(id);

            foreach (var item in node.Attributes.YouSays)
            {
                list.Add(
                    new CardAction()
                    {
                        Title = item.Attributes.Text,
                        Type = ActionTypes.ImBack,
                        Value = item.Attributes.NodeId,
                        DisplayText = item.Attributes.Text,
                        Text = item.Attributes.NodeId
                    });
            }

            return list;
        }

        private static Task<Node> GetNodeById(string id)
        {
            return Task.FromResult(_nodes.Data.FirstOrDefault(n => n.Id == id));
        }

        private static async Task<NodesWrapper> GetNodesAsync()
        {
            var response = _client.GetAsync("https://faqbot-production.herokuapp.com/api/nodes").Result;
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var nodes = Newtonsoft.Json.JsonConvert.DeserializeObject<NodesWrapper>(result);

            return nodes;
        }
    }
}