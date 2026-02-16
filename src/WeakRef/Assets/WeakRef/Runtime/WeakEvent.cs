using System;
using System.Collections.Generic;

namespace xpTURN.WeakRef
{
    /// <summary>
    /// Manages event handlers with weak references.
    /// </summary>
    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent
    {
        private List<WeakDelegate<Action>> _handlers = new();
        private List<WeakDelegate<Action>> _raiseSnapshot = new();

        public void Subscribe(Action handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action>(handler));
        }

        public void Unsubscribe(Action handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent operator +(WeakEvent evt, Action handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent operator -(WeakEvent evt, Action handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise()
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke();
            }
        }
    }
    
    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1>
    {
        private List<WeakDelegate<Action<T1>>> _handlers = new();
        private List<WeakDelegate<Action<T1>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1>>(handler));
        }

        public void Unsubscribe(Action<T1> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1> operator +(WeakEvent<T1> evt, Action<T1> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1> operator -(WeakEvent<T1> evt, Action<T1> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1);
            }
        }
    }

    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1, T2>
    {
        private List<WeakDelegate<Action<T1, T2>>> _handlers = new();
        private List<WeakDelegate<Action<T1, T2>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1, T2> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1, T2>>(handler));
        }

        public void Unsubscribe(Action<T1, T2> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1, T2> operator +(WeakEvent<T1, T2> evt, Action<T1, T2> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1, T2> operator -(WeakEvent<T1, T2> evt, Action<T1, T2> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1, T2 arg2)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1, arg2);
            }
        }
    }

    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1, T2, T3>
    {
        private List<WeakDelegate<Action<T1, T2, T3>>> _handlers = new();
        private List<WeakDelegate<Action<T1, T2, T3>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1, T2, T3> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1, T2, T3>>(handler));
        }

        public void Unsubscribe(Action<T1, T2, T3> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1, T2, T3> operator +(WeakEvent<T1, T2, T3> evt, Action<T1, T2, T3> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1, T2, T3> operator -(WeakEvent<T1, T2, T3> evt, Action<T1, T2, T3> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1, T2 arg2, T3 arg3)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1, arg2, arg3);
            }
        }
    }

    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1, T2, T3, T4>
    {
        private List<WeakDelegate<Action<T1, T2, T3, T4>>> _handlers = new();
        private List<WeakDelegate<Action<T1, T2, T3, T4>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1, T2, T3, T4> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1, T2, T3, T4>>(handler));
        }

        public void Unsubscribe(Action<T1, T2, T3, T4> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1, T2, T3, T4> operator +(WeakEvent<T1, T2, T3, T4> evt, Action<T1, T2, T3, T4> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1, T2, T3, T4> operator -(WeakEvent<T1, T2, T3, T4> evt, Action<T1, T2, T3, T4> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1, arg2, arg3, arg4);
            }
        }
    }

    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1, T2, T3, T4, T5>
    {
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5>>> _handlers = new();
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1, T2, T3, T4, T5> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1, T2, T3, T4, T5>>(handler));
        }

        public void Unsubscribe(Action<T1, T2, T3, T4, T5> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1, T2, T3, T4, T5> operator +(WeakEvent<T1, T2, T3, T4, T5> evt, Action<T1, T2, T3, T4, T5> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1, T2, T3, T4, T5> operator -(WeakEvent<T1, T2, T3, T4, T5> evt, Action<T1, T2, T3, T4, T5> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1, arg2, arg3, arg4, arg5);
            }
        }
    }

    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1, T2, T3, T4, T5, T6>
    {
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6>>> _handlers = new();
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1, T2, T3, T4, T5, T6> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1, T2, T3, T4, T5, T6>>(handler));
        }

        public void Unsubscribe(Action<T1, T2, T3, T4, T5, T6> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1, T2, T3, T4, T5, T6> operator +(WeakEvent<T1, T2, T3, T4, T5, T6> evt, Action<T1, T2, T3, T4, T5, T6> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1, T2, T3, T4, T5, T6> operator -(WeakEvent<T1, T2, T3, T4, T5, T6> evt, Action<T1, T2, T3, T4, T5, T6> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }
    }

    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1, T2, T3, T4, T5, T6, T7>
    {
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7>>> _handlers = new();
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1, T2, T3, T4, T5, T6, T7> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7>>(handler));
        }

        public void Unsubscribe(Action<T1, T2, T3, T4, T5, T6, T7> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1, T2, T3, T4, T5, T6, T7> operator +(WeakEvent<T1, T2, T3, T4, T5, T6, T7> evt, Action<T1, T2, T3, T4, T5, T6, T7> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1, T2, T3, T4, T5, T6, T7> operator -(WeakEvent<T1, T2, T3, T4, T5, T6, T7> evt, Action<T1, T2, T3, T4, T5, T6, T7> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
        }
    }

    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8>>> _handlers = new();
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1, T2, T3, T4, T5, T6, T7, T8> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8>>(handler));
        }

        public void Unsubscribe(Action<T1, T2, T3, T4, T5, T6, T7, T8> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8> operator +(WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8> evt, Action<T1, T2, T3, T4, T5, T6, T7, T8> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8> operator -(WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8> evt, Action<T1, T2, T3, T4, T5, T6, T7, T8> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
        }
    }

    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>> _handlers = new();
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>(handler));
        }

        public void Unsubscribe(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9> operator +(WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9> evt, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9> operator -(WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9> evt, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
        }
    }

    /// <remarks>This type is not thread-safe. Subscribe, Unsubscribe, and Raise must be used from the same thread or synchronized by the caller.</remarks>
    public class WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>> _handlers = new();
        private List<WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>> _raiseSnapshot = new();

        public void Subscribe(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(new WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(handler));
        }

        public void Unsubscribe(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> handler) =>
            _handlers.RemoveAll(wd => { var d = wd.TryGetDelegate(); return d == null || d == handler; });

        public static WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> operator +(WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> evt, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> handler) { evt?.Subscribe(handler); return evt; }
        public static WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> operator -(WeakEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> evt, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> handler) { evt?.Unsubscribe(handler); return evt; }

        public void Raise(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            _raiseSnapshot.Clear();
            foreach (var wd in _handlers)
                if (wd.IsAlive) _raiseSnapshot.Add(wd);
            _handlers.RemoveAll(wd => !wd.IsAlive);
            foreach (var weakDelegate in _raiseSnapshot)
            {
                var handler = weakDelegate.TryGetDelegate();
                handler?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
        }
    }
}
