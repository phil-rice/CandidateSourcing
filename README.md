# Capstone project

This is the capstone project for a 'Full Stack Dotnet developer' course. 

- [The project is licensed under the MIT license](LICENSE.md)
- [This document describes how the requirements have been met](REQUIREMENTS_AUDIT.md)
- [Here are some musings in the Architectural decision Log](ARCHITECTURAL_DECISION_LOG.md)

## Project description

# Capstone project

This is the capstone project for a 'Full Stack Dotnet developer' course. 

## Project description

For Large Organisations, it's impractical to maintain the records of the interviewing process manually. The Candidate Sourcing portal enables the maintenance of interview records and scores the candidates based on the discussion. Managers will log in to the system and be able to review the candidature.

Each interview would have rounds for:
- Panellist 1
- Panellist 2
- HR
- Manager
- Recruiter
- Candidate

### Scenarios

**Candidate Login:** 
Candidate updates his details, Passport Number, Name, address, DOB. He should not be able to view panellist forms.

**First Round:** 
Panellist 1 logs in to the portal and provides feedback in various sections on a score with a scale of 1 to 10 (10 being the highest). Detailed feedback should allow a maximum of 1000 characters. The weightage for the score is 40%.

**Second Round:** 
Panellist 2 logs in to the portal and provides feedback in various sections on a score with a scale of 1 to 10 (10 being the highest). Detailed feedback should allow a maximum of 1000 characters. The weightage for the score is 50%.

**HR Round:** 
HR logs in to the portal, provides feedback, and gives a score on a scale of 1 to 10 (10 being the highest). Detailed feedback should allow a maximum of 1000 characters. The weightage for this score is 10%.

**Manager:** 
The Manager logs in and checks the candidate rating, which is the weighted average of the interview scores from the three rounds of the candidate.

- Weighted average > 8: Expert
- Weighted average > 6 and <= 7.99: Proficient
- Weighted average > 4 and <= 5.99: Proficient
- Weighted average < 4: Reject

**Search and view reports of candidate feedback**

Candidate details are maintained in the organization for 1 year. Candidates who are rejected can't appear for the interview within 6 months of their attempt. Recruiters can log in and check if the candidate has appeared for an interview in the last 6 months

# Non functional requirements

## Web Application Criteria

The following criteria should be met for each of the web applications:

### Must Haves:
- Use .Net 6.0 framework
- Use MVC for front end
- Web API for backend
- Entity Framework (code first approach is preferred) for database connectivity
- SQL Server for database (if you don't have SQL server, use Sqlite)
- Implement Authentication/Authorization (Cookies based for MVC and JWT for Web API)
- Swagger support in Web API
- Validations (client side as well as server side)
- CORS
- Dependency Injection
- Repository pattern

### Good to Haves:
- Multilayer architecture
- Exception Handling
- Response Caching
- Filters

# Extra requirements

I was unhappy with the idea of hard coding the process, so I have
made the process and the questions fully customisable.

We can have as many different jobs as we like, each with their own process

# I differentiate between Owner and Manager

I am not sure if I have the workflow perfect here. It meets the requirements, but for
example I might want to let the manager add candidates instead of just the owner

This is the kind of thing that I would 'ask the product owner' about. I just made a decision and can 
review and change it later

# Project Overview

## Common
This is the bottom level project. It holds the utilities that 
just about every other project needs. It is a class library that
only deals with strings / ints / Guids etc

## jobCommon
This is the domain model for the job. It is a class library that
declares 
- The domains model
- How they map to the database
- The database context for EntityFramework
- The migrations for EntityFramework

## Microservices
The heavy lifting class library
- How to do controllers
- How to do repositories
- How to do Http Clients that work with the controllers

There is no domain in this class library. It knows nothing about 
jobs or applications

This allows us to dramatically reduce the 'boilerplate' code in the
other projects. It reduces 'cut and paste abuse' and makes the code
more repeatable, maintainable, easier to test, easier to extend and 
so on

## jobApi
This is the actual API that is the backend. It is a web application

This project is tiny as all the heavy lifting is done by Microservices, 
and this just customizes GenericControllers

##jobClient

A class library that declares HttpClients that talk to the API

This could be merged in with jobCommon, but I wanted all the heavy 
HTTP libraries not to be part of jobCommon. Part of the 'single responsibility'
principle

## gui
This is the web gui

As this is my first dotnet web gui it's more disorganised that 
I would like

It really needs splitting up into 'single responsibility' projects, but
I have ran out of time to do that.

## Test

All the tests go here.











