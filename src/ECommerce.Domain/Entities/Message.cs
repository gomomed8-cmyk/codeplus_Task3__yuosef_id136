using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities
{
    public class Message : Entity
    {
        public int ConversationId { get; private set; }
        public int? SenderId { get; private set; }
        public MessageType SenderType { get; private set; }
        public string Content { get; private set; } = default!;
        public DateTime SentAt { get; private set; }
        public bool IsRead { get; private set; }

        public Conversation Conversation { get; private set; } = default!;

        private Message()
        {
        }

        public Message(
            int conversationId,
            int? senderId,
            MessageType senderType,
            string content)
        {
            if (conversationId <= 0)
                throw new DomainException("Invalid Conversation ID.");

            if (string.IsNullOrWhiteSpace(content))
                throw new DomainException("Message content cannot be empty.");

            ConversationId = conversationId;
            SenderId = senderId;
            SenderType = senderType;
            Content = content.Trim();
            SentAt = DateTime.UtcNow;
            IsRead = false;
        }
    }
}