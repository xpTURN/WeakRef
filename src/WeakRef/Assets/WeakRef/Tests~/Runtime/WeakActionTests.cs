using System;
using NUnit.Framework;
using xpTURN.WeakRef;

namespace xpTURN.WeakRef.Tests
{
    public class WeakActionTests
    {
        [Test]
        public void IsAlive_WhenTargetAlive_ReturnsTrue()
        {
            var target = new Handler();
            var wa = new WeakAction(target.OnCalled);
            Assert.That(wa.IsAlive, Is.True);
        }

        [Test]
        public void Invoke_WhenTargetAlive_InvokesAction()
        {
            var target = new Handler();
            var wa = new WeakAction(target.OnCalled);
            wa.Invoke();
            Assert.That(target.Called, Is.True);
        }

        [Test]
        public void Invoke_WhenTargetCollected_DoesNotThrow()
        {
            var wa = CreateWeakActionWithShortLivedTarget();
            ForceGcAndWait();
            Assert.DoesNotThrow(() => wa.Invoke());
        }

        [Test]
        public void IsAlive_WhenTargetCollected_ReturnsFalse()
        {
            var wa = CreateWeakActionWithShortLivedTarget();
            ForceGcAndWait();
            Assert.That(wa.IsAlive, Is.False);
        }

        [Test]
        public void Constructor_Null_ThrowsArgumentNullException()
        {
            Assert.That(() => new WeakAction(null), Throws.ArgumentNullException.With.Property("ParamName").EqualTo("action"));
        }

        [Test]
        public void Constructor_Lambda_ThrowsArgumentException()
        {
            Assert.That(() => new WeakAction(() => { }), Throws.ArgumentException.With.Message.Contain("Lambda"));
        }

        private static WeakAction CreateWeakActionWithShortLivedTarget()
        {
            var holder = new Handler();
            return new WeakAction(holder.OnCalled);
        }

        private static void ForceGcAndWait()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
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
            public void OnCalled() => Called = true;
        }
    }

    public class WeakActionOfTTests
    {
        [Test]
        public void IsAlive_WhenTargetAlive_ReturnsTrue()
        {
            var target = new IntHandler();
            var wa = new WeakAction<int>(target.OnRaise);
            Assert.That(wa.IsAlive, Is.True);
        }

        [Test]
        public void Invoke_WhenTargetAlive_InvokesWithArg()
        {
            var target = new IntHandler();
            var wa = new WeakAction<int>(target.OnRaise);
            wa.Invoke(42);
            Assert.That(target.Received, Is.EqualTo(42));
        }

        [Test]
        public void Invoke_WhenTargetCollected_DoesNotThrow()
        {
            var wa = CreateWeakActionWithShortLivedTarget();
            ForceGcAndWait();
            Assert.DoesNotThrow(() => wa.Invoke(0));
        }

        [Test]
        public void Constructor_Null_ThrowsArgumentNullException()
        {
            Assert.That(() => new WeakAction<int>(null), Throws.ArgumentNullException.With.Property("ParamName").EqualTo("action"));
        }

        [Test]
        public void Constructor_Lambda_ThrowsArgumentException()
        {
            Assert.That(() => new WeakAction<int>(x => { }), Throws.ArgumentException.With.Message.Contain("Lambda"));
        }

        private static WeakAction<int> CreateWeakActionWithShortLivedTarget()
        {
            var holder = new IntHandler();
            return new WeakAction<int>(holder.OnRaise);
        }

        private static void ForceGcAndWait()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            for (int i = 0; i < 3; i++)
            {
                _ = new byte[10000];
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();
            }
        }

        private class IntHandler
        {
            public int Received;
            public void OnRaise(int x) => Received = x;
        }
    }
}
