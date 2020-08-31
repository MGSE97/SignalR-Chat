namespace ChatCore.Models
{
    /// <summary>
    /// Represents user mapping between Identity and SignalR
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// User nickname
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// User full name (Email]
        /// </summary>
        public string FullName { get; set;}

        /// <summary>
        /// Hub user Id (Should be same for all connections done by same user)
        /// </summary>
        public string HubUserId { get; set; }

        /// <summary>
        /// Hub connection Id (One user can have multiple connections)
        /// </summary>
        public string ConnectionId { get; set; }

        public override string ToString() => $"{Name}: {HubUserId} ({ConnectionId})";
    }
}
