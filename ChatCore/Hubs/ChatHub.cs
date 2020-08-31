using System;
using System.Threading.Tasks;
using ChatCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatCore.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        #region Init
        
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        #endregion

        #region Identity User -> SignalR User Mappings
        
        public override Task OnConnectedAsync()
        {
            _chatService.ConnectUser(Context);

            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _chatService.DisconnectUser(Context, Clients);

            await base.OnDisconnectedAsync(exception);
        }

        #endregion

        #region User Actions
        
        /// <summary>
        /// Massage sending and command handling
        /// </summary>
        /// <param name="message">User message</param>
        public async Task SendMessage(string message)
        {
            await _chatService.SendMessageAsync(Context, Clients, message);
        }

        /// <summary>
        /// Join notifications, could be moved to OnConneted
        /// </summary>
        public async Task Joined()
        {
            await _chatService.UserJoinedAsync(Context, Clients);
        }
        

        #endregion
    }
}