using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SignalR.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public class UserDetail
        {
            public string Name { get; set; }
            public string FullName { get; set;}
            public string HubId { get; set; }
            public string ConnectionId { get; set; }
            public override string ToString() => $"{Name}: {HubId} ({ConnectionId})";
        }

        #region Identity User -> Hub User Mapping
        
        public static Dictionary<string, UserDetail> Users = new Dictionary<string, UserDetail>();

        public override Task OnConnectedAsync()
        {
            lock (Users)
            {
                Users.Add(Context.ConnectionId, new UserDetail()
                {
                    FullName =Context.User.Identity.Name,
                    Name = GetName(Context.User.Identity.Name),
                    HubId = Context.UserIdentifier,
                    ConnectionId = Context.ConnectionId
                });
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            lock (Users)
            {
                Users.Remove(Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }

        #endregion

        /// <summary>
        /// Massage sending and command handling
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string message)
        {
            #region Commands

            // Private message
            if (message.StartsWith("/pm:", StringComparison.InvariantCultureIgnoreCase))
            {
                var regex = new Regex(@"^\/pm\:\s*([^\s]+)\s+(.*)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                var match = regex.Match(message);
                if (match.Success)
                {
                    var userId = match.Groups[1].Value;
                    var userName = userId;
                    
                    // Find our user
                    var found = Users.FirstOrDefault(u => u.Value.Name.Equals(userId) || u.Value.FullName.Equals(userId));
                    if (found.Value != null)
                    {
                        userId = found.Value.HubId;
                        userName = found.Value.Name;
                    }

                    await Clients.Users(userId, Context.UserIdentifier).SendAsync("ReceiveMessage", DateTime.Now.ToString("G"), $"{GetName()} -> {userName}", match.Groups[2].Value);
                    return;
                }
            }
            // User list
            else if (message.Equals("/users", StringComparison.InvariantCultureIgnoreCase))
            {
                await Clients.Caller.SendAsync("ReceiveMessage", DateTime.Now.ToString("G"), null, $"Users:\n{string.Join('\n', Users.Select(u => u.Value))}");
                return;
            }
            
            #endregion

            //todo Should store message for later
            await Clients.All.SendAsync("ReceiveMessage", DateTime.Now.ToString("G"), GetName(), message);
        }

        /// <summary>
        /// Join notifications, could be moved to OnConneted
        /// </summary>
        /// <returns></returns>
        public async Task Joined()
        {
            var name = GetName();
            await Clients.Others.SendAsync("ReceiveMessage", DateTime.Now.ToString("G"), null, $"{name} came to scare us!");
            await Clients.Caller.SendAsync("ReceiveMessage", DateTime.Now.ToString("G"), name, "Lock and loaded!");
        }

        /// <summary>
        /// Get nice username without @ and rest.
        /// </summary>
        /// <param name="name">Provided user name</param>
        /// <returns></returns>
        private string GetName(string name = null)
        {
            name ??= Context.User.Identity.Name;
            return name.Split('@')[0];
        }
    }
}