﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
