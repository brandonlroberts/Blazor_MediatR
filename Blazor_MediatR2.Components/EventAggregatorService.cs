namespace Blazor_MediatR2.Components
{
    public class EventAggregatorService : IEventAggregatorService
    {
        private Dictionary<string, List<Action<object>>> _eventHandlers;

        public EventAggregatorService()
        {
            _eventHandlers = new Dictionary<string, List<Action<object>>>();
        }

        public void Subscribe(string eventName, Action<object> eventHandler)
        {
            if (!_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers[eventName] = new List<Action<object>>();
            }
            _eventHandlers[eventName].Add(eventHandler);
        }

        public void Unsubscribe(string eventName, Action<object> eventHandler)
        {
            if (_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers[eventName].Remove(eventHandler);
            }
        }

        public async Task PublishAsync(string eventName)
        {
            await PublishAsync(eventName, null!);
        }

        public async Task PublishAsync(string eventName, object eventData)
        {
            if (_eventHandlers.ContainsKey(eventName))
            {
                var handlers = _eventHandlers[eventName].ToList();

                foreach (var handler in handlers)
                {
                    await Task.Run(() => handler.Invoke(eventData));
                }
            }
        }
    }
}
