using System.Numerics;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Services
{
    public class Solution2023_24Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 24, example);
            List<List<long>> points = lines.Select(line => line.QuickRegex(@"(\d+), (\d+), (\d+) @ (-?\d+), (-?\d+), (-?\d+)").ToLongs()).ToList();

            long minTestArea = example ? 7 : 200000000000000;
            long maxTestArea = example ? 27 : 400000000000000;

            int answer = 0;

            // Iterate over each combination of 2 vectors to check for intersections
            foreach (int i in points.Count - 1)
            {
                for (int j = i + 1; j < points.Count; j++) {
                    List<long> pointA = points[i];
                    List<long> pointB = points[j];

                    // Convert lines to the form Ax + By = C
                    // A = y2 - y1
                    // B = x1 - x2
                    // C = A * x + B * y1

                    long a1 = pointA[4];
                    long b1 = -pointA[3];
                    long c1 = a1 * pointA[0] + b1 * pointA[1];

                    long a2 = pointB[4];
                    long b2 = -pointB[3];
                    long c2 = a2 * pointB[0] + b2 * pointB[1];

                    decimal delta = a1 * b2 - a2 * b1;

                    // Delta of 0 means the lines are parallel
                    if (delta != 0) {
                        // For larger numbers b2 * c1 causes an overflow so we rearrange the order of operations
                        decimal x = b2 / delta * c1 - b1 / delta * c2; // (b2 * c1 - b1 * c2) / delta
                        decimal y = a1 / delta * c2 - a2 / delta * c1; // (a1 * c2 - a2 - c1) / delta

                        // Check if the intersection is within
                        if (x >= minTestArea && x <= maxTestArea && y >= minTestArea && y <= maxTestArea) {
                            // Check if the intersection is in the future by checking if the difference between the intersection and the starting points matches the slopes direction
                            if (x < pointA[0] == pointA[3] < 0 && y < pointA[1] == pointA[4] < 0 && x < pointB[0] == pointB[3] < 0 && y < pointB[1] == pointB[4] < 0) {
                                answer++;
                            }
                        }
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 24, example);
            List<List<long>> points = lines.Select(line => line.QuickRegex(@"(\d+), (\d+), (\d+) @ (-?\d+), (-?\d+), (-?\d+)").ToLongs()).ToList();

            // Inspired by the solution here: https://github.com/DeadlyRedCube/AdventOfCode/blob/main/2023/AOC2023/D24.h

            // We have 6 unknowns for the rock the x, y, and z starting points and velocities (Rx, Ry, Rz, Rdx, Rdy, Rdz)
            // In order to solve these 6 variables, we need 6 independent linear equations

            // The equation for the position of a rock or hailstone after a certain amount of time for a single axis is Rx + Rdx * tn
            // The rock will collide with a specific hailstone (A) at a specific time when they end up at the same position
            // Rx + Rdx * tn = Ax + Adx * tn

            // We can then rearrange the formula so that time is on 1 side
            // tn = (Rx - Ax)/(Adx - Rdx)

            // This formula has an equivalent y and z version
            // We can set them equal to each other to get 3 independent equations
            // (Rx - Ax)/(Adx - Rdx) = (Ry - Ay)/(Ady - Rdy)
            // (Rx - Ax)/(Adx - Rdx) = (Rz - Az)/(Adz - Rdz)
            // (Ry - Ay)/(Ady - Rdy) = (Rz - Az)/(Adz - Rdz)

            // These equations can be rearranged...
            // (Rx - Ax)(Ady - Rdy) = (Ry - Ay)(Adx - Rdx)
            // (Rx - Ax)(Adz - Rdz) = (Rz - Az)(Adx - Rdx)
            // (Ry - Ay)(Ady - Rdy) = (Rz - Az)(Adx - Rdx)
            // ...distributed...
            // Rx * Ady - Rx * Rdy - Ax * Ady + Ax * Rdy = Ry * Adx - Ry * Rdx - Ay * Adx + Ay * Rdx
            // Rx * Adz - Rx * Rdz - Ax * Adz + Ax * Rdz = Rz * Adx - Rz * Rdx - Az * Adx + Az * Rdx
            // Ry * Adz - Ry * Rdz - Ay * Adz + Ay * Rdz = Rz * Ady - Rz * Rdy - Az * Ady + Az * Rdy
            // ...and then rearranged again so that the constant unknowns are on 1 side
            // -Rx * Rdy + Ry * Rdx = -Rx * Ady + Ry * Adx + Rdx * Ay - Rdy * Ax + Ax * Ady - Ay * Adx
            // -Rx * Rdz + Rz * Rdx = -Rx * Adz + Rz * Adx + Rdx * Az - Rdz * Ax + Ax * Adz - Az * Adx
            // -Ry * Rdz + Rz * Rdy = -Ry * Adz + Rz * Ady + Rdy * Az - Rdz * Ay + Ay * Adz - Az * Ady

            // For any given hailstone, the left side of the equation will be the same so we can set them equal for hailstones A and B
            // This gives us independent linear equations
            // -Rx * Ady + Ry * Adx + Rdx * Ay - Rdy * Ax + Ax * Ady - Ay * Adx = -Rx * Bdy + Ry * Bdx + Rdx * By - Rdy * Bx + Bx * Bdy - By * Bdx
            // -Rx * Adz + Rz * Adx + Rdx * Az - Rdz * Ax + Ax * Adz - Az * Adx = -Rx * Bdz + Rz * Bdx + Rdx * Bz - Rdz * Bx + Bx * Bdz - Bz * Bdx
            // -Ry * Adz + Rz * Ady + Rdy * Az - Rdz * Ay + Ay * Adz - Az * Ady = -Ry * Bdz + Rz * Bdy + Rdy * Bz - Rdz * By + By * Bdz - Bz * Bdy
            
            // These simplify to
            // (-Ady + Bdy) * Rx + (Adx - Bdx) * Ry + (Ay - By) * Rdx + (-Ax + Bx) * Rdy = (-Ax * Ady) + (Ay * Adx) + (Bx * Bdy) + (-By * Bdx)
            // (-Adz + Bdz) * Rx + (Adx - Bdx) * Rz + (Az - Bz) * Rdx + (-Ax + Bx) * Rdz = (-Ax * Adz) + (Az * Adx) + (Bx * Bdz) + (-Bz * Bdx)
            // (-Adz + Bdz) * Ry + (Ady - Bdy) * Rz + (Az - Bz) * Rdy + (-Ay + By) * Rdz = (-Ay * Adz) + (Az * Ady) + (By * Bdz) + (-Bz * Bdy)

            // In order to get a full 6 independent linear equations we can repeat this with A-C
            // (-Ady + Bdy) * Rx + (Adx - Bdx) * Ry + (Ay - By) * Rdx + (-Ax + Bx) * Rdy = (-Ax * Ady) + (Ay * Adx) + (Bx * Bdy) + (-By * Bdx)
            // (-Adz + Bdz) * Rx + (Adx - Bdx) * Rz + (Az - Bz) * Rdx + (-Ax + Bx) * Rdz = (-Ax * Adz) + (Az * Adx) + (Bx * Bdz) + (-Bz * Bdx)
            // (-Adz + Bdz) * Ry + (Ady - Bdy) * Rz + (Az - Bz) * Rdy + (-Ay + By) * Rdz = (-Ay * Adz) + (Az * Ady) + (By * Bdz) + (-Bz * Bdy)
            // (-Ady + Cdy) * Rx + (Adx - Cdx) * Ry + (Ay - Cy) * Rdx + (-Ax + Cx) * Rdy = (-Ax * Ady) + (Ay * Adx) + (Cx * Cdy) + (-Cy * Cdx)
            // (-Adz + Cdz) * Rx + (Adx - Cdx) * Rz + (Az - Cz) * Rdx + (-Ax + Cx) * Rdz = (-Ax * Adz) + (Az * Adx) + (Cx * Cdz) + (-Cz * Cdx)
            // (-Adz + Cdz) * Ry + (Ady - Cdy) * Rz + (Az - Cz) * Rdy + (-Ay + Cy) * Rdz = (-Ay * Adz) + (Az * Ady) + (Cy * Cdz) + (-Cz * Cdy)

            long Ax = points[0][0];
            long Ay = points[0][1];
            long Az = points[0][2];
            long Adx = points[0][3];
            long Ady = points[0][4];
            long Adz = points[0][5];
            long Bx = points[1][0];
            long By = points[1][1];
            long Bz = points[1][2];
            long Bdx = points[1][3];
            long Bdy = points[1][4];
            long Bdz = points[1][5];
            long Cx = points[2][0];
            long Cy = points[2][1];
            long Cz = points[2][2];
            long Cdx = points[2][3];
            long Cdy = points[2][4];
            long Cdz = points[2][5];

            // The columns are Rx, Ry, Rz, Rdx, Rdy, Rdz's coefficients
            // I'm having to use BigIntegers here otherwise calculating the Determinate results in an overflow
            List<List<BigInteger>> equations = [
                [(-Ady + Bdy),  (Adx - Bdx),           0, (Ay - By), (-Ax + Bx),          0],
                [(-Adz + Bdz),            0, (Adx - Bdx), (Az - Bz),          0, (-Ax + Bx)],
                [           0, (-Adz + Bdz), (Ady - Bdy),         0,  (Az - Bz), (-Ay + By)],
                [(-Ady + Cdy),  (Adx - Cdx),           0, (Ay - Cy), (-Ax + Cx),          0],
                [(-Adz + Cdz),            0, (Adx - Cdx), (Az - Cz),          0, (-Ax + Cx)],
                [           0, (-Adz + Cdz), (Ady - Cdy),         0,  (Az - Cz), (-Ay + Cy)]
            ];

            // This is the right side values of the linear equations
            List<long> values = [
                (-Ax * Ady) + (Ay * Adx) + (Bx * Bdy) + (-By * Bdx),
                (-Ax * Adz) + (Az * Adx) + (Bx * Bdz) + (-Bz * Bdx),
                (-Ay * Adz) + (Az * Ady) + (By * Bdz) + (-Bz * Bdy),
                (-Ax * Ady) + (Ay * Adx) + (Cx * Cdy) + (-Cy * Cdx),
                (-Ax * Adz) + (Az * Adx) + (Cx * Cdz) + (-Cz * Cdx),
                (-Ay * Adz) + (Az * Ady) + (Cy * Cdz) + (-Cz * Cdy)
            ];

            // Use Cramer's Rule to solve the linear system of equations
            BigInteger determinate = Utility.MatrixDeterminate(equations);
            BigInteger xDeterminate = Utility.MatrixDeterminate(equations.Select((row, i) => row.Select((cell, j) => j == 0 ? values[i] : cell).ToList()).ToList());
            BigInteger yDeterminate = Utility.MatrixDeterminate(equations.Select((row, i) => row.Select((cell, j) => j == 1 ? values[i] : cell).ToList()).ToList());
            BigInteger zDeterminate = Utility.MatrixDeterminate(equations.Select((row, i) => row.Select((cell, j) => j == 2 ? values[i] : cell).ToList()).ToList());

            BigInteger x = xDeterminate / determinate;
            BigInteger y = yDeterminate / determinate;
            BigInteger z = zDeterminate / determinate;

            BigInteger answer = x + y + z;

            return answer.ToString();
        }
    }
}