using System.Numerics;

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

            long answer = 0;
            
            // Test a potential line for the rock by taking a point from 2 different hailstones at 2 different times and then testing for an intersection for all other hailstones
            int t1 = 1;
            int t2 = 2;

            while (answer == 0) {
                foreach (int i in points.Count - 1)
                {
                    for (int j = i + 1; j < points.Count; j++) {
                        long point1X = points[i][0] + t1 * points[i][3];
                        long point1Y = points[i][1] + t1 * points[i][4];
                        long point1Z = points[i][2] + t1 * points[i][5];
                        long point2X = points[j][0] + t2 * points[j][3];
                        long point2Y = points[j][1] + t2 * points[j][4];
                        long point2Z = points[j][2] + t2 * points[j][5];
                        long deltaX = point2X - point1X;
                        long deltaY = point2Y - point1Y;
                        long deltaZ = point2Z - point1Z;

                        List<long> testPoint = [point1X - t1 * deltaX, point1Y - t1 * deltaY, point1Z - t1 * deltaZ, deltaX, deltaY, deltaZ];

                        bool hasIntersection = true;
                        foreach (List<long> point in points) {
                            // Check if our test point has an intersection for every hailstone
                        }

                        if (hasIntersection) {
                            answer = testPoint[0] + testPoint[1] + testPoint[2];
                        }
                    }

                    if (answer != 0) {
                        break;
                    }
                }

                if (answer != 0) {
                    break;
                }

                // We didn't find a solution between any hailstone at t1 and t2, try a new combination of t1 and t2
                t2++;
            }

            return answer.ToString();
        }
    }
}