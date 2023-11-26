# Hanken Quantum Hackathon 2023
Project repository for Hanken Quantum Hackathon 2023 project. The goal of the hackathon is to develop real-life business applications for quantum computing.

## Introduction

In this repository you will find both quantum computer code under `quantum` and a tech demo, housed in `app` and `backend`. The purpose of the application is to model company supply chains, and optimize a route between two points on one or all of three metrics: transport time, CO2 emissions, or transport cost. There is also a balanced optimization version available, which prioritizes all parameters equally, but punishes large values, so it favours the factors being balanced.

There are three scenarios available with data housed in JSON files in the backend: Asian supply lines, European supply lines and North American supply lines.

## Running locally

The tech demo consists of a backend written in C# running on .NET 8.0 and a 
React 18 frontend written in JavaScript. To run the app locally, it is required to have both Node.js and .NET 8.x installed.

https://dotnet.microsoft.com/en-us/download/dotnet/8.0
https://nodejs.org/en/download/current

The backend can be run in the directory `backend/src` with the command `dotnet run`.

The frontend can be run in the directory `app`, with `npm install` to install the required packages, and `npm start` to start the application.