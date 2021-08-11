using MediatR;
using Serendip.IK.Commands;
using Serendip.IK.EventManager;
using Serendip.IK.Services.Abstractions;
using System.Threading.Tasks;

namespace Serendip.IK.Services.Concrete
{
    public class PublisherManager : IPublisherService
    {
        private readonly IEventService eventService;
        private readonly IMediator mediator;

        public PublisherManager(IEventService eventService, IMediator mediator)
        {
            this.eventService = eventService;
            this.mediator = mediator;
        }
        public Task Publish<TEntity>(string eventName, TEntity entity, bool doRunWorkFlow = true)
        {
            var command = new NotificationCommand { EventParameter = eventService.GetEventParameter(eventName, entity) };
            command.DoRunWorkFlow = doRunWorkFlow;
            mediator.Publish(command);
            return Task.CompletedTask;
        }
    }
}
