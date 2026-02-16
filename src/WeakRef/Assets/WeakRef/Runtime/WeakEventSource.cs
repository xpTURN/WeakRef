using System;
using System.Collections.Generic;

namespace xpTURN.WeakRef
{
    /// <summary>
    /// Manages event handlers with weak references.
    /// </summary>
    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEventSource<TEventArgs> where TEventArgs : EventArgs
    {
        private List<WeakDelegate<EventHandler<TEventArgs>>> _handlers = new();
        private List<WeakDelegate<EventHandler<TEventArgs>>> _raiseSnapshot = new();

        public void Subscribe(EventHandler<TEventArgs> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<EventHandler<TEventArgs>>(handler));
        }

        public void Unsubscribe(EventHandler<TEventArgs> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEventSource<TEventArgs> operator +(WeakEventSource<TEventArgs> evt, EventHandler<TEventArgs> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEventSource<TEventArgs> operator -(WeakEventSource<TEventArgs> evt, EventHandler<TEventArgs> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(object sender, TEventArgs args)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(sender, args);
            }
        }
    }
}
