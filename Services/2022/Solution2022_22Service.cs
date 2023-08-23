namespace AdventOfCode.Services
{
    public class Solution2022_22Service : ISolutionDayService
    {
        private enum Facing {
            Right,
            Down,
            Left,
            Up
        }

        public Solution2022_22Service() { }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 22, example);

            List<List<char>> grid = lines.SkipLast(2).Select(line => line.ToList()).ToList();
            string unparsedInstruction = lines.Last();

            List<string> instructions = new();

            string currentValue = string.Empty;

            foreach (char instructionLetter in unparsedInstruction) {
                if (string.IsNullOrEmpty(currentValue)) {
                    currentValue += instructionLetter;
                }
                else {
                    bool currentIsNumber = int.TryParse(currentValue, out int currentNumber);
                    bool instructionLetterIsNumber = char.IsNumber(instructionLetter);

                    if (currentIsNumber != instructionLetterIsNumber) {
                        instructions.Add(currentValue);
                        currentValue = instructionLetter.ToString();
                    }
                    else {
                        currentValue += instructionLetter;
                    }
                }
            }

            instructions.Add(currentValue);

            int row = 1;
            int column = grid[0].FindIndex(r => r != ' ') + 1;
            Facing facing = Facing.Right;

            foreach (string instruction in instructions)
            {
                if (int.TryParse(instruction, out int movement)) {
                    // Move
                    while (movement > 0) {
                        List<char> rowData = grid[row - 1];
                        List<char> columnData = grid.Select(r => column <= r.Count ? r[column - 1] : ' ').ToList();
                        int newColumn = column;
                        int newRow = row;

                        switch (facing) {
                            case Facing.Right:
                                // Check if we're at the input's total edge
                                if (column > rowData.Count) {
                                    // Check for next available column
                                    newColumn = rowData.FindIndex(r => r != ' ') + 1;
                                }
                                else {
                                    newColumn = rowData.FindIndex(column, r => r != ' ') + 1;

                                    if (newColumn == 0) {
                                        newColumn = rowData.FindIndex(r => r != ' ') + 1;
                                    }
                                }
                                break;
                            case Facing.Down:
                                // Check if we're at the input's total edge
                                if (row > columnData.Count) { // Row sizes differ!
                                    // Check for next available column
                                    newRow = columnData.FindIndex(r => r != ' ') + 1;
                                }
                                else {
                                    newRow = columnData.FindIndex(row, r => r != ' ') + 1;

                                    if (newRow == 0) {
                                        newRow = columnData.FindIndex(r => r != ' ') + 1;
                                    }
                                }
                                break;
                            case Facing.Left:
                                // Check if we're at the input's total edge
                                if (column == 1) {
                                    // Check for next available column
                                    newColumn = rowData.FindLastIndex(r => r != ' ') + 1;
                                }
                                else {
                                    newColumn = rowData.FindLastIndex(column - 2, r => r != ' ') + 1;

                                    if (newColumn == 0) {
                                        newColumn = rowData.FindLastIndex(r => r != ' ') + 1;
                                    }
                                }
                                break;
                            case Facing.Up:
                                // Check if we're at the input's total edge
                                if (row == 1) {
                                    // Check for next available column
                                    newRow = columnData.FindLastIndex(r => r != ' ') + 1;
                                }
                                else {
                                    newRow = columnData.FindLastIndex(row - 2, r => r != ' ') + 1;

                                    if (newRow == 0) {
                                        newRow = columnData.FindLastIndex(r => r != ' ') + 1;
                                    }
                                }
                                break;
                        }
                    
                        if (grid[newRow - 1][newColumn - 1] == '.') {
                            row = newRow;
                            column = newColumn;
                            movement--;
                        }
                        else {
                            movement = 0;
                        }
                    }
                }
                else {
                    // Update facing
                    facing = (Facing)Utility.Mod((int)facing + (instruction == "R" ? 1 : -1), 4);
                }
            }

            int answer = 1000 * row + 4 * column + (int)facing;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 22, example);

            List<List<char>> grid = lines.SkipLast(2).Select(line => line.ToList()).ToList();
            string unparsedInstruction = lines.Last();

            List<string> instructions = new();

            string currentValue = string.Empty;

            foreach (char instructionLetter in unparsedInstruction) {
                if (string.IsNullOrEmpty(currentValue)) {
                    currentValue += instructionLetter;
                }
                else {
                    bool currentIsNumber = int.TryParse(currentValue, out int currentNumber);
                    bool instructionLetterIsNumber = char.IsNumber(instructionLetter);

                    if (currentIsNumber != instructionLetterIsNumber) {
                        instructions.Add(currentValue);
                        currentValue = instructionLetter.ToString();
                    }
                    else {
                        currentValue += instructionLetter;
                    }
                }
            }

            instructions.Add(currentValue);

            int row = 1;
            int column = grid[0].FindIndex(r => r != ' ') + 1;
            Facing facing = Facing.Right;
            
            /*
                For my specific input, I'm visualizing the faces as follows:
                 21
                 3
                54
                6
            */
            int face = 2;
            int faceSize = grid.Count / 4;

            Dictionary<int, int> minRow = new(){
                {1, 1},
                {2, 1},
                {3, 1 + faceSize},
                {4, 1 + faceSize * 2},
                {5, 1 + faceSize * 2},
                {6, 1 + faceSize * 3}
            };

            Dictionary<int, int> minCol = new(){
                {1, 1 + faceSize * 2},
                {2, 1 + faceSize},
                {3, 1 + faceSize},
                {4, 1 + faceSize},
                {5, 1},
                {6, 1}
            };

            foreach (string instruction in instructions)
            {
                if (int.TryParse(instruction, out int movement)) {
                    // Move
                    while (movement > 0) {
                        List<char> rowData = grid[row - 1];
                        List<char> columnData = grid.Select(r => column <= r.Count ? r[column - 1] : ' ').ToList();
                        int newColumn = column;
                        int newRow = row;
                        Facing newFacing = facing;
                        int newFace = face;

                        switch (facing) {
                            case Facing.Right:
                                // Check if we're at the edge of the face
                                int maxFaceCol = minCol[face] + faceSize - 1;
                                
                                if (column == maxFaceCol) {
                                    // Move to other face
                                    // Ouch my brain :(
                                    switch (face) {
                                        case 1:
                                            newFace = 4;
                                            newFacing = Facing.Left;
                                            newColumn = minCol[newFace] + faceSize - 1;
                                            newRow = minRow[newFace] + faceSize - 1 - row + minRow[face];
                                            break;
                                        case 2:
                                            newFace = 1;
                                            newColumn++;
                                            break;
                                        case 3:
                                            newFace = 1;
                                            newFacing = Facing.Up;
                                            newColumn = minCol[newFace] + row - minRow[face];
                                            newRow = minRow[newFace] + faceSize - 1;
                                            break;
                                        case 4:
                                            newFace = 1;
                                            newFacing = Facing.Left;
                                            newColumn = minCol[newFace] + faceSize - 1;
                                            newRow = minRow[newFace] + faceSize - 1 - row + minRow[face];
                                            break;
                                        case 5:
                                            newFace = 4;
                                            newColumn++;
                                            break;
                                        case 6: 
                                            newFace = 4;
                                            newFacing = Facing.Up;
                                            newColumn = minCol[newFace] + row - minRow[face];
                                            newRow = minRow[newFace] + faceSize - 1;
                                            break;
                                        default:
                                            break; // Shouldn't happen
                                    }
                                }
                                else {
                                    // Move normally within the face
                                    newColumn++;
                                }
                                break;
                            case Facing.Down:
                                // Check if we're at the edge of the face     
                                int maxFaceRow = minRow[face] + faceSize - 1;

                                if (row == maxFaceRow) {
                                    // Move to other face
                                    // Ouch my brain :(
                                    switch (face) {
                                        case 1:
                                            newFace = 3;
                                            newFacing = Facing.Left;
                                            newColumn = minCol[newFace] + faceSize - 1;
                                            newRow = minRow[newFace] + column - minCol[face];
                                            break;
                                        case 2:
                                            newFace = 3;
                                            newRow++;
                                            break;
                                        case 3:
                                            newFace = 4;
                                            newRow++;
                                            break;
                                        case 4:
                                            newFace = 6;
                                            newFacing = Facing.Left;
                                            newColumn = minCol[newFace] + faceSize - 1;
                                            newRow = minRow[newFace] + column - minCol[face];
                                            break;
                                        case 5:
                                            newFace = 6;
                                            newRow++;
                                            break;
                                        case 6: 
                                            newFace = 1;
                                            newColumn = column + faceSize * 2;
                                            newRow = minRow[newFace];
                                            break; 
                                        default:
                                            break; // Shouldn't happen
                                    }
                                }
                                else {
                                    // Move normally within the face
                                    newRow++;
                                }
                                break;
                            case Facing.Left:
                                // Check if we're at the edge of the face
                                if (column == minCol[face]) {
                                    // Move to other face
                                    // Ouch my brain :(
                                    switch (face) {
                                        case 1:
                                            newFace = 2;
                                            newColumn--;
                                            break;
                                        case 2:
                                            newFace = 5;
                                            newFacing = Facing.Right;
                                            newColumn = minCol[newFace];
                                            newRow = minRow[newFace] + faceSize - 1 - row + minRow[face];
                                            break;
                                        case 3:
                                            newFace = 5;
                                            newFacing = Facing.Down;
                                            newColumn = minCol[newFace] + row - minRow[face];
                                            newRow = minRow[newFace];
                                            break;
                                        case 4:
                                            newFace = 5;
                                            newColumn--;
                                            break;
                                        case 5:
                                            newFace = 2;
                                            newFacing = Facing.Right;
                                            newColumn = minCol[newFace];
                                            newRow = minRow[newFace] + faceSize - 1 - row + minRow[face];
                                            break;
                                        case 6: 
                                            newFace = 2;
                                            newFacing = Facing.Down;
                                            newColumn = minCol[newFace] + row - minRow[face];
                                            newRow = minRow[newFace];
                                            break;
                                        default:
                                            break; // Shouldn't happen
                                    }
                                }
                                else {
                                    // Move normally within the face
                                    newColumn--;
                                }
                                break;
                            case Facing.Up:
                                // Check if we're at the edge of the face
                                if (row == minRow[face]) {
                                    // Move to other face
                                    // Ouch my brain :(
                                    switch (face) {
                                        case 1:
                                            newFace = 6;
                                            newColumn = column - faceSize * 2;
                                            newRow = minRow[newFace] + faceSize - 1;
                                            break;
                                        case 2:
                                            newFace = 6;
                                            newFacing = Facing.Right;
                                            newColumn = minCol[newFace];
                                            newRow = minRow[newFace] + column - minCol[face];
                                            break;
                                        case 3:
                                            newFace = 2;
                                            newRow--;
                                            break;
                                        case 4:
                                            newFace = 3;
                                            newRow--;
                                            break;
                                        case 5:
                                            newFace = 3;
                                            newFacing = Facing.Right;
                                            newColumn = minCol[newFace];
                                            newRow = minRow[newFace] + column - minCol[face];
                                            break;
                                        case 6:
                                            newFace = 5;
                                            newRow--;
                                            break; 
                                        default:
                                            break; // Shouldn't happen
                                    }
                                }
                                else {
                                    // Move normally within the face
                                    newRow--;
                                }
                                break;
                        }
                    
                        if (grid[newRow - 1][newColumn - 1] == '.') {
                            row = newRow;
                            column = newColumn;
                            facing = newFacing;
                            face = newFace;
                            movement--;
                        }
                        else {
                            movement = 0;
                        }
                    }
                }
                else {
                    // Update facing
                    facing = (Facing)Utility.Mod((int)facing + (instruction == "R" ? 1 : -1), 4);
                }
            }

            int answer = 1000 * row + 4 * column + (int)facing;

            // Too low 54269
            // Too Low 93304

            return answer.ToString();
        }
    }
}