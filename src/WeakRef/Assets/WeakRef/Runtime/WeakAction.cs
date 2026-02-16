using System;

namespace xpTURN.WeakRef
{
    /// <summary>
    /// Holds the target of an <see cref="Action"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction
    {
        private readonly WeakDelegate<Action> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action action)
        {
            _inner = new WeakDelegate<Action>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action if the target is alive; otherwise does nothing.</summary>
        public void Invoke()
        {
            _inner.TryGetDelegate()?.Invoke();
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1>
    {
        private readonly WeakDelegate<Action<T1>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1> action)
        {
            _inner = new WeakDelegate<Action<T1>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg)
        {
            _inner.TryGetDelegate()?.Invoke(arg);
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1, T2}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1, T2>
    {
        private readonly WeakDelegate<Action<T1, T2>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1, T2> action)
        {
            _inner = new WeakDelegate<Action<T1, T2>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg1, T2 arg2)
        {
            _inner.TryGetDelegate()?.Invoke(arg1, arg2);
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1, T2, T3}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1, T2, T3>
    {
        private readonly WeakDelegate<Action<T1, T2, T3>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1, T2, T3> action)
        {
            _inner = new WeakDelegate<Action<T1, T2, T3>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            _inner.TryGetDelegate()?.Invoke(arg1, arg2, arg3);
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1, T2, T3, T4}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1, T2, T3, T4>
    {
        private readonly WeakDelegate<Action<T1, T2, T3, T4>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1, T2, T3, T4> action)
        {
            _inner = new WeakDelegate<Action<T1, T2, T3, T4>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            _inner.TryGetDelegate()?.Invoke(arg1, arg2, arg3, arg4);
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1, T2, T3, T4, T5}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1, T2, T3, T4, T5>
    {
        private readonly WeakDelegate<Action<T1, T2, T3, T4, T5>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1, T2, T3, T4, T5> action)
        {
            _inner = new WeakDelegate<Action<T1, T2, T3, T4, T5>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            _inner.TryGetDelegate()?.Invoke(arg1, arg2, arg3, arg4, arg5);
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1, T2, T3, T4, T5, T6}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1, T2, T3, T4, T5, T6>
    {
        private readonly WeakDelegate<Action<T1, T2, T3, T4, T5, T6>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1, T2, T3, T4, T5, T6> action)
        {
            _inner = new WeakDelegate<Action<T1, T2, T3, T4, T5, T6>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            _inner.TryGetDelegate()?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1, T2, T3, T4, T5, T6, T7}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1, T2, T3, T4, T5, T6, T7>
    {
        private readonly WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            _inner = new WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            _inner.TryGetDelegate()?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        private readonly WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            _inner = new WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            _inner.TryGetDelegate()?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        private readonly WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            _inner = new WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            _inner.TryGetDelegate()?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }
    }

    /// <summary>
    /// Holds the target of an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> weakly; invokes it via <see cref="Invoke"/> only when the target is alive. Lambda and anonymous methods are not allowed.
    /// </summary>
    public class WeakAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        private readonly WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> _inner;

        public bool IsAlive => _inner.IsAlive;

        public WeakAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            _inner = new WeakDelegate<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(action ?? throw new ArgumentNullException(nameof(action)));
        }

        /// <summary>Invokes the action with the given argument if the target is alive; otherwise does nothing.</summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            _inner.TryGetDelegate()?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }
    }
}
