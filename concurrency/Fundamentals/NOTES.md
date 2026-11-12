**Semaphore/SemphoreSlim**
Semaphore and SemaphoreSlim are thread counters. They count how any threads can be executed at once. 
`new SemaphoreSlim(initialcount: , maxcount: )`
The `initialcount` checks how many slots/permits are available to start(this is only a counter), `maxcount` checks how many slots are allowed. 
Works as the Club/Bouncer analogy.
`.Wait()` decrements the count or waits until another thread releases. 
`.Release()` increments the count 'releasing' a slot/permit for another thread.

**lock**
lock give mutual exlusion. It blocks other threads and let one thread at a time to execute inside the body.
requires an object initialization, uses that object as it's argument.
`lock(object) { // code }`

**Barrier**
A rendezvous point, waits for a specified number of threads to arrive.
`new Barrier(__numebr_of_threads__)`
`.SignalAndWait()` waits for a specified number of threads to arrive, once they are in it resests.