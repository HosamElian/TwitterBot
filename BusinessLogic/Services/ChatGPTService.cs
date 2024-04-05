using Microsoft.Extensions.Configuration;
using OpenAI_API;
using TwitterBot.Core;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.IServices;
using TwitterBot.Core.Models;

namespace BusinessLogic.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public ChatGPTService(IUnitOfWork unitOfWork,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<bool> SendChatMessage(IEnumerable<News> news)
        {
            var key = _configuration.GetSection(nameof(Shared.Keys_Holder)).GetSection(nameof(Shared.ChatGPT_Key)).Value;

            var openAI = new OpenAIAPI(new APIAuthentication(key));
            var convrsation = openAI.Chat.CreateConversation();

            foreach (var item in news)
            {
                convrsation.AppendUserInput($"Please Reformate this to be more Formal and write only reformated News {item.OriganalNews} ");
                item.ParaphrasdNews = await convrsation.GetResponseFromChatbotAsync();
            }
            if (_unitOfWork.SaveChanges()) return true;
            return false;

        }
        public async Task<bool> SendChatMessage(News news)
        {
            var key = _configuration.GetSection(Shared.Keys_Holder).GetSection(Shared.ChatGPT_Key).Value;

            var openAI = new OpenAIAPI(new APIAuthentication(key));
            var convrsation = openAI.Chat.CreateConversation();
            convrsation.AppendUserInput($"Please Reformate this to be more Formal and write only reformated News {news.OriganalNews} ");
            news.ParaphrasdNews = await convrsation.GetResponseFromChatbotAsync();
            
            if (_unitOfWork.SaveChanges()) return true;
            return false;

        }
    }
}
