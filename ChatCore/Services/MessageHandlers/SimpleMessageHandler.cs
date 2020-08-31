using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ChatCore.Services.MessageHandlers
{
    /// <summary>
    /// Last handler, it just sends message to all other users. Never fails :D
    /// </summary>
    public class SimpleMessageHandler : IMessageHandler
    {
        public uint Order => 1000;

        private readonly IChatService _chatService;

        public SimpleMessageHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<bool> Handle(HubCallerContext context, IHubCallerClients clients, string message)
        {
            //todo Should store message for later
            await clients.All.SendAsync("ReceiveMessage", DateTime.Now.ToString("G"), _chatService.GetName(context), message);

            return true;
        }
    }
}