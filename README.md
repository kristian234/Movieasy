### What is Movieasy?
- Movieasy is a platform to share your opinion on movies and/or see others' opinion and find something to watch.

### Features
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
    + some more
   Admin:
    - everything that the user can
    - add/edit/delete movie/s
    - add/edit/delete genre/s
    - add/edit/delete actor/s
    + some more
</pre>

------------

### Architecture
The backend of Movieasy adheres to the principles and practices of **clean architecture**.

![alt text](https://pbs.twimg.com/media/F92a6qvXYAA6i8K?format=png&name=4096x4096 "Logo Title Text 1")

------------

### Api
When you launch the api with the provided configuration and docker-compose it'll open a swagger window on port 5001, there you can find more information about the api.

![alt text](https://github.com/kristian234/Movieasy/blob/master/images/swagger.png)

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
- NetArchTest
- Framer-Motion

### Movieasy as is, is dependent on the following services:
- [Cloudinary](https://cloudinary.com/)
- [Keycloak](https://www.keycloak.org/)
  
------------

### How to launch?
- The provided docker-compose, has all the containers except next-app in development mode. There is a production ready/ish docker-compose, but it contains real keys keys and information I can't safely share. That being said, even in development mode, the local performance is extremely fast with next-app in production mode.
- The provided docker-compose and configs seed the database when in development mode with an admin user, with the following credentials:
  email: admin@gmail.com
  password: admin
- The appsettings.Development.json file is not included as it contains a lot of sensitive information, if you wish to get it please message me. (I've already given it in the softuni form)
- If you decide to launch it yourself, and wish to be able to delete/edit the movies that are initially seeded, please make sure you change the cloudinary image URLs to match your own in the seed.json located in the infrastructure layer. You can still test by adding your own movies though.
- If you're launching the application you'll notice it requires a aspnetapp.pfx (self signed https certificate) in the docker-compose.override.yml. This is to ensure the api runs on https which is important in this case. For security reasons I am unable to upload my self signed certicate for others to use, so please generate one as shown in here https://learn.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide and move it to  Movieasy.Api/Certificates (ignore the temp file that's already in there). Make sure you give it a password of "movieasy", you can also change it in docker-compose.override.yml
- PS: If it seems like the certificate is not working restart your computer and if that doesn't work redo the docker volumes 
  
- If you for some reason decide to do the certificate validation differently, you can find all the needed information in docker-compose.override.yml. I've found that just trusting a certificate without actually moving it into the certificates folder yields inconsistent results on startup with docker, so please use any other way on your own risk or as an alternative if none of the above worked somehow (I tried it on 2 different systems and it did).
(specifically those 2 lines):
      - ASPNETCORE_Kestrel__Certificates__Default__Password=movieasy
      - ASPNETCORE_Kestrel__Certificates__Default__Path=Certificates/aspnetapp.pfx
  
------------

### ERD (without the keycloak tables)
![alt text](https://github.com/kristian234/Movieasy/blob/master/images/erd1.jpg)

------------

### Tests
![alt text](https://github.com/kristian234/Movieasy/blob/master/images/domain-tests.jpg)
![alt text](https://github.com/kristian234/Movieasy/blob/master/images/application-tests.jpg)

------------

### Pictures
- I am unable to upload all of these photos on this github page as it'll turn it into a mess but here are the pages on imgur
- https://imgur.com/a/gYHvcdJ

- In case you were wondering how it scales and looks on mobile (it was designed to scale well, at least in my opinion)
- https://imgur.com/a/Ov2PQm4
  
------------

### Deployment
- The application is deployed using docker on my own VPS. You can take a look at it here https://movieasy.xyz/ (If the website is offline please contact me.)
