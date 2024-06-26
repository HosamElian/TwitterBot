﻿using Microsoft.Extensions.Configuration;
using System.Text;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterBot.Core;
using TwitterBot.Core.IServices;
using TwitterBot.Core.NoDbModels;

namespace BusinessLogic.Services
{
    public class TwitterHandlerService : ITwitterHandlerService
    {
        private readonly IConfiguration _configuration;

        public TwitterHandlerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> PostTweet(PostTweetRequestDto postModel)
        {
            var firstkey = _configuration.GetSection(Shared.Keys_Holder).GetSection(nameof(Shared.TwitterFirstKey)).Value;
            var secondkey = _configuration.GetSection(Shared.Keys_Holder).GetSection(nameof(Shared.TwitterSecondKey)).Value;
            var thirdkey = _configuration.GetSection(Shared.Keys_Holder).GetSection(nameof(Shared.TwitterThirdKey)).Value;
            var fourthkey = _configuration.GetSection(Shared.Keys_Holder).GetSection(nameof(Shared.TwitterFourthKey)).Value;

            //var client = new TwitterClient("1763894308338278400-FIwb6caWaRAbgjc46lrfDvRy6rwUcN",
            //                                "LP9xvOhlyc2q0eCeykjqq3JbGBsk5ecxVl7qUsTRd264U",
            //                                "1763894308338278400-xIUQGecm8W9NeW24vq3NM43oh0O2W5",
            //                                "vmxDx9u3ridY1hf0scB7Sb02TOqQ5lc0mx36mdtN5Rg7E");
            var client = new TwitterClient(firstkey, secondkey, thirdkey, fourthkey);
            var result = await client.Execute.AdvanceRequestAsync(BuildTwitterRequest(postModel, client));

            if (!result.Response.IsSuccessStatusCode) return await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        private static Action<ITwitterRequest> BuildTwitterRequest(
       PostTweetRequestDto postModel,
       TwitterClient client)
        {
            return (ITwitterRequest request) =>
            {
                var jsonBody = client.Json.Serialize(postModel);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                request.Query.Url = "https://api.twitter.com/2/tweets";
                request.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
                request.Query.HttpContent = content;
            };
        }
    }

   

}
