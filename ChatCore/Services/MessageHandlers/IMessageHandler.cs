using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ChatCore.Services.MessageHandlers
{
    /// <summary>
    /// Simple message handling interface, since we don`t want big if singularity
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Handler order in processing
        /// </summary>
        uint Order { get; }

        /// <summary>
        /// Handles message. If false, we will use another handler.
        /// </summary>
        /// <param name="context">Hub context</param>
        /// <param name="clients">Client collection</param>
        /// <param name="message">User message</param>
        /// <returns></returns>
        Task<bool> Handle(HubCallerContext context, IHubCallerClients clients, string message);
    }
}