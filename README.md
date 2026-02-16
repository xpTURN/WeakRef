# WeakRef

When subscribing to delegates and events in Unity/C#, **publishers hold strong references to subscriber instances**, so even after unsubscribing the GC cannot collect them, and "ghost callbacks" plus memory leaks often occur—e.g. callbacks still firing after scene changes or object destruction.

This package wraps delegates, `Action`, and events in **weak references**, so when the target is **collected by the GC (plain C# objects)** or **destroyed in Unity (`UnityEngine.Object`)**, it is automatically treated as "dead."

**What it does**

- **Reduces memory leaks**: If you forget to unsubscribe, references disappear once the target instance is gone, so it becomes eligible for GC.
- **Prevents ghost callbacks**: No invocations on destroyed Unity objects or already-collected instances.
- **Unity-aware**: `UnityEngine.Object` is considered "dead" at `Destroy()` time (via `UnityWeakReference<T>`, respecting Unity null semantics).

**Provided types**: `UnityWeakReference<T>`, `WeakDelegate<TDelegate>`, `WeakAction`/`WeakAction<T1..T10>`, `WeakEvent`/`WeakEvent<T1..T10>`, `WeakEventSource<TEventArgs>`. See the Type Summary and sections below for details.

---

## Cautions

- **No lambdas or anonymous methods**: Weak references track only the **delegate target instance**. Lambdas and anonymous methods target compiler-generated closures, not the intended "subscriber" instance. Use **method groups** or **named methods** only.
  - e.g. `Subscribe(handler.OnRaise)` ✅ / `Subscribe(() => handler.OnRaise())` ❌
- **Not thread-safe**: `WeakEvent`, `WeakEventSource`, etc. do not assume concurrent multi-threaded access. Synchronize subscribe/unsubscribe/raise from other threads at the call site.
- **Unity-only type**: `UnityWeakReference<T>` supports only `T : UnityEngine.Object`. For plain C# objects use .NET `WeakReference<T>` or this package's `WeakDelegate`/`WeakAction`.
- **Cleanup on Raise**: In `WeakEvent`/`WeakEventSource`, "dead" subscribers are removed from the list **when Raise is called**. If Raise is infrequent, dead references may remain until then.

---

## Installing the WeakRef Package (for Unity 2021.3 or later)

1. Open Window > Package Manager
2. Click the '+' button > Select "Add package from git URL..."
3. Enter the following URL:

```text
https://github.com/xpTURN/WeakRef.git?path=src/WeakRef/Assets/WeakRef
```

---

## Type Summary

| Type | Purpose |
|------|---------|
| **UnityWeakReference&lt;T&gt;** | Weak reference for `UnityEngine.Object` only. Treats destroyed objects as "dead" using Unity null semantics. |
| **WeakDelegate&lt;TDelegate&gt;** | Holds the delegate target weakly. `TryGetDelegate()` returns the delegate only when the target is alive. |
| **WeakAction** / **WeakAction&lt;T1..T10&gt;** | Holds an `Action` or `Action<T1..T10>` weakly. `Invoke()` runs only when the target is alive. |
| **WeakEvent** / **WeakEvent&lt;T1..T10&gt;** | Weak event based on `Action`. Subscribers that are GC'd or destroyed are removed on Raise. |
| **WeakEventSource&lt;TEventArgs&gt;** | Weak event based on `EventHandler<TEventArgs>`. Sender/args pattern. |

---

## UnityWeakReference&lt;T&gt;

- **Constraint**: `T : UnityEngine.Object` (plain C# objects cannot be used).
- **Role**: Makes `IsAlive`, `Target`, and `TryGetTarget` behave as "not present" when the Unity object has been destroyed.
- **Members**: `IsAlive`, `Target`, `TryGetTarget(out T target)`, `SetTarget(T target)`.

```csharp
var go = new GameObject();
var wr = new UnityWeakReference<GameObject>(go);
Debug.Assert(wr.IsAlive && wr.TryGetTarget(out var t));

Destroy(go);
Debug.Assert(!wr.IsAlive && !wr.TryGetTarget(out _));
```

---

## WeakDelegate&lt;TDelegate&gt;

- **Role**: Holds only the delegate target weakly. When the target is GC'd (plain) or destroyed (Unity object), `TryGetDelegate()` returns null.
- **Restriction**: Lambda and anonymous methods are not allowed. Use method groups or named delegates only.
- **Unity**: When the target is a `UnityEngine.Object`, uses `UnityWeakReference` internally so destroyed objects are treated as dead (`#if UNITY_2017_1_OR_NEWER`).

**Members**

- `IsAlive`: true if the method is static or the target is alive.
- `TryGetDelegate()`: Returns the delegate when alive, null otherwise.

```csharp
var handler = new MyHandler();
var wd = new WeakDelegate<Action>(handler.OnCalled);
var d = wd.TryGetDelegate();
d?.Invoke();
// When handler is GC'd (plain) or destroyed (Unity object), TryGetDelegate() == null
```

---

## WeakAction / WeakAction&lt;T1..T10&gt;

- **Role**: Holds an `Action` or `Action<T1..T10>` weakly. `Invoke()` runs only when the target is alive; otherwise no-op.
- **Restriction**: Lambda and anonymous methods are not allowed. Uses `WeakDelegate` internally.
- **Overloads**: No-arg `WeakAction`, one-arg `WeakAction<T1>` … up to `WeakAction<T1..T10>`.

**Members**: `IsAlive`, `Invoke()` / `Invoke(arg1, ...)` (up to 10 args).

```csharp
var wa = new WeakAction(handler.OnRaise);
wa.Invoke(); // Runs if target is alive; otherwise does nothing
```

---

## WeakEvent / WeakEvent&lt;T1..T10&gt;

- **Role**: Subscribe, unsubscribe, and raise multiple `Action` handlers with weak references. Dead references are removed on Raise.
- **Restriction**: Lambda and anonymous methods are not allowed. Not thread-safe (same thread or caller synchronization required).
- **Overloads**: No-arg `WeakEvent`, one-arg `WeakEvent<T1>` … up to `WeakEvent<T1..T10>`.

**Members**: `Subscribe`, `Unsubscribe`, `Raise` / `Raise(arg1, ...)`, `+` / `-` operators.

```csharp
var evt = new WeakEvent();
evt.Subscribe(handler.OnRaise);
evt.Raise();
evt.Unsubscribe(handler.OnRaise);
```

---

## WeakEventSource&lt;TEventArgs&gt;

- **Role**: Weak event following the `EventHandler<TEventArgs>` pattern. Passes sender and `TEventArgs`.
- **Restriction**: Lambda and anonymous methods are not allowed. Not thread-safe.

**Members**: `Subscribe`, `Unsubscribe`, `Raise(object sender, TEventArgs args)`, `+` / `-` operators.

```csharp
var evt = new WeakEventSource<EventArgs>();
evt.Subscribe(handler.OnEvent);
evt.Raise(this, EventArgs.Empty);
```

## Links

- **Changelog**: [CHANGELOG](./CHANGELOG.md)
- **License**: [LICENSE](./LICENSE.md)
- **Author**: [xpTURN](https://github.com/xpTURN)
