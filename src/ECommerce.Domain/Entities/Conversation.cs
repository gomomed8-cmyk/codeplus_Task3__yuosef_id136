using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    public class Conversation : Entity
    {
        public int CustomerId { get; private set; }
        public ConversationStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }

        public Customer Customer { get; private set; } = default!;

        public ICollection<Message> Messages { get; private set; }
            = new List<Message>();

        private Conversation()
        {
        }

        public Conversation(int customerId)
        {
            CustomerId = customerId;
            Status = ConversationStatus.Open;
            CreatedAt = DateTime.UtcNow;
        }
    }
}