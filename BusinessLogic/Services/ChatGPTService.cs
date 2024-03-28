using OpenAI_API;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.IServices;
using TwitterBot.Core.Models;

namespace BusinessLogic.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChatGPTService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SendChatMessage(IEnumerable<News> news)
        {
            var openAI = new OpenAIAPI(new APIAuthentication("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"));
            var convrsation = openAI.Chat.CreateConversation();

            foreach (var item in news)
            {
                convrsation.AppendUserInput($"Please Reformate this to be more Formal and write only reformated News {item.OriganalNews} ");
                item.ParaphrasdNews = await convrsation.GetResponseFromChatbotAsync();
            }
            if (_unitOfWork.SaveChanges()) return true;
            return false;

        }
    }
}
