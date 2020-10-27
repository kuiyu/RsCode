using PetaPoco;
using PetaPoco.Core;
using System.Collections.Generic;

namespace RsCode.Domain.Aggregate
{

    public abstract class Entity : IEntity
    {
       

        #region domain event
     
        private List<IDomainEvent> _domainEvents;
        [Ignore]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        #endregion

    }

     
    public abstract class Entity<TPrimaryKey>:Entity,IEntity<TPrimaryKey>
    {
     
       
    }
}
