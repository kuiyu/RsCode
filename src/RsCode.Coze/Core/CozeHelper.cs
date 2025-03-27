using Microsoft.Extensions.Options;
using OpenAI.Chat;
using RsCode.Coze.Core;
using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Coze
{
    public class CozeHelper
    { 
        CozeManager cozeManager;
        HttpClient _httpClient;
        public ChatClient ChatClient { get;private set; }
       
        public CozeHelper(CozeManager cozeManager, HttpClient httpClient)
        {
            this.cozeManager = cozeManager;
            _httpClient = httpClient;
            //var options = _optionsSnapshot.Value.FirstOrDefault();
            var token = cozeManager.RefreshToken("");
            ApiKeyCredential cred = new ApiKeyCredential(token);
            ChatClient = new ChatClient("coze", cred, new OpenAI.OpenAIClientOptions
            {
                Endpoint = new Uri("https://api.coze.com"),
                UserAgentApplicationId = "webmote",
                ProjectId = "coze",
                RetryPolicy = ClientRetryPolicy.Default
            });
        }

        public async IAsyncEnumerable<ChatCompletionCreateResponse> CreateCompletionAsStream(string url,ChatCompletionCreateRequest chatCompletionCreateRequest, string? modelId = null, bool justDataMode = true,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            // Mark the request as streaming
            chatCompletionCreateRequest.Stream = true;

            // Send the request to the CompletionCreate endpoint
            //chatCompletionCreateRequest.ProcessModelId(modelId, _defaultModelId);

            using var response = _httpClient.PostAsStreamAsync(url, chatCompletionCreateRequest, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                yield return await response.HandleResponseContent<ChatCompletionCreateResponse>(cancellationToken);
                yield break;
            }

            await foreach (var baseResponse in response.AsStream<ChatCompletionCreateResponse>(cancellationToken: cancellationToken)) yield return baseResponse;
        }
    }
}
