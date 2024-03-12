using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot.Core.IServices
{
    public interface IChatGPTService
    {
        Task<ChatCompletionMessage[]> SendChatMessage(string message);
    }
}
