# README #

## Project Requirement ##
https://drive.google.com/folderview?id=0B2O-VpiinYoCdjBvUWJ0bF9HSTg&usp=sharing

## System Requirements ##

### Game Build Requirements ###

Unity3D 5 or newer

Andriod 6.0 or newer

### Database Requirements ###

MySQL-client 5 or newer

MySQL-server 5 or newer

### Server Requirements ###

Python 2.7

Django

djangorestframework

## Serverr Build ##

### Install Dependencies and envirnment set up###

On Linux:

> sudo apt_get install mysql-server mysql-client

> sudo apt_get install pip

> pip install virtualenv

> virtualenv env

> pip install django

> pip install djangorestframework

> pip install MySQL-python

> /usr/local/<mysql_dir>/supporting_files/mysql.server start

> (OR /usr/bin/mysqld (if above command fails))

### CREATE DATABASE ###

command line -> mysql -uroot -p

CREATE DATABASE BSDB;
CREATE USER 'admin'@'localhost' identified by 'password';
grant all privileges on BSDB.* to 'admin'@'localhost';
flush privileges;

### Start Server ###

On Linux or MacOS

> source <OUR_REPOSITORY>/databaseServer/env/bin/activate

> cd <OUR_REPOSITORY>/databaseServer/server/

> python manage.py runserver <ip_address>:80

First time start the server:

> python manage.py makemigrations

> python manage.py migrate

Our current server runs on http://115.146.88.121/

### Database test ###

Test envirnment: Request tool
HTTPie — CLI HTTP client

> pip install httpie

New player register request:

> http --json POST http://(Server IP address)/users/register/ username="test123" password="password123" 

Sample response:

>HTTP/1.0 201 Created
>Allow: POST, OPTIONS
>Content-Type: application/json
>Date: Sun, 23 Oct 2016 06:21:17 GMT
>Server: WSGIServer/0.1 Python/2.7.6
>Vary: Accept, Cookie
>X-Frame-Options: SAMEORIGIN
>
>{
>    "password": "password123", 
>    "username": "test123"
>}

Player Login request:

> http -a test123:password123 http://(Server IP address)/users/test123/

Sample response:

>HTTP/1.0 200 OK
>Allow: PUT, GET, OPTIONS, DELETE
>Content-Type: application/json
>Date: Sun, 23 Oct 2016 06:29:28 GMT
>Server: WSGIServer/0.1 Python/2.7.6
>Vary: Accept, Cookie
>X-Frame-Options: SAMEORIGIN
>
>{
>    "password": "pbkdf2_sha256$30000$BencDB2zPI4j$gNM5patnCNegdF9bZK6u04HZ1kLEBN80vcVQRc3+pPw=", 
>    "pk": 11, 
>    "username": "test123"
>}

Get user's rank request:

> http -a test123:password123 http://(Server IP address)/users/rank/test123/

Sample response:

>HTTP/1.0 200 OK
>Allow: PUT, OPTIONS, GET
>Content-Type: application/json
>Date: Sun, 23 Oct 2016 06:31:32 GMT
>Server: WSGIServer/0.1 Python/2.7.6
>Vary: Accept, Cookie
>X-Frame-Options: SAMEORIGIN
>
>{
>    "rank": "1"
>}

update user's rank request:

> http -a test123:password123 PUT http://(Server IP address)/users/rank/test123/ rank="2"

Sample response:

>HTTP/1.0 200 OK
>Allow: PUT, OPTIONS, GET
>Content-Type: application/json
>Date: Sun, 23 Oct 2016 06:33:13 GMT
>Server: WSGIServer/0.1 Python/2.7.6
>Vary: Accept, Cookie
>X-Frame-Options: SAMEORIGIN
>
>{
>    "rank": "2"
>}

## Game Build ##

1. In Unity, load project BubbleSoccer (./BubbleSoccer).
( 1.1 Find LevelManager.cs in _Scripts and at line 15, change the ip address to own server ip.)

### On MacOS ###
File > Build Settings > <Choose desired platform of build> > Build