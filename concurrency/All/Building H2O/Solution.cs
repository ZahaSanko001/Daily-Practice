using System.Threading;

public class H2O {
    private readonly SemaphoreSlim _h = new SemaphoreSlim(2, 2);
    private readonly SemaphoreSlim _o = new SemaphoreSlim(1, 1);
    private readonly Barrier _barrier = new Barrier(3);
    private readonly object _lock = new object();

    public H2O() {
        
    }

    public void Hydrogen(Action releaseHydrogen) {
		_h.Wait();
        _barrier.SignalAndWait();
        // releaseHydrogen() outputs "H". Do not change or remove this line.
        lock(_lock) releaseHydrogen();
        _h.Release();
    }

    public void Oxygen(Action releaseOxygen) {
        _o.Wait();
        _barrier.SignalAndWait();
        // releaseOxygen() outputs "O". Do not change or remove this line.
		lock(_lock) releaseOxygen();
        _o.Release();
    }
}