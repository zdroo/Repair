using Repair.Domain.Common;

namespace Repair.Infrastructure.Outbox
{
    public class OutboxMessage : BaseEntity
    {
        public DateTime OccurredOn { get; set; }
        public string Type { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? ProcessedOn { get; set; }

        public OutboxMessage(DateTime occurredOn, string type, string content)
        {
            OccurredOn = occurredOn;
            Type = type;
            Content = content;
        }

        // EF
        protected OutboxMessage() { }
    }

}
