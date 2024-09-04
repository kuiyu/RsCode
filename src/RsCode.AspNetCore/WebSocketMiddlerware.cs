/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */

using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace RsCode.AspNetCore
{
    public class WebSocketMiddlerware
    {
        readonly RequestDelegate next;
        public WebSocketMiddlerware(RequestDelegate _next)
        {
            this.next = _next;
        }
       
        ConcurrentDictionary<string, WebSocket> sockets = new ConcurrentDictionary<string, WebSocket>();

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {
                if (!context.WebSockets.IsWebSocketRequest)
                {
                    await next.Invoke(context);
                    return;
                }

                var token = context.RequestAborted;
                var socket = await context.WebSockets.AcceptWebSocketAsync();

                var guid = Guid.NewGuid().ToString();
                sockets.TryAdd(guid, socket);

                while (true)
                {
                    if (token.IsCancellationRequested)
                        break;

                    var message = await GetMessageAsync(socket, token);
                    //System.Console.WriteLine($"Received message - {message} at {DateTime.Now}");

                    if (string.IsNullOrEmpty(message))
                    {
                        if (socket.State != WebSocketState.Open)
                            break;

                        continue;
                    }

                    foreach (var s in sockets.Where(p => p.Value.State == WebSocketState.Open))
                        await SendMessageAsync(s.Value, message, token);
                }

                sockets.TryRemove(guid, out WebSocket redundantSocket);

                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Session ended", token);
                socket.Dispose();
            }
        }

        async Task<string> GetMessageAsync(WebSocket socket, CancellationToken token)
        {
            WebSocketReceiveResult result;
            var message = new ArraySegment<byte>(new byte[4096]);
            string receivedMessage = string.Empty;

            do
            {
                token.ThrowIfCancellationRequested();

                result = await socket.ReceiveAsync(message, token);
                var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                receivedMessage = Encoding.UTF8.GetString(messageBytes);

            } while (!result.EndOfMessage);

            if (result.MessageType != WebSocketMessageType.Text)
                return null;

            return receivedMessage;
        }

        Task SendMessageAsync(WebSocket socket, string message, CancellationToken token)
        {
            var byteMessage = Encoding.UTF8.GetBytes(message);
            var segmnet = new ArraySegment<byte>(byteMessage);

            return socket.SendAsync(segmnet, WebSocketMessageType.Text, true, token);
        }
    }

    /*
     * Configure()
     * var webSocketOptions = new WebSocketOptions()
			{
				KeepAliveInterval = TimeSpan.FromSeconds(120),
				ReceiveBufferSize = 4 * 1024
			};
			app.UseWebSockets(webSocketOptions);

            app.UseMiddleware<WebSocketMiddlerware>(); 
     */
}
