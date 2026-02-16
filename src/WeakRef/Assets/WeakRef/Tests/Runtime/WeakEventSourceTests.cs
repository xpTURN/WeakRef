using System;
using NUnit.Framework;
using xpTURN.WeakRef;

namespace xpTURN.WeakRef.Tests
{
    public class WeakEventSourceTests
    {
        [Test]
        public void SubscribeRaise_InvokesHandler()
        {
            var evt = new WeakEventSource<EventArgs>();
            var sender = new object();
            var spy = new EventHandlerSpy();
            evt.Subscribe(spy.OnEvent);
            evt.Raise(sender, EventArgs.Empty);
            Assert.That(spy.Called, Is.True);
            Assert.That(spy.ReceivedSender, Is.SameAs(sender));
        }

        [Test]
        public void SubscribeRaise_WithCustomEventArgs_InvokesHandler()
        {
            var evt = new WeakEventSource<MyEventArgs>();
            var args = new MyEventArgs { Value = 42 };
            var handler = new MyEventArgsHandler();
            evt.Subscribe(handler.OnEvent);
            evt.Raise(this, args);
            Assert.That(handler.ReceivedValue, Is.EqualTo(42));
        }

        [Test]
        public void Unsubscribe_DoesNotInvoke()
        {
            var evt = new WeakEventSource<EventArgs>();
            var handler = new EventHandlerSpy();
            evt.Subscribe(handler.OnEvent);
            evt.Unsubscribe(handler.OnEvent);
            evt.Raise(this, EventArgs.Empty);
            Assert.That(handler.Called, Is.False);
        }

        [Test]
        public void MultipleHandlers_AllInvoked()
        {
            var evt = new WeakEventSource<EventArgs>();
            var counter = new EventCountHandler();
            evt.Subscribe(counter.OnEvent1);
            evt.Subscribe(counter.OnEvent2);
            evt.Raise(this, EventArgs.Empty);
            Assert.That(counter.Count, Is.EqualTo(2));
        }

        [Test]
        public void Raise_PrunesDeadReferences()
        {
            var evt = new WeakEventSource<EventArgs>();
            var liveHandler = new EventHandlerSpy();
            evt.Subscribe(liveHandler.OnEvent);
            SubscribeWithShortLivedTarget(evt);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            evt.Raise(this, EventArgs.Empty);
            Assert.That(liveHandler.Called, Is.True);
        }

        [Test]
        public void Subscribe_Lambda_ThrowsArgumentException()
        {
            var evt = new WeakEventSource<EventArgs>();
            Assert.That(() => evt.Subscribe((s, e) => { }), Throws.ArgumentException.With.Message.Contain("Lambda"));
        }

        private static void SubscribeWithShortLivedTarget(WeakEventSource<EventArgs> evt)
        {
            var holder = new EventSubscriber();
            evt.Subscribe(holder.OnEvent);
        }

        private class EventHandlerSpy
        {
            public bool Called { get; private set; }
            public object ReceivedSender { get; private set; }
            public void OnEvent(object sender, EventArgs e)
            {
                Called = true;
                ReceivedSender = sender;
            }
        }

        private class MyEventArgsHandler
        {
            public int ReceivedValue { get; private set; }
            public void OnEvent(object sender, MyEventArgs e) => ReceivedValue = e.Value;
        }

        private class EventCountHandler
        {
            public int Count;
            public void OnEvent1(object sender, EventArgs e) => Count++;
            public void OnEvent2(object sender, EventArgs e) => Count++;
        }

        private class MyEventArgs : EventArgs
        {
            public int Value { get; set; }
        }

        private class EventSubscriber
        {
            public void OnEvent(object sender, EventArgs e) { }
        }
    }
}
