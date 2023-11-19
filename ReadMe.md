# Advent Of Code
<!-- Generated via https://github.com/alexandru-dinu/advent-of-code/blob/main/.scripts/gen_badges.py -->
![](https://img.shields.io/badge/2022-50%20stars-239323)
![](https://img.shields.io/badge/2021-20%20stars-6e621d)
![](https://img.shields.io/badge/2020-21%20stars-6b651e)
![](https://img.shields.io/badge/2019-5%20stars-ae3919)
![](https://img.shields.io/badge/2018-28%20stars-56721f)
![](https://img.shields.io/badge/2017-0%20stars-ef0f14)
![](https://img.shields.io/badge/2016-2%20stars-c62917)
![](https://img.shields.io/badge/2015-50%20stars-239323)

This repository is setup to assist with solving challenges from the [Advent of Code](https://adventofcode.com/).

It allows you to easily run solutions in C#, submit answers and see the response, pull down your inputs, and run test inputs via an API described below.
This also includes some utilities to make some solutions easier. See the `Services/Utility.cs` file for more info.

The `main` branch contains a ready to use template to start your own solutions.
You may also reference the `aowensby-solutions` branch which contains my own solutions.

## Quick Start
1. Run the Program (See [Setup](#setup) below)
1. (Optional) Create a Cookie.txt file to enable puzzle input/submission (See [Puzzle Helper](#puzzle-helper) below)
1. (Optional) Make the `import-input-file` API call to get your input for the day you're trying to solve (See [API](#post-apiimport-input-file) below)
1. Code your solution in one of the Service files
1. Make the `run-solution` API call and optionally submit the solution (See [API](#get-apirun-solution) below)

## Setup
1. If not already installed, install [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download)
1. Run the program
   - If using Visual Studio or VSCode, use the play button to build and run the code
   - If using a CLI, run `dotnet run` from the repo's base folder
1. Run API calls
   - You can use Swagger to execute the API calls, or use your own tool to call to the API
   - Visual Studio
      - The browser should open by default, to change this behavior, update the `Properties/launchSettings.json`
   - VSCode
      - Use the `Launch (web)` or `Launch (web - no browser)` to toggle if you want the broswer to open automatically
      - If you'd prefer to remain within VSCode to make the API calls, I've used the [Thunder Client](https://marketplace.visualstudio.com/items?itemName=rangav.vscode-thunder-client) extension.
   - Other
      - Visit `https://localhost:5001/swagger` in your browser

## Puzzle Helper
This allows you to easily create the needed services as well as submitting answers.

In the `PuzzleHelper` folder, create a `Cookie.txt` file and add your own cookie that gets created when logging into the Advent of Code website. If you open the Network tab in your browser's Dev Tools while on the site, you'll see the cookie in the API calls that are made. This typically expires after a month so you'll need to update it each year.

If you would like to avoid this setup, you can always manually add you input and submit your solutions without having to create a Cookie.txt file.
The file is only required with interacting with the Advent of Code website.

### Automation
The Puzzle Helper does follow the automation guidelines on the [/r/adventofcode community wiki](https://www.reddit.com/r/adventofcode/wiki/faqs/automation).

Specifically:
* Outbound calls are throttled to every 3 minutes in the AdventOfCodeGateway's `ThrottleCall()` function
* Once inputs are downloaded, they are cached locally (PuzzleHelper's `WriteInputFile(int year, int day)` function) through either the `api/puzzle-helper-daily` endpoint described below.
* If you suspect your input is corrupted, you can get a fresh copy by deleting the old file and re-running the `api/puzzle-helper-daily` endpoint.
* The User-Agent header in the Program.cs's gateway configuration is set to me since I maintain this tool :)

## API

### GET `api/run-solution`
- Query parameters
   - year (Ex. 2022) (Defaults to 2015)
   - day (Ex. 14) (Defaults to 1)
   - secondHalf (Ex. true) (Defaults to false)
   - send (Ex. true) (Defaults to false) Submit the result to Advent of Code
   - example (Ex. true) (Defaults to false) Use an example file instead of the regular input, you must add the example at `Inputs/<YYYY>/<DD>_example.txt`
- Ex. `GET api/run-solution?year=2022&day=14&secondHalf=true&send=true`

Runs a specific day's solution, and optionally posts the answer to Advent of Code and returns the result.

### POST `api/generate-service-files`

Creates missing daily solution service files.
Useful when a new year has started to preemptively generate the service files for the calendar year before the advent starts.

The program is idempotent (You can run this multiple times as it will only add files if they are needed.)

### POST `api/import-input-file`
- Query parameters
   - year (Ex. 2022) (Defaults to 2015)
   - day (Ex. 14) (Defaults to 1)
- Ex. `POST api/import-input-file?year=2022&day=14`

Imports the input from Advent of Code for a specific day.

The program is idempotent (You can run this multiple times as it will only add a file if it is needed.)

## Extra Notes
- The admin of Advent of Code have requested that puzzle inputs be cached (To reduce load on the system) and not be made publically available (To make it harder to completely copy the site)
- This puzzle helper currently does not use the leaderboard api, but if you choose to copy this template and talk to the leaderboard, make sure to throttle the and cache the calls to not overload the server