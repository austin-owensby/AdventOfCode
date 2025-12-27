using System.Runtime.Intrinsics.Arm;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/09.txt
    public class Solution2025_09Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 9, example);
            List<(long, long)> points = lines.Select(l => l.Split(",").ToLongs()).Select(l => (l.First(), l.Last())).ToList();

            long answer = 0;

            for (int i = 0; i < points.Count - 1; i++)
            {
                (long x1, long y1) = points[i];
                for (int j = i + 1; j < points.Count; j++)
                {
                    (long x2, long y2) = points[j];

                    long xLength = Math.Abs(x1 - x2) + 1;
                    long yLength = Math.Abs(y1 - y2) + 1;

                    long area = xLength * yLength;
                    answer = Math.Max(area, answer);
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 9, example);
            List<(long, long)> points = lines.Select(l => l.Split(",").ToLongs()).Select(l => (l.First(), l.Last())).ToList();

            long answer = 0;

            // Loop over each unique pair of points
            for (int i = 0; i < points.Count - 1; i++)
            {
                (long x1, long y1) = points[i];
                for (int j = i + 1; j < points.Count; j++)
                {
                    (long x2, long y2) = points[j];

                    // Calculate the area of the rectangle
                    long xLength = Math.Abs(x1 - x2) + 1;
                    long yLength = Math.Abs(y1 - y2) + 1;

                    long area = xLength * yLength;

                    // Ignore areas that are too small
                    if (area < answer)
                    {
                        continue;
                    }

                    bool hasIntersection = false;

                    // Loop over all of the other points
                    for (int k = 0; k < points.Count; k++)
                    {
                        // Ignore points that are part of the current rectangle
                        if (k == i || k == j)
                        {
                            continue;
                        }

                        (long x3, long y3) = points[k];

                        bool xCoordinateIntersection = x1 < x3 && x3 < x2 || x2 < x3 && x3 < x1;
                        bool yCoordinateIntersection = y1 < y3 && y3 < y2 || y2 < y3 && y3 < y1;

                        // If this point is within our rectangle, our rectangle is invalid
                        if (xCoordinateIntersection && yCoordinateIntersection)
                        {
                            hasIntersection = true;
                            break;
                        }

                        // Check if there's a line that moves through our rectangle
                        // If both coordinates are outside of our rectangle, continue on, 
                        //   there's no risk of this point eliminating the rectangle
                        if (!xCoordinateIntersection && !yCoordinateIntersection)
                        {
                            continue;
                        }

                        // Check the previous point which should be a straight line the current point
                        (long x4, long y4) = k == 0 ? points.Last() : points[k - 1];
                        bool xCoordinateIntersection2 = x1 < x4 && x4 < x2 || x2 < x4 && x4 < x1;
                        bool yCoordinateIntersection2 = y1 < y4 && y4 < y2 || y2 < y4 && y4 < y1;

                        // If the previous point is inside our rectangle, it is invalid
                        if (xCoordinateIntersection2 && yCoordinateIntersection2)
                        {
                            hasIntersection = true;
                            break;
                        }

                        // Check if this is a line splitting our rectangle
                        if (xCoordinateIntersection && xCoordinateIntersection2 && x3 == x4)
                        {
                            if (y1 < y2 && (y3 <= y1 && y2 <= y4 || y4 <= y1 && y2 <= y3) || y1 > y2 && (y3 <= y2 && y1 <= y4 || y4 <= y2 && y1 <= y3))
                            {
                                hasIntersection = true;
                                break;
                            }
                        }
                        else if (yCoordinateIntersection && yCoordinateIntersection2 && y3 == y4)
                        {
                            if (x1 < x2 && (x3 <= x1 && x2 <= x4 || x4 <= x1 && x2 <= x3) || x1 > x2 && (x3 <= x2 && x1 <= x4 || x4 <= x2 && x1 <= x3))
                            {
                                hasIntersection = true;
                                break;
                            }
                        }
                    }

                    // If there are no lines intersecting our rectangle, this is the new best answer
                    if (!hasIntersection)
                    {
                        answer = area;
                    }
                }
            }

            return answer.ToString();
        }
    }
}