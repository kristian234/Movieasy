### What is Movieasy?
- Movieasy is a platform to share your opinion on movies and/or see others' opinion and find something to watch.

### Features (this list is to expand even more by 17th of April)
<pre>
   User:
    - register and login
    - search for movies by genre/movie title/actor 
    - use the search filters
    - leave a review consisting of a comment and star rating on any movie (except ones who are to be released)
    - receive live notifications of a newly added movies
    - edit their already posted reviews at any time
    - see details about every movie
    - watch the trailer of every movie
  Admin:
    - everything that the user can
    - add/edit/delete movie/s
    - add/edit/delete genre/s
    - add/edit/delete actor/s
</pre>

------------

### Architecture
The backend of Movieasy adheres to the principles and practices of **clean architecture**.

![alt text](https://pbs.twimg.com/media/F92a6qvXYAA6i8K?format=png&name=4096x4096 "Logo Title Text 1")

------------

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
- Redis (for caching)
- Yup 
- React Hook Form 

### Movieasy as is, is dependent on the following services:
- [Cloudinary](https://cloudinary.com/)
- [Keycloak](https://www.keycloak.org/)
  
------------

### How to launch?
- The provided docker-compose, has all the containers except next-app in development mode. There is a production ready/ish docker-compose, but it contains real keys keys and information I can't safely share. That being said, even in development mode, the local performance is extremely with next-app in production mode.
- The provided docker-compose and configs seed the database when in development mode with an admin user, with the following credentials:
  email: admin@gmail.com
  password: admin
- The appsettings.Development.json file is not included as it contains a lot of sensitive information, if you wish to get it please message me. (I've already given it in the softuni form)

------------

### ERD (without the keycloak tables)
![alt text](https://github.com/kristian234/Movieasy/blob/master/images/erd1.jpg)

### Tests
- Domain layer tests
![alt text](https://github.com/kristian234/Movieasy/blob/master/images/domain-tests.jpg)

**More coming soon, to be continued**
