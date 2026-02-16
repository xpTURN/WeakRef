#if UNITY_2017_1_OR_NEWER                
using UnityEngine;

namespace xpTURN.WeakRef
{
    /// <summary>
    /// Holds a <see cref="UnityEngine.Object"/> in a weak reference.
    /// </summary>
    public class UnityWeakReference<T> : System.WeakReference where T : UnityEngine.Object
    {
        public UnityWeakReference(T target) : base(target) { }

        /// <summary>
        /// Checks if the target is alive.
        /// </summary>
        public override bool IsAlive
        {
            get
            {
                // UnityEngine.Object has special null-check semantics, so we cast and check explicitly.
                T obj = Target as T;
                return obj != null;
            }
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public new T Target
        {
            get
            {
                T target = (base.Target as T);
                if (target != null) return target;

                return null;
            }

            set
            {
                base.Target = value;
            }
        }

        /// <summary>
        /// Sets the target.
        /// </summary>
        public void SetTarget(T target)
        {
            base.Target = target;
        }

        /// <summary>
        /// Tries to get the target and returns true if successful.
        /// </summary>
        public bool TryGetTarget(out T target)
        {
            T t = base.Target as T;
            target = t;
            return t != null;
        }
    }
}
#endif