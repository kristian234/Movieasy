### What is Movieasy?
- 

### Architecture
The backend of Movieasy adheres to the principles and practices of **clean architecture**.

![alt text](https://pbs.twimg.com/media/F92a6qvXYAA6i8K?format=png&name=4096x4096 "Logo Title Text 1")


### Used technologies
- Next js
- React
- Typescript
- CSS
- TailwindCSS
- ASP.NET
- PostgreSQL
- Keycloak
- Entity Framework Core
- NextAuth js
- SignalR
- Cloudinary (for storing images)  
- NSubstitute
- MediatR
- FluentValidation
- xUnit
- Redis (soon, in the next 2-3 days)

### Movieasy as is, is dependent on the following services:
- [Cloudinary](https://cloudinary.com/)
- [Keycloak](https://www.keycloak.org/)
  
------------

### How to launch?
- The provided docker-compose, has all the containers except next-app in development mode. There is a production ready/ish docker-compose, but it contains real keys keys and information I can't safely share. That being said, even in development mode, the local performance is extremely with next-app in production mode.
- The provided docker-compose and configs seed the database when in development mode with an admin user, with the following credentials:
  email: admin@gmail.com
  password: admin
  
**More coming soon, to be continued**
