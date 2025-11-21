using AdventOfCode;
using AdventOfCode.Console.Controllers;

// Set any of these values to default them and bypass the console interface
int? mode = null;
int? year = null;
int? day = null;
bool? secondHalf = null;
bool? send = null;
bool? example = null;

while (mode == null) {
    Console.Clear();
    Console.WriteLine("Select a mode:");
    Console.WriteLine("1) Run Solution");
    Console.WriteLine("2) Import Input");
    Console.WriteLine("3) Generate Missing Solution Service Files");

    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input)) {
        continue;
    }

    if(int.TryParse(input, out int modeValue)) {
        if (modeValue < 1 || modeValue > 3) {
            continue;
        }
        mode = modeValue;
    }
}

if (mode == 1 || mode == 2) {
    while (year == null) {
        Console.Clear();
        Console.WriteLine($"Enter a year ({Globals.START_YEAR}-{DateTime.Now.Year}):");

        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input)) {
            continue;
        }

        if(int.TryParse(input, out int yearValue)) {
            if (yearValue < Globals.START_YEAR || yearValue > DateTime.Now.Year) {
                continue;
            }
            year = yearValue;
        }
    }

    while (day == null) {
        Console.Clear();
        Console.WriteLine("Enter a day (1-25):");

        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input)) {
            continue;
        }

        if(int.TryParse(input, out int dayValue)) {
            if (dayValue < 1 || dayValue > 25) {
                continue;
            }
            day = dayValue;
        }
    }
}

if (mode == 1) {
    while (secondHalf == null) {
        Console.Clear();
        Console.WriteLine("Run second half? (Y/N):");

        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input)) {
            continue;
        }

        if(input.ToLower() == "y") {
            secondHalf = true;
        }
        else if (input.ToLower() == "n") {
            secondHalf = false;
        }
    }

    while (example == null) {
        Console.Clear();
        Console.WriteLine("Run example? (Y/N):");

        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input)) {
            continue;
        }

        if(input.ToLower() == "y") {
            example = true;
        }
        else if (input.ToLower() == "n") {
            example = false;
        }
    }

    while (send == null) {
        Console.Clear();
        Console.WriteLine("Submit results? (Y/N):");

        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input)) {
            continue;
        }

        if(input.ToLower() == "y") {
            send = true;
        }
        else if (input.ToLower() == "n") {
            send = false;
        }
    }
}

Console.Clear();

Controller controller = new();

if (mode == 1) {
    await controller.GetSolution(year!.Value, day!.Value, secondHalf!.Value, send!.Value, example!.Value);
}
else if (mode == 2) {
    await controller.ImportInputFile(year!.Value, day!.Value);
}
else if (mode == 3) {
    await controller.GenerateMissingSolutionServiceFiles();
}
else {
    Console.WriteLine($"Unknown mode: {mode}");
}