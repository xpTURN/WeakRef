using System;
using NUnit.Framework;
using xpTURN.WeakRef;

namespace xpTURN.WeakRef.Tests
{
    public class WeakEventTests
    {
        [Test]
        public void SubscribeRaise_InvokesHandler()
        {
            var evt = new WeakEvent();
            var handler = new BoolHandler();
            evt.Subscribe(handler.OnRaise);
            evt.Raise();
            Assert.That(handler.Called, Is.True);
        }

        [Test]
        public void Unsubscribe_DoesNotInvoke()
        {
            var evt = new WeakEvent();
            var handler = new BoolHandler();
            evt.Subscribe(handler.OnRaise);
            evt.Unsubscribe(handler.OnRaise);
            evt.Raise();
            Assert.That(handler.Called, Is.False);
        }

        [Test]
        public void MultipleHandlers_AllInvoked()
        {
            var evt = new WeakEvent();
            var counter = new CounterHandler();
            evt.Subscribe(counter.Inc1);
            evt.Subscribe(counter.Inc2);
            evt.Raise();
            Assert.That(counter.Count, Is.EqualTo(2));
        }

        [Test]
        public void Raise_PrunesDeadReferences()
        {
            var evt = new WeakEvent();
            var liveHandler = new BoolHandler();
            evt.Subscribe(liveHandler.OnRaise);
            SubscribeWithShortLivedTarget(evt);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            evt.Raise();
            Assert.That(liveHandler.Called, Is.True);
        }

        [Test]
        public void Subscribe_Null_ThrowsArgumentNullException()
        {
            var evt = new WeakEvent();
            Assert.That(() => evt.Subscribe(null), Throws.ArgumentNullException.With.Property("ParamName").EqualTo("handler"));
        }

        [Test]
        public void Subscribe_Lambda_ThrowsArgumentException()
        {
            var evt = new WeakEvent();
            Assert.That(() => evt.Subscribe(() => { }), Throws.ArgumentException.With.Message.Contain("Lambda"));
        }

        private static void SubscribeWithShortLivedTarget(WeakEvent evt)
        {
            var holder = new ActionSubscriber();
            evt.Subscribe(holder.OnRaise);
        }

        private class BoolHandler
        {
            public bool Called;
            public void OnRaise() => Called = true;
        }

        private class CounterHandler
        {
            public int Count;
            public void Inc1() => Count++;
            public void Inc2() => Count++;
        }

        private class ActionSubscriber
        {
            public void OnRaise() { }
        }
    }

    public class WeakEventOfTTests
    {
        [Test]
        public void SubscribeRaise_InvokesHandler_WithArg()
        {
            var evt = new WeakEvent<int>();
            var handler = new IntHandler();
            evt.Subscribe(handler.OnRaise);
            evt.Raise(42);
            Assert.That(handler.Received, Is.EqualTo(42));
        }

        [Test]
        public void Unsubscribe_DoesNotInvoke()
        {
            var evt = new WeakEvent<int>();
            var handler = new IntHandler { Received = -1 };
            evt.Subscribe(handler.OnRaise);
            evt.Unsubscribe(handler.OnRaise);
            evt.Raise(99);
            Assert.That(handler.Received, Is.EqualTo(-1));
        }

        [Test]
        public void MultipleHandlers_AllInvoked()
        {
            var evt = new WeakEvent<int>();
            var sumHandler = new SumHandler();
            evt.Subscribe(sumHandler.Add1);
            evt.Subscribe(sumHandler.Add2);
            evt.Raise(3);
            Assert.That(sumHandler.Sum, Is.EqualTo(6));
        }

        [Test]
        public void Raise_PrunesDeadReferences()
        {
            var evt = new WeakEvent<int>();
            var liveHandler = new IntHandler { Received = -1 };
            evt.Subscribe(liveHandler.OnRaise);
            SubscribeWithShortLivedTarget(evt);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            evt.Raise(7);
            Assert.That(liveHandler.Received, Is.EqualTo(7));
        }

        [Test]
        public void Subscribe_Null_ThrowsArgumentNullException()
        {
            var evt = new WeakEvent<int>();
            Assert.That(() => evt.Subscribe(null), Throws.ArgumentNullException.With.Property("ParamName").EqualTo("handler"));
        }

        [Test]
        public void Subscribe_Lambda_ThrowsArgumentException()
        {
            var evt = new WeakEvent<int>();
            Assert.That(() => evt.Subscribe(x => { }), Throws.ArgumentException.With.Message.Contain("Lambda"));
        }

        private static void SubscribeWithShortLivedTarget(WeakEvent<int> evt)
        {
            var holder = new ActionSubscriber();
            evt.Subscribe(holder.OnRaise);
        }

        private class IntHandler
        {
            public int Received;
            public void OnRaise(int x) => Received = x;
        }

        private class SumHandler
        {
            public int Sum;
            public void Add1(int x) => Sum += x;
            public void Add2(int x) => Sum += x;
        }

        private class ActionSubscriber
        {
            public void OnRaise(int value) { }
        }
    }
}
