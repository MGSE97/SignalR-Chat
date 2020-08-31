using System.Collections.Generic;
using System.Threading.Tasks;
using ChatCore.Models;
using ChatCore.Services.MessageHandlers;
using Microsoft.AspNetCore.SignalR;

namespace ChatCore.Services
{
    /// <summary>
    /// Chat functions and handlers
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        /// Message handlers for user messages. Created on initialization.
        /// </summary>
        IList<IMessageHandler> MessageHandlers { get; set; }

        #region User Management

        /// <summary>
        /// Map Identity user to SignalR Id
        /// </summary>
        /// <param name="context">Hub context</param>
        void ConnectUser(HubCallerContext context);

        /// <summary>
        /// Remove User mappings before disconnect
        /// </summary>
        /// <param name="context">Hub context</param>
        /// <param name="clients">Client collection</param>
        Task DisconnectUser(HubCallerContext context, IHubCallerClients clients);

        /// <summary>
        /// Get`s user name from email
        /// </summary>
        /// <param name="context">Hub context</param>
        /// <param name="email">User email</param>
        /// <returns>Name before @ part</returns>
        string GetName(HubCallerContext context, string email = null);

        /// <summary>
        /// Finds user details in mappings
        /// </summary>
        /// <param name="userId">User Name or FullName or HubId or ConnectionId</param>
        /// <returns></returns>
        public UserDetail FindUser(string userId);
        
        #endregion

        #region User Actions

        /// <summary>
        /// Notifications when user joins chat
        /// Could be done in OnConnectedAsync event
        /// </summary>
        /// <param name="context">Hub context</param>
        /// <param name="clients">Client collection</param>
        Task UserJoinedAsync(HubCallerContext context, IHubCallerClients clients);

        /// <summary>
        /// Handle user incoming messages
        /// </summary>
        /// <param name="context">Hub context</param>
        /// <param name="clients">Client collection</param>
        Task SendMessageAsync(HubCallerContext context, IHubCallerClients clients, string message);

        /// <summary>
        /// Gets connected users list formatted for chat output
        /// </summary>
        /// <param name="context">Hub context</param>
        /// <param name="clients">Client collection</param>
        /// <returns>User list formatted string</returns>
        string GetUsersListString(HubCallerContext context, IHubCallerClients clients);

        #endregion
    }
}