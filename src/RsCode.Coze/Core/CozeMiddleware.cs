
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using RsCode.Coze.Chat;
using System.Text.Json;

namespace RsCode.Coze
{
    public static class CozeMiddlewareExtensions
    {
        public static IApplicationBuilder UseCoze(this IApplicationBuilder app)
        {
            if(app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<CozeMiddleware>();
        }
    }

    public class CozeMiddleware
    {
        RequestDelegate _next;
        CozeManager cozeManager;
        ChatService chatService;
        public CozeMiddleware(RequestDelegate next,CozeManager cozeManager,ChatService chatService) {
            _next = next;
            this.cozeManager = cozeManager;
            this.chatService = chatService;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            //if(!context.Request.Path.Value.ToLower().StartsWith("/coze/chat"))
            //{
            //    await _next(context);
            //}else
            //{
            //    // 读取前端发送的请求体
            //    using var streamReader = new StreamReader(context.Request.Body);
            //    var body=await streamReader.ReadToEndAsync();
            //    var request = JsonSerializer.Deserialize<ChatRequest>(body);
            //    if(request==null)
            //    {
            //        await _next(context);
            //    }else
            //    {
            //        await CallApi(context,request);
            //    }
               
            //}  
        }

        async Task CallApi(HttpContext context,ChatRequest request)
        {
            //context.Response.ContentType = "text/event-stream";
            //try
            //{
            //    cozeManager.RefreshToken("");
            //    string botId = "7430113185258782754"; //文案创作

            //    //验证客户端
            //    //var clientInfo = apiDomainService.GetApiClientInfo();
            //    //string userId = clientInfo.UserId;
            //    var chatRequest = new ChatSendRequest
            //    {
            //        BotId = botId,
            //        UserId = userId,
            //        Stream = true,
            //        AutoSaveHistory = true,
            //        AdditionalMessages = request.Messages
            //    };
            //    //请求api
            //    var clientInfo = apiDomainService.GetApiClientInfo();
            //    string userId = clientInfo.UserId;
            //    using var aiResponse = await chatService.SendByStreamAsync(request.ConversactionId, chatRequest);
            //    using var aiStream = await aiResponse.Content.ReadAsStreamAsync();

            //    var buffer = new byte[4096];
            //    int bytesRead;
            //    while ((bytesRead = await aiStream.ReadAsync(buffer, context.RequestAborted)) > 0)
            //    {
            //        if (context.RequestAborted.IsCancellationRequested) break;

            //        // 解析AI响应（根据实际格式调整）
            //        var chunk = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //        var cleanedChunk = CleanChunk(chunk); // 处理特殊字符

            //        // 封装为SSE格式
            //        await context.Response.WriteAsync($"data: {cleanedChunk}\n\n", context.RequestAborted);
            //        await context.Response.Body.FlushAsync(context.RequestAborted);
            //    }
            //}
            //catch (OperationCanceledException)
            //{
            //    // 正常取消
            //}
            //finally
            //{

            //}
        }

        string CleanChunk(string chunk) =>
    System.Text.Json.JsonSerializer.Serialize(new { content = chunk });

    }
}
