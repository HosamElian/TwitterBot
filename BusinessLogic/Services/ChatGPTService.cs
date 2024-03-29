﻿using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using TwitterBot.Core.IServices;

namespace BusinessLogic.Services
{
    public class ChatGPTService : IChatGPTService
    {
        public ChatGPTService()
        {
            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiKey = "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",
                OrganizationId = "organizationId"
            };

            openAIClient = new OpenAIClient(openAIConfigurations);
            _messages = new();
        }
        //Where business happens
        async Task<ChatCompletionMessage[]> SendChatMessage(
          ChatCompletionMessage message)
        {
            //we should send all the messages
            //so we can give Open AI context of conversation
            StackMessages(message);

            var chatCompletion = new ChatCompletion
            {
                Request = new ChatCompletionRequest
                {
                    Model = "gpt-3.5-turbo",
                    Messages = _messages.ToArray(),
                    Temperature = 0.2,
                    MaxTokens = 800
                }
            };

            var result = await openAIClient
              .ChatCompletions
              .SendChatCompletionAsync(chatCompletion);

            var choices = result.Response.Choices;

            var messages = ToCompletionMessage(choices);

            //stack the response as well - everything is context to Open AI
            StackMessages(messages);

            return messages;
        }
        readonly OpenAIClient openAIClient;

        //all messages in the conversation
        readonly List<ChatCompletionMessage> _messages;

        //initialize the configuration with api key and sub

        void StackMessages(params ChatCompletionMessage[] message)
        {
            _messages.AddRange(message);
        }

        static ChatCompletionMessage[] ToCompletionMessage(
          ChatCompletionChoice[] choices)
          => choices.Select(x => x.Message).ToArray();

        //Public method to Send messages to OpenAI
        public Task<ChatCompletionMessage[]> SendChatMessage(string message)
        {
            var chatMsg = new ChatCompletionMessage()
            {
                Content = message,
                Role = "user"
            };
            return SendChatMessage(chatMsg);
        }
    }
}
