POS

This project is a basic demonstration of WebAPI principles and methods, this readme is dedicated specfically to the parts of the project for assignment 2, Authentication and Authorization

Getting Started

This program has been done with Visual Studio Code in C# and has utilized Postman to test the endpoints. I have attempted to remain within class instruction

Installation

Testing requires installation of Postman and sqllite

Once the code is downloaded and the sql database is setup, start the program with dotnet run, and uses the following body statements in postman

For user1: 
{
    "username": "user1",
    "password": "password123"
}

For admin1
{
    "username": "admin1",
    "password": "password321"
}

Authentication

Two roles have been defined in this API, user and Admin, each has one account tied to it. It is accessed via postman using the corresponding username and password

Sample Users

The emails listed here are not real emails (As far as I know) They have been created as placeholders

    User 1 (Role: Role1)
        Username: user1
        Email: user1@example.com
        Password: password123

    User 2 (Role: Role2)
        Username: admin1
        Email: admin1@example.com
        Password: password321

Endpoints

I have added three endpoints in an attempt to meet the assignment criteria

Login - Allows user to log into their account (Currently either user1 or admin1)

admin-endpoint - Attempted to display a welcome message for admin1

user-endpoint - Attempted to display a welcome message for user1



    

