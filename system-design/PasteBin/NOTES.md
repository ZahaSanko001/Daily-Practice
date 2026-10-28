# Use Cases
- Users can enter their paste's url and view content
- Users can write content and generate a url for it
---
# Constraint and Assumptions
- Users are anonymous
- pastes have an expiration time
- the service is always available 
- 10 million writes per month
- 100 million reads per month
- 10:1 read/write ratio
- `shortlink` 7 bytes
- `paste_path` 255 bytes
- `expiration` 8 bytes
- `created_at` 8 bytes
- `content` 1 KB
## back-of-the-envelope calculations
*storage*
per paste size 1 KB + (7 + 255 + 8 + 8) bytes = 1.278 KB ~ 1.28
per month data generated 1.28 * 10 mill = 12.8 GB
per year data generation 12.8 GB * 12 = 153.6 GB ~154 GB
In 5 years data generated = 770 GB
*throughput*
read per second 100 mill / (30 * 24 * 60 * 60) = 38.58 ~ 39 reads
writes per second 10 mill / (30 * 24 * 60 * 60) = 3.858 ~ 4 writes
1 MB read sequentially from memory takes 250 microseconds, on SSD that's *4 and on HDD *120 
per second 39 * 1.28 = 49.92 ~ 50 KB reads
per second 4 * 1.28 = 5.12 KB writes
## High Level Design
Client -> Web server -> Read API, Write API -> SQL, Storage
Analytics -> SQL, Storage
# Core Components design
## Use Case: User puts a block of texts and generates a random shortlink
- User sends create request to web server, web server sends it to write API, API puts it into database and storage.
### table
`shortlink char(7) not null 
 expiration datetime not null 
 created_at datetime not null 
 paste_path varchar(255) not null
 PRIMARY KEY(shortlink)`
`shortlink` is the primary key, database creates the index based on that, we also create additional indices with `created_at` and `paste_path` for quicker lookups.
### `shortlink` generation  
- Use user's ip_address + timestamp to generate an MD5 key
- Generate a `shortlink` through a Base64 function using the MD5 key
- checks for duplicates in database
    - if found re-generate a new `shortlink`
    - else store it into the database
## Use Case: User enters a shortlink and read the content
- User sends get request to web server 
- web server sends it to read API 
- API searches the database for it
    - if found send back response with it
    - else send back a not found response
## Use Case: Service Tracks analytics of pages
# API design 
` Post /pastes
  {
    Content,
    ip,
    timestamp,
  }
  returns
  {
    status: 201,
    url,
  }

  Get /pastes/{shortlink}
  returns
  {
    status: 200, 404, 410,
    content,
  }
## Service deletes expired urls
- Service checks database periodically with `created_at` 
    - If current date exceeds the expiration delete it
    - else do nothing


# This is my first attempt, need more in depth understanding on availability, load balancer, caching, database scaling, limits and abuse.