using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitterBot.Core.NoDbModels
{
    public class PostTweetRequestDto
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }
}
