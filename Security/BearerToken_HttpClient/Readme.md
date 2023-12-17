# Bearer Token HttpClient Request

## UserApi:
This project implements an Api with Jwt Authentication with Bearer Token and also provide user details like create a new user, get all users etc.

## Matrix:
This project implements creates a console interface and connect to UserApi Api Project client which authenticates and get the users data.

## MatrixClientTest
This project implements the test cases of the `Matrix` (`UserApi` client) project.

## Application:
This class library project implements user interface logic and connect to the `Domain` and `Infrastructure` project.

## Domain:
This class library project implements the core application models and repository interfaces of api logic.

## Infrastructure:
This class library project implements the `UserApi` route logic for Login and Users details.