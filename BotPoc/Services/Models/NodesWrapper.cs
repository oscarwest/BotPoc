using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotPoc.Services.Models
{
    public class NodesWrapper
    {
        [JsonRequired]
        [JsonProperty("data")]
        public List<Node> Data { get; set; }
    }

    public class Node
    {
        [JsonRequired]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonRequired]
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonRequired]
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
    }

    public class Attributes
    {
        [JsonRequired]
        [JsonProperty("spaceId")]
        public string SpaceId { get; set; }

        [JsonRequired]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonRequired]
        [JsonProperty("searchTitle")]
        public string SearchTitle { get; set; }

        [JsonRequired]
        [JsonProperty("botSays")]
        public List<TextMessage> BotSays { get; set; }

        [JsonProperty("youSays")]
        public List<NodeMessage> YouSays { get; set; }

        [JsonRequired]
        [JsonProperty("showSearch")]
        public bool ShowSearch { get; set; }
    }

    public class NodeMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("attributes")]
        public NodeMessageAttributes Attributes { get; set; }
    }

    public class NodeMessageAttributes
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "nodeId")]
        public string NodeId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "link")]
        public string Link { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "phone")]
        public string Phone { get; set; }
    }

    public class TextMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("attributes")]
        public TextMessageAttributes Attributes { get; set; }
    }

    public class TextMessageAttributes
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}