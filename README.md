# backendChatApp
This is Stage two of Server ServerChat task in advance 2 course.

The task was coded by Avia Hadad, Ori Angle, and David Gurovic.
The project main repositry can be found at:

https://github.com/oriAngel62/backendChatApp.git
This stage of the project focuses on building a back end of a Chat
resembling a real server from social media tool such as Whatsupp, Discord, Facebook Chat etc'

The project is written with Visual Code Framwork with ASP.NET Entity
We worked in a Model View Controller pattern while using Maria DB to save the data.

We created a http Server and used Maria DB to negotiate with the DataBase
the server enable us to add new users and store them localy in DB.
The server return the information from api request in json format. 
We also added Signal R whos enable us to commiounicate with other servers in 
a real-time, so when you want to chat with somone, that someone see it immeditaly. 

Steps to run the project:
*open visual studio
*Clone the project to your PC (the project is in the main branch- just copy the url from git)

Install-Package Pomelo.EntityFrameworkCore.MySQL -Version 6.0.1
Install-Package Microsoft.EntityFrameworkcore.Tools -version 6.0.1

Add-Migration to API: 
VisualStudio -> search -> package maneger -> ( a console window open) -> write: Add-Migration "nameOfYpurChoise"  ->
-> write: Update-DataBase   -----  we got DB.

how to play with it:
open prompt - search in windown : "MySQL Client (MariaDB ... )" -> then password
use PomeloDB (name of the db);   - to open DB
(open the table "user") SELECT * FROM user;

if you already have db that not update write "drop databse PomeloDB;" in the prompt , delete all migration files and Add-Migration again. 

*Run the server (set as start up project) API project (swagger should open).
Our working url server is hard coded in program.cs file, so in every fetch method we include this url http://localhost:5285.
*Run ranking server.
In order to ran the rank system you should ran except API project also the MVC project(set start up).
