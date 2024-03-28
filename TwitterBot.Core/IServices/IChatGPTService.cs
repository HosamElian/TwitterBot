using TwitterBot.Core.Models;

namespace TwitterBot.Core.IServices
{
    public interface IChatGPTService
    {
        Task<bool> SendChatMessage(IEnumerable<News> news);
    }
}
