using System;

namespace Microsoft.BotBuilderSamples.Data.Models
{
    public class BotInfo
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string? ConversationId { get; set; }
        public string? ServiceUrl { get; set; }
    }
}
