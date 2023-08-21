# Advent Of Code
<!-- Generated via https://github.com/alexandru-dinu/advent-of-code/blob/main/.scripts/gen_badges.py -->
<!-- begin-year-badge -->
![](https://img.shields.io/badge/2022-50%20stars-239323)
![](https://img.shields.io/badge/2021-20%20stars-6e621d)
![](https://img.shields.io/badge/2020-21%20stars-6b651e)
![](https://img.shields.io/badge/2019-5%20stars-ae3919)
![](https://img.shields.io/badge/2018-13%20stars-87521c)
![](https://img.shields.io/badge/2017-0%20stars-ef0f14)
![](https://img.shields.io/badge/2016-0%20stars-ef0f14)
![](https://img.shields.io/badge/2015-47%20stars-298f23)
<!-- end-year-badge -->
This repository serves to hold my own solutions to the [Advent of Code](https://adventofcode.com/).
Feel free to copy this template and generate your own solutions.

## Setup
1. If not already installed, install [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download)
1. Run the program
   - If using Visual Studio or VSCode, use the play button to build and run the code
   - If using a CLI, run `dotnet run` from the repo's base folder
1. Built-in API specification and execution using Swagger
   - You can use Swagger to execute the API calls, or use your own tool to call to the API
   - Visual Studio
      - The browser should open by default, to change this behavior, update the `Properties/launchSettings.json`
   - VSCode
      - Use the `Launch (web)` or `Launch (web - no browser)` to toggle if you want the broswer to open automatically
   - Other
      - Visit `https://localhost:5001/swagger` in your browser

## Puzzle Helper
This allows you to easily create the needed input files and services.

If you would like to use this project's template for you own solution and inputs
1. First delete the daily service files (This is all of the folders named after a year in the `Services` folder).
1. Next in the `PuzzleHelper` folder, create a `Cookie.txt` file and add your own cookie that gets created when logging into the Advent of Code website. If you open the Network tab in your browser's Dev Tools while on the site, you'll see the cookie in the API calls that are made.
1. Finally, run the project and use the Puzzle Helper API (`POST api/puzzle-helper`) to import your inputs and create fresh services

The program is idempotent (You can run this multiple times as it will only add files if they are needed.)

### Automation
The Puzzle Helper does follow the automation guidelines on the [/r/adventofcode community wiki](https://www.reddit.com/r/adventofcode/wiki/faqs/automation).

Specifically:
* Outbound calls are throttled to every 3 minutes in the AdventOfCodeGateway's `ThrottleCall()` function
* Once inputs are downloaded, they are cached locally (PuzzleHelper's `WriteInputFile(int year, int day)` function) through either the `api/puzzle-helper` or `api/puzzle-helper-daily` endpoints described below.
* If you suspect your input is corrupted, you can get a fresh copy by deleting the old file and re-running the `api/puzzle-helper-daily` endpoint.
* The User-Agent header in the Program.cs's gateway configuration is set to me since I maintain this tool :)

## API

### GET `api/run-solution`
- Query parameters
   - year (Ex. 2022) (Defaults to 2015)
   - day (Ex. 14) (Defaults to 1)
   - secondHalf (Ex. true) (Defaults to false)
   - send (Ex. true) (Defaults to false)
- Ex. `GET api/run-solution?year=2022&day=14&secondHalf=true&send=true`

Runs a specific day's solution, posts the answer to Advent of Code, and returns the result from Advent of Code

### POST `api/puzzle-helper`

Imports all missing inputs from Advent of Code as well as creating missing service files.
This can be run before the event starts to pre-emptively populate the service files for the upcoming advent as well as importing the past advent inputs.

### POST `api/puzzle-helper-daily`
- Query parameters
   - year (Ex. 2022) (Defaults to 2015)
   - day (Ex. 14) (Defaults to 1)
- Ex. `POST api/puzzle-helper-daily?year=2022&day=14`

Imports the input from Advent of Code for a specific day. Useful when you want a streamlined version of the above call to only check for a specific day.

### POST `api/init`

Clears existing input files and services files and finally runs the `api/puzzle-helper` function to get fresh inputs and service files.
This only needs to be run once for a new user.

## Extra Notes
- The admin of Advent of Code have requested that puzzle inputs be cached (To reduce load on the system) and not be made publically available (To make it harder to completely copy the site)
- This puzzle helper currently does not use the leaderboard api, but if you choose to copy this template and talk to the leaderboard, make sure to throttle the and cache the calls to not overload the server

## TODO
- Add an easy way to switch between the example and real input
- Add Utility to decode printed text in a grid