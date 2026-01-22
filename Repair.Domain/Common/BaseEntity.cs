namespace Repair.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        private readonly List<object> _domainEvents = new();

        public IReadOnlyCollection<object> DomainEvents => _domainEvents;

        protected void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
