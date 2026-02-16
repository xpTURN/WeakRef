using System;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

using xpTURN.WeakRef;

namespace xpTURN.WeakRef.Tests
{
    public class WeakDelegateTests
    {
        [Test]
        public void InstanceMethod_IsAlive_WhenTargetAlive_ReturnsTrue()
        {
            var target = new Handler();
            var wd = new WeakDelegate<Action>(target.OnCalled);
            Assert.That(wd.IsAlive, Is.True);
        }

        [Test]
        public void InstanceMethod_TryGetDelegate_WhenTargetAlive_ReturnsInvokableDelegate()
        {
            var target = new Handler();
            var wd = new WeakDelegate<Action>(target.OnCalled);
            var d = wd.TryGetDelegate();
            Assert.That(d, Is.Not.Null);
            d();
            Assert.That(target.Called, Is.True);
        }

        [Test]
        public void InstanceMethod_WithArg_TryGetDelegate_InvokesCorrectly()
        {
            var target = new Handler();
            var wd = new WeakDelegate<Action<int>>(target.SetValue);
            var d = wd.TryGetDelegate();
            Assert.That(d, Is.Not.Null);
            d(42);
            Assert.That(target.Value, Is.EqualTo(42));
        }

        [Test]
        public void InstanceMethod_Action_int_int_TryGetDelegate_InvokesCorrectly()
        {
            var target = new Handler();
            var wd = new WeakDelegate<Action<int, int>>(target.AddTwo);
            var d = wd.TryGetDelegate();
            Assert.That(d, Is.Not.Null);
            d(10, 20);
            Assert.That(target.Value, Is.EqualTo(30));
        }

        [Test]
        public void InstanceMethod_Action_int_int_int_TryGetDelegate_InvokesCorrectly()
        {
            var target = new Handler();
            var wd = new WeakDelegate<Action<int, int, int>>(target.AddThree);
            var d = wd.TryGetDelegate();
            Assert.That(d, Is.Not.Null);
            d(10, 20, 30);
            Assert.That(target.Value, Is.EqualTo(60));
        }

        [Test]
        public void StaticMethod_IsAlive_ReturnsTrue()
        {
            var wd = new WeakDelegate<Action>(StaticHandler.NoOp);
            Assert.That(wd.IsAlive, Is.True);
        }

        [Test]
        public void StaticMethod_TryGetDelegate_ReturnsInvokableDelegate()
        {
            var wd = new WeakDelegate<Action>(StaticHandler.NoOp);
            var d = wd.TryGetDelegate();
            Assert.That(d, Is.Not.Null);
            Assert.DoesNotThrow(() => d());
        }

        [Test]
        public void Lambda_ThrowsArgumentException()
        {
            Action lambda = () => { };
            Assert.That(() => new WeakDelegate<Action>(lambda),
                Throws.ArgumentException.With.Message.Contain("Lambda").And.Message.Contain("anonymous"));
        }

        /// <summary>Verifies that TryGetDelegate returns null when the target is collected. GC is non-deterministic so retries may be needed.</summary>
        [Test]
        public void TryGetDelegate_WhenTargetCollected_ReturnsNull()
        {
            var wd = CreateWeakDelegateWithShortLivedTarget();
            ForceGcAndWait();
            var d = wd.TryGetDelegate();
            Assert.That(d, Is.Null, "Target may not have been collected (GC is non-deterministic).");
        }

        private static WeakDelegate<Action<int>> CreateWeakDelegateWithShortLivedTarget()
        {
            var holder = new Handler();
            return new WeakDelegate<Action<int>>(holder.SetValue);
        }

        /// <summary>Repeatedly allocates and collects to increase the chance of GC running.</summary>
        private static void ForceGcAndWait()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            // 수집 유도: 임시 객체 다수 생성 후 수집
            for (int i = 0; i < 3; i++)
            {
                _ = new byte[10000];
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();
            }
        }

        private class Handler
        {
            public bool Called;
            public int Value;
            public void OnCalled() => Called = true;
            public void SetValue(int x) => Value = x;
            public void AddTwo(int a, int b) => Value = a + b;
            public void AddThree(int a, int b, int c) => Value = a + b + c;
        }

        private static class StaticHandler
        {
            public static void NoOp() { }
        }
    }

    /// <summary>Verifies WeakDelegate behavior when the target is a UnityEngine.Object (UnityWeakReference compatibility).</summary>
    public class WeakDelegateUnityObjectTests
    {
        [Test]
        public void UnityObjectTarget_WhenAlive_IsAliveReturnsTrue()
        {
            var obj = ScriptableObject.CreateInstance<UnityHandler>();
            var wd = new WeakDelegate<Action>(obj.OnCalled);
            Assert.That(wd.IsAlive, Is.True);
            Object.DestroyImmediate(obj);
        }

        [Test]
        public void UnityObjectTarget_WhenAlive_TryGetDelegateInvokes()
        {
            var obj = ScriptableObject.CreateInstance<UnityHandler>();
            var wd = new WeakDelegate<Action>(obj.OnCalled);
            var d = wd.TryGetDelegate();
            Assert.That(d, Is.Not.Null);
            d();
            Assert.That(obj.Called, Is.True);
            Object.DestroyImmediate(obj);
        }

        [Test]
        public void UnityObjectTarget_WhenDestroyed_IsAliveReturnsFalse()
        {
            var obj = ScriptableObject.CreateInstance<UnityHandler>();
            var wd = new WeakDelegate<Action>(obj.OnCalled);
            Object.DestroyImmediate(obj);
            Assert.That(wd.IsAlive, Is.False);
        }

        [Test]
        public void UnityObjectTarget_WhenDestroyed_TryGetDelegateReturnsNull()
        {
            var obj = ScriptableObject.CreateInstance<UnityHandler>();
            var wd = new WeakDelegate<Action>(obj.OnCalled);
            Object.DestroyImmediate(obj);
            var d = wd.TryGetDelegate();
            Assert.That(d, Is.Null);
        }

        private class UnityHandler : ScriptableObject
        {
            public bool Called;
            public void OnCalled() => Called = true;
        }
    }
}
