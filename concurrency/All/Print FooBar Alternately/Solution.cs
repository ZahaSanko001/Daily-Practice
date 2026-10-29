using System.Threading;

public class FooBar {
    private int n;
    public readonly SemaphoreSlim _foo = new SemaphoreSlim(0);
    public readonly SemaphoreSlim _bar = new SemaphoreSlim(1);

    public FooBar(int n) {
        this.n = n;
    }

    public void Foo(Action printFoo) {
        
        for (int i = 0; i < n; i++) {
            _bar.Wait();
        	// printFoo() outputs "foo". Do not change or remove this line.
        	printFoo();
            _foo.Release();
        }
    }

    public void Bar(Action printBar) {
        
        for (int i = 0; i < n; i++) {
            _foo.Wait();
            // printBar() outputs "bar". Do not change or remove this line.
        	printBar();
            _bar.Release();
        }
    }
}