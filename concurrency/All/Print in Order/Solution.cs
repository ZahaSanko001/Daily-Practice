using System.Threading;

public class Foo {

    public readonly SemaphoreSlim _second = new SemaphoreSlim(0);
    public readonly SemaphoreSlim _third = new SemaphoreSlim(0);

    public void First(Action printFirst) {
        printFirst();
        _second.Release();
    }

    public void Second(Action printSecond) {
        _second.Wait();
        printSecond();
        _third.Release();
    }

    public void Third(Action printThird) {
        _third.Wait();
        printThird();
    }
}