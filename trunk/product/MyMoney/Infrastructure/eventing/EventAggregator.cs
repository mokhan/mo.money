using System.Collections.Generic;
using System.Linq;
using MoMoney.Utility.Extensions;

namespace MoMoney.Infrastructure.eventing
{
    public interface IEventAggregator
    {
        void subscribe_to<Event>(IEventSubscriber<Event> subscriber) where Event : IEvent;
        void publish<Event>(Event the_event_to_broadcast) where Event : IEvent;
        void publish<Event>() where Event : IEvent, new();
    }

    public class EventAggregator : IEventAggregator
    {
        readonly IDictionary<string, List<object>> subscribers;

        public EventAggregator()
        {
            subscribers = new Dictionary<string, List<object>>();
        }

        public void subscribe_to<Event>(IEventSubscriber<Event> subscriber) where Event : IEvent
        {
            if (!get_list_for<Event>().Contains(subscriber))
            {
                get_list_for<Event>().Add(subscriber);
            }
        }

        public void publish<Event>(Event the_event_to_broadcast) where Event : IEvent
        {
            get_list_for<Event>()
                .Select(x => x.downcast_to<IEventSubscriber<Event>>())
                .each(x => x.notify(the_event_to_broadcast));
        }

        public void publish<Event>() where Event : IEvent, new()
        {
            publish(new Event());
        }

        List<object> get_list_for<Event>()
        {
            if (!subscribers.ContainsKey(typeof (Event).FullName))
            {
                subscribers.Add(typeof (Event).FullName, new List<object>());
            }
            return subscribers[typeof (Event).FullName];
        }
    }
}