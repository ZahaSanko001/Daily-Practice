*Semaphore Solution*

```
using System;
using System.Threading;

public class ZeroEvenOdd {
    private int n;
    private readonly SemaphoreSlim _zero = new SemaphoreSlim(1, 1);
    private readonly SemaphoreSlim _odd  = new SemaphoreSlim(0, 1);
    private readonly SemaphoreSlim _even = new SemaphoreSlim(0, 1);

    public ZeroEvenOdd(int n) {
        this.n = n;
    }

    public void Zero(Action<int> printNumber) {
        for (int i = 1; i <= n; i++) {
            _zero.Wait();
            printNumber(0);
            if (i % 2 == 0) _even.Release();
            else            _odd.Release();
        }
    }

    public void Even(Action<int> printNumber) {
        for (int i = 2; i <= n; i += 2) {
            _even.Wait();
            printNumber(i);
            _zero.Release();
        }
    }

    public void Odd(Action<int> printNumber) {
        for (int i = 1; i <= n; i += 2) {
            _odd.Wait();
            printNumber(i);
            _zero.Release();
        }
    }
}
```