using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Core.NoDbModels;

namespace TwitterBot.Core.IServices
{
    public interface ITwitterHandlerService
    {
        public Task<bool> PostTweet(PostTweetRequestDto postDto);
    }
}
