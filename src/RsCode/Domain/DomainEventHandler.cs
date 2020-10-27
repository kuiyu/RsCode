using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Domain
{
    public interface DomainEventHandler<TDomainEvent>:INotificationHandler<TDomainEvent> 
        where TDomainEvent:IDomainEvent
    {
    }
}
