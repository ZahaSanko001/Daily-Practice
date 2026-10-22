Using Semaphore is the simplest solution. Two semaphores for the second and third thread, both are at 0 counter so they can't execute immediately upon arrival and kept sleeping for order if their block is reached. SemaphoreSlim is used for no reason here but because it's good for async stuff hence the usage.

The second solution is a bit more code with Motion and lock. lock keeps other threads from entering until the current block finishes, Monitor is use in a while loop to make the current thread sleep and release all other threads if the current thread is out of order, and once the thread is executed all other threads are released.

```
using System.Threading;

public class Foo {

    public readonly object _lock = new object();
    public int step = 0;

    public void First(Action printFirst) {
        lock(_lock) {
            printFirst();
            step = 1;
            Monitor.PulseAll(_lock);
        }
    }

    public void Second(Action printSecond) {
        lock(_lock) {
            while (step != 1) {
                Monitor.Wait(_lock);
            }
            printSecond();
            step = 2;
            Monitor.PulseAll(_lock);
        }
    }

    public void Third(Action printThird) {
        lock(_lock) {
            while (step != 2) {
                Monitor.Wait(_lock);
            }        
            printThird();
        }
    }
}

```