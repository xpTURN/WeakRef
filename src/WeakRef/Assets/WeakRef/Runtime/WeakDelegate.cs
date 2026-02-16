using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace xpTURN.WeakRef
{
    /// <summary>
    /// Holds the delegate target in a weak reference. When no strong reference to the target remains, it becomes eligible for GC and <see cref="TryGetDelegate"/> returns null.
    /// When the target is a <see cref="UnityEngine.Object"/>, uses <see cref="UnityWeakReference{T}"/> so that IsAlive and TryGetDelegate behave correctly after the object is destroyed.
    /// Lambda and anonymous methods are not allowed, since they cannot be removed by the same instance on Unsubscribe.
    /// </summary>
    public class WeakDelegate<TDelegate> where TDelegate : Delegate
    {
        private readonly System.WeakReference _targetRef;
        private readonly MethodInfo _method;

        public bool IsAlive => _method.IsStatic || _targetRef?.IsAlive == true;

        public WeakDelegate(TDelegate @delegate)
        {
            if (@delegate == null)
                throw new ArgumentNullException(nameof(@delegate));

            var method = @delegate.Method;
            var declaringType = method.DeclaringType;
            if (declaringType != null && declaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length > 0)
                throw new ArgumentException("Lambda and anonymous methods are not allowed. Use a method group or a named delegate.", nameof(@delegate));

            if (@delegate.Target != null)
            {
#if UNITY_2017_1_OR_NEWER                
                _targetRef = @delegate.Target is UnityEngine.Object unityObj
                    ? new UnityWeakReference<UnityEngine.Object>(unityObj)
                    : new System.WeakReference(@delegate.Target);
#else
                _targetRef = new System.WeakReference(@delegate.Target);
#endif
                _method = method;
            }
            else
            {
                _targetRef = null;
                _method = method;
            }
        }

        /// <summary>
        /// Tries to get the delegate and returns it if successful.
        /// </summary>
        public TDelegate TryGetDelegate()
        {
            if (_method.IsStatic)
            {
                return (TDelegate)Delegate.CreateDelegate(typeof(TDelegate), _method);
            }

            object target = _targetRef?.IsAlive == true ? _targetRef.Target : null;
            if (target != null)
            {
                return (TDelegate)Delegate.CreateDelegate(typeof(TDelegate), target, _method);
            }

            return null;
        }
    }
}
