using MediatR;

namespace MonthlyCosts.Domain.Core.Events
{
    public abstract class Message : IRequest<Unit>
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}