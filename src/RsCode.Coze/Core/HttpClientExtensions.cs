using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace RsCode.Coze;
internal static class HttpClientExtensions
{
    //public static async Task<TResponse> PostAndReadAsAsync<TResponse>(this HttpClient client, string uri, object? requestModel, CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
    //{
    //    var response = await client.PostAsJsonAsync(uri, requestModel, new JsonSerializerOptions
    //    {
    //        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    //    }, cancellationToken);
    //    return await HandleResponseContent<TResponse>(response, cancellationToken);
    //}
    //public static async Task<TResponse> HandleResponseContent<TResponse>(this HttpResponseMessage response, CancellationToken cancellationToken) where TResponse : BaseResponse, new()
    //{
    //    TResponse result;

    //    if (!response.Content.Headers.ContentType?.MediaType?.Equals("application/json", StringComparison.OrdinalIgnoreCase) ?? true)
    //    {
    //        result = new()
    //        {
    //            Error = new()
    //            {
    //                MessageObject = await response.Content.ReadAsStringAsync(cancellationToken)
    //            }
    //        };
    //    }
    //    else
    //    {
    //        result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken) ?? throw new InvalidOperationException();
    //    }

    //    result.HttpStatusCode = response.StatusCode;
    //    result.HeaderValues = response.ParseHeaders();

    //    return result;
    //}
    public static HttpResponseMessage PostAsStreamAsync(this HttpClient client, string uri, object requestModel, CancellationToken cancellationToken = default)
    {
        var settings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        var content = JsonContent.Create(requestModel, null, settings);

        using var request = CreatePostEventStreamRequest(uri, content);

        try
        {
            return client.Send(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        }
        catch (PlatformNotSupportedException)
        {
            using var newRequest = CreatePostEventStreamRequest(uri, content);

            return SendRequestPreNet6(client, newRequest, cancellationToken);
        }
    }

    public static async Task<TResponse> HandleResponseContent<TResponse>(this HttpResponseMessage response, CancellationToken cancellationToken) where TResponse : class
    {
        TResponse result;

        if (!response.Content.Headers.ContentType?.MediaType?.Equals("application/json", StringComparison.OrdinalIgnoreCase) ?? true)
        {
            return null;
        }
        else
        {
            result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken) ?? throw new InvalidOperationException();
        }

         
       // result.HttpStatusCode = response.StatusCode;
        //result.HeaderValues = response.ParseHeaders();

        return result;
    }
    public static ResponseHeaderValues ParseHeaders(this HttpResponseMessage response)
    {
        return new()
        {
            Date = response.Headers.Date,
            Connection = response.Headers.Connection?.ToString(),
            AccessControlAllowOrigin = response.Headers.GetHeaderValue("access-control-allow-origin"),
            CacheControl = response.Headers.GetHeaderValue("cache-control"),
            Vary = response.Headers.Vary?.ToString(),
            XRequestId = response.Headers.GetHeaderValue("x-request-id"),
            StrictTransportSecurity = response.Headers.GetHeaderValue("strict-transport-security"),
            CFCacheStatus = response.Headers.GetHeaderValue("cf-cache-status"),
            SetCookie = response.Headers.Contains("set-cookie") ? response.Headers.GetValues("set-cookie").ToList() : null,
            Server = response.Headers.Server?.ToString(),
            CF_RAY = response.Headers.GetHeaderValue("cf-ray"),
            AltSvc = response.Headers.GetHeaderValue("alt-svc"),
            All = response.Headers.ToDictionary(x => x.Key, x => x.Value.AsEnumerable()),

            RateLimits = new()
            {
                LimitRequests = response.Headers.GetHeaderValue("x-ratelimit-limit-requests"),
                LimitTokens = response.Headers.GetHeaderValue("x-ratelimit-limit-tokens"),
                LimitTokensUsageBased = response.Headers.GetHeaderValue("x-ratelimit-limit-tokens_usage_based"),
                RemainingRequests = response.Headers.GetHeaderValue("x-ratelimit-remaining-requests"),
                RemainingTokens = response.Headers.GetHeaderValue("x-ratelimit-remaining-tokens"),
                RemainingTokensUsageBased = response.Headers.GetHeaderValue("x-ratelimit-remaining-tokens_usage_based"),
                ResetRequests = response.Headers.GetHeaderValue("x-ratelimit-reset-requests"),
                ResetTokens = response.Headers.GetHeaderValue("x-ratelimit-reset-tokens"),
                ResetTokensUsageBased = response.Headers.GetHeaderValue("x-ratelimit-reset-tokens_usage_based")
            },

            OpenAI = new()
            {
                Model = response.Headers.GetHeaderValue("openai-model"),
                Organization = response.Headers.GetHeaderValue("openai-organization"),
                ProcessingMs = response.Headers.GetHeaderValue("openai-processing-ms"),
                Version = response.Headers.GetHeaderValue("openai-version")
            }
        };
    }
    public static string? GetHeaderValue(this HttpResponseHeaders headers, string headerName)
    {
        return headers.Contains(headerName) ? headers.GetValues(headerName).FirstOrDefault() : null;
    }
    private static HttpResponseMessage SendRequestPreNet6(HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var responseTask = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        var response = responseTask.GetAwaiter().GetResult();
        return response;
    }
    private static HttpRequestMessage CreatePostEventStreamRequest(string uri, HttpContent content)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Headers.Accept.Add(new("text/event-stream"));
        request.Content = content;

        return request;
    }
}

