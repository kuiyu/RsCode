using RsCode.Domain;

namespace WebApplicationDemo
{
    public class UserCreateEvent:IDomainEvent
    {
        public UserCreateEvent(string name)
        {
            UserName = name;
        }
        public string UserName { get;private set; }
    }

    class UserCreateEventHandler : DomainEventHandler<UserCreateEvent>
    {
        public Task Handle(UserCreateEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(notification.UserName); 
            return Task.CompletedTask;
        }
    }
}
