**Statfull Applications**

A statefull application stores the interaction with a client and maintains it. The server keeps track of past interation/session and maintains that information as a state. These states/sessions can be returned to, such as online banking or email or open world video games. These are performed using the context of the previous sessions. Because of this, statefull applications use the same servers each time they process a request from the user. If a statefull transaction is interrupted, the context and history are stored and can be picked 

In less fancy words, in a statefull application some of the session information is retained in the server memory/storage, this information can only be used if the request flow goes through this server. A statefull server can have a database, but some of the session data that is used for the actual application interaction is stored in the server's memory/storage, this causes the flow to be strictly bound to that server, other servers don't know of this specific session.

*Some good examples are*

 - User-centric applications: User-centric applications like social media apps and  e-commerce sites keep track of a logged-in user’s session, including their preferences or the items in their shopping cart.

 - IoT systems: An Internet of Things (IoT) system works by continuously sending, receiving, and analyzing data in a feedback loop. Based on historical or real-time data, a device will respond to a changing state over time, like your home thermostat.

 - AI/ML model training: Artificial intelligence and machine learning (AI/ML) training models learn from data and remember that data. The maintained state of learning and remembering helps models function at their fullest capacity.

One thing to remember is Statefullness is not solely a UX specific thing, like banking transaction, open world games. It does do that, however one other way to look at it is through the network layer `TCP protocol`, they don't care about user experience; they care about packet delivery. TCP maintains a stateful connection (handshake, sequence numbers) to ensure data arrives reliably and in the correct order. Same with OS and Hardwares.


**Statless Applications**

A stateless application or process does not retain information about the user's previous interactions, meaning there's no stored knowledge of or reference to past transactions. Each transaction is made as if from scratch for the first time.
 
*Some good examples are*

 - REST APIs: REST application programming interface (APIs) transfer a representation of the state of a resource to the requester or endpoint. Each API request is separate and the server does not store information about the previous request.
 - Microservices: Microservices allow each core function within an application to exist independently. Because of that, microservices are well suited to both stateless and stateful applications.
 - Serverless architectures: Serverless architectures are designed to respond to events in isolation and do not retain context from previous actions and therefore do not need to maintain state. Serverless architectures are ideal for asynchronous, stateless apps that can be started instantaneously.
 - Large language models (LLMs): At a technical level, LLMs operate in a stateless manner during inference. The model weights are fixed, and they don't change between each request. 
 
*Scalability*

Stateless applications are generally more scalable, as each request is independent and can be handled by any available server using load balancing. Stateful applications have tightly coupled instances making it more difficult to scale. They may require more specific instances or pods in Kubernetes to manage state, load balance, and manage sessions.

*Fault tolerance*

Stateless applications can be more fault-tolerant, as the loss of a server doesn't impact user sessions. In stateful applications, the loss of a server can result in the loss of session data unless additional measures, such as session replication or clustering, are in place.