## Availability and Reliability ##
Availability measures whether a system is up and reachable when needed, while reliability measures whether it performs correctly without failure over time. Together, teams define targets with SLIs, SLOs, and SLAs, quantify uptime in "nines," and survive failures using redundancy and failover.

**SLI** 
Service Level Indicator(SLI), the actual metrics measured like latency, error rate, successful request per minute.
**SLO**
Service Level Objective(SLO), the internal target for an SLI over a period. For instance let's say we need 99.9% of requests to succeed under 300 ms over 300 days.
**SLA**
Service Level Agreement(SLA), the external promise to customers, backed by financial credits or penalties if breached.

## Nines of Availability ##
Availability is often expressed as percentages with repeating nines. Each extra nine reduces allowed downtime by roughly 10× and increases engineering cost and complexity.
> 99% (two nines): ~3.65 days of downtime per year
> 99.9% (three nines): ~8 hours 46 minutes per year (~43.8 minutes/month)
> 99.99% (four nines): ~52 minutes per year
> 99.999% (five nines): ~5 minutes 15 seconds per year

## Redundancy and Failover& ##
**Redundancy**
Duplicating critical components—such as servers, availability zones, network links, or databases—so there is no single point of failure. Parallel components raise combined availability, while sequential dependencies multiply failure risk and lower it.
**Failover** 
Switching traffic or workload execution to a backup component when a primary one fails, usually via health checks and automated routing. Common models include active-active (all nodes serve traffic simultaneously) and active-passive (standby nodes take over on failure).