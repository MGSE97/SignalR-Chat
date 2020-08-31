using ChatCore.Hubs;
using ChatCore.Services;
using ChatCore.Services.MessageHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ChatCore
{
    public static class Startup
    {
        /// <summary>
        /// Will add all used services by chat
        /// </summary>
        /// <param name="services">Service collection</param>
        public static void UseChat(this IServiceCollection services)
        {
            // Core services
            services.AddSignalR();

            // Chat services
            // Chat service can be both singleton and scoped. We use singleton to limit reflection search to once.
            services.AddSingleton<IChatService, ChatService>();
            //services.AddScoped<IChatService, ChatService>();

            // Register message handlers for dependency injection (optional)
            services.AddScoped<IMessageHandler, SimpleMessageHandler>();
            services.AddScoped<IMessageHandler, UserListMessageHandler>();
            services.AddScoped<IMessageHandler, PrivateMessageHandler>();
        }

        /// <summary>
        /// Will register endpoint hub for chat
        /// </summary>
        /// <param name="endpoint">Endpoint builder</param>
        public static void UseChat(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapHub<ChatHub>("/chathub");
        }
    }
}
