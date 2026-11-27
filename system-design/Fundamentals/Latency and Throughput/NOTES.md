### Latency and Throughput ###
Latency measures how long a single request takes to complete, while throughput measures how much work or data a systems processes over a given period.

**Latency**
Times elapsed since sending a request to recieving a response. Lower is better interactive feel
*hardware reads latency*
Send 1 KB bytes over 1 Gbps network     10,000   ns       10 us
Read 4 KB randomly from SSD*           150,000   ns      150 us 
Read 1 MB sequentially from memory     250,000   ns      250 us
Read 1 MB sequentially from SSD*     1,000,000   ns    1,000 us    1 ms 
HDD seek                            10,000,000   ns   10,000 us   10 ms
Read 1 MB sequentially from HDD     30,000,000   ns   30,000 us   30 ms 
**Throughput**
Volume of requests, transaction, or data processed per unit time. 
**Tail Metrics**
Latency is tracked with P50, P95, and P99 to spot slow outliers. Throughput is bounded by the system's tightest bottleneck and maximum link bandwidth.

### How they interact ###
**Little's Law** 
Average requests in the system equal throughput multiplied by average time spent in the system (L = λ × W).
**Batching and Queues**
Grouping requests or queuing increases throughput at scale, but often raises per-request latency.
*To lower latency* 
Cache data closer to users, reduce network round trips, and tune slow database queries.
*To raise throughput*
Scale horizontally across more nodes, parallelize work, and remove bottlenecks.

### Back-of-the-envelop calculations ###

Latency Comparison Numbers
-----------------------------------------------------------------------
L1 cache reference                           0.5 ns
Branch mispredict                            5   ns
L2 cache reference                           7   ns                      14x L1 cache
Mutex lock/unlock                           25   ns
Main memory reference                      100   ns                      20x L2 cache, 200x L1 cache
Compress 1K bytes with Zippy            10,000   ns       10 us
Send 1 KB bytes over 1 Gbps network     10,000   ns       10 us
Read 4 KB randomly from SSD*           150,000   ns      150 us          ~1GB/sec SSD
Read 1 MB sequentially from memory     250,000   ns      250 us
Round trip within same datacenter      500,000   ns      500 us
Read 1 MB sequentially from SSD*     1,000,000   ns    1,000 us    1 ms  ~1GB/sec SSD, 4X memory
HDD seek                            10,000,000   ns   10,000 us   10 ms  20x datacenter roundtrip
Read 1 MB sequentially from 1 Gbps  10,000,000   ns   10,000 us   10 ms  40x memory, 10X SSD
Read 1 MB sequentially from HDD     30,000,000   ns   30,000 us   30 ms 120x memory, 30X SSD
Send packet CA->Netherlands->CA    150,000,000   ns  150,000 us  150 ms
-----------------------------------------------------------------------
Notes

1 ns = 10^-9 seconds
1 us = 10^-6 seconds = 1,000 ns
1 ms = 10^-3 seconds = 1,000 us = 1,000,000 ns

Handy metrics based on numbers above:

Read sequentially from HDD at 30 MB/s
Read sequentially from 1 Gbps Ethernet at 100 MB/s
Read sequentially from SSD at 1 GB/s
Read sequentially from main memory at 4 GB/s
6-7 world-wide round trips per second
2,000 round trips per second within a data center