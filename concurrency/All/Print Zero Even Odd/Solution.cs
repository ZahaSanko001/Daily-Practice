using System.Threading;

public class ZeroEvenOdd {
    private int n;
    public readonly AutoResetEvent _zero = new AutoResetEvent(true);
    public readonly AutoResetEvent _odd = new AutoResetEvent(false);
    public readonly AutoResetEvent _even = new AutoResetEvent(false);
    int current = 0;
    
    public ZeroEvenOdd(int n) {
        this.n = n;
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Zero(Action<int> printNumber) {
        while (true) {
            _zero.WaitOne();
            current++;
            if (current <= n) {
                printNumber(0);
                var gate = (current % 2 == 0) ? _even  :  _odd;
                gate.Set();
            }
            else {
                _even.Set();
                _odd.Set();
                break;
            }
        }
    }

    public void Even(Action<int> printNumber) {
        while(true) {
            _even.WaitOne();
            if (current <= n) {
                printNumber(current);
                _zero.Set();
            }
            else break;
        }
    }

    public void Odd(Action<int> printNumber) {
        while (true) {
            _odd.WaitOne();
            if (current <= n) {
                printNumber(current);
                _zero.Set();
            }
            else break;
        }
    }
}