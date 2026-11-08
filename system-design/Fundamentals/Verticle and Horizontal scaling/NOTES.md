Scaling is required for increased worlkloads. Based on the project a scalable architecture must be chosen, either between `Vertical Scaling` or `Horizontal Scaling`. Both more or less increases the capacity(resouces) for the system. In the most intuitive term projects with less data should be scaled vertically and projects with a lot of data should be scaled horizontally.

Scaling itself doesn't guarantee availability and fault tolerance since it only relates to the approach on increasing resources as data increases.

# Vertical Scaling 
Vertical scaling(scaling up) increases a single machine resources(CPU, RAM, ...). The same server is upgraded instead of adding more servers.
   - Advantages: Less server means less maintanance, With a single server upgrade architecture also needs less changing, good applications that do not yet require distributed deployment.
   - Disadvantages: Upgrading will cause downtime because of server restart/replacement, No failure isolation, if machine breaks the server goes down completely.
# Horizontal Scaling
Horizontal scaling(scaling out) increases the number of servers. Multiple instances share the workload, a load balancer is commonly used to distribute incoming traffic, The application and its dependencies must support distributed operation.
    Advantages: Way higher capacity with more flexible growth, there's an availablity potential here since trafic can be redirected to healthy servers.
    Disadvantages: Higher complexity, replication, synchronization, and consistancy becomes more complex, more network overhead, more latency, maintanance becomes more complex.
# When to choose what
`Vertical`: Simplicity is important, the workload can still be handled efficiently by a larger machine, distributed-system complexity is unnecessary, fast capacity improvement is required with minimal architectural changes.
`Horizontal`: Workload growth may exceed the capacity of a single machine, high traffic must be distributed across multiple instances, redundancy and flexible capacity expansion are important, the application can operate correctly across multiple nodes.