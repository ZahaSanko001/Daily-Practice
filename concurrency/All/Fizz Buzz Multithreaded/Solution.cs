using System.Threading;

public class FizzBuzz {
    private int n;
    SemaphoreSlim _f = new(0);
    SemaphoreSlim _b = new(0);
    SemaphoreSlim _fb = new(0);
    SemaphoreSlim _n = new(1);

    public FizzBuzz(int n) {
        this.n = n;
    }

    // printFizz() outputs "fizz".
    public void Fizz(Action printFizz) {
        for (int i = 3; i <= n; i += 3) {
            if (i % 5 != 0) {
                _f.Wait();
                printFizz(); 
                _n.Release();
            } 
        }
    }

    // printBuzzz() outputs "buzz".
    public void Buzz(Action printBuzz) {
        for (int i = 5; i <= n; i += 5) {
            if (i % 3 != 0) {
                _b.Wait();
                printBuzz(); 
                _n.Release();
            } 
        }
    }

    // printFizzBuzz() outputs "fizzbuzz".
    public void Fizzbuzz(Action printFizzBuzz) {
        for (int i = 15; i <= n; i += 15) {
                _fb.Wait();
                printFizzBuzz(); 
                _n.Release();
        }
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Number(Action<int> printNumber) {
        for (int i = 1; i <= n; i++) {
            _n.Wait();
            if (i % 15 == 0) {
                _fb.Release();
            } else if (i % 3 == 0) {
                _f.Release();
            } else if (i % 5 == 0) {
                _b.Release();
            } else {
                printNumber(i);
                _n.Release();
            }
        }

        _f.Release();
        _b.Release();
        _fb.Release();
    }
}