namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2022/15.txt
    public class Solution2022_15Service : ISolutionDayService
    {
                private class SensorBeaconPair {
            public int SensorX {get; set;}
            public int SensorY {get; set;}
            public int BeaconX {get; set;}
            public int BeaconY {get; set;}
            public int Distance {get; set;}
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 15, example);
            List<SensorBeaconPair> sensors = lines.QuickRegex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)").ToInts().Select(sensorBeaconPair => new SensorBeaconPair(){
                SensorX = sensorBeaconPair[0],
                SensorY = sensorBeaconPair[1],
                BeaconX = sensorBeaconPair[2],
                BeaconY = sensorBeaconPair[3]
            }).ToList();

            foreach (SensorBeaconPair sensor in sensors)
            {
                int diffX = Math.Abs(sensor.BeaconX - sensor.SensorX);
                int diffY = Math.Abs(sensor.BeaconY - sensor.SensorY);
                sensor.Distance = diffX + diffY;
            }

            int minX = Math.Min(sensors.Min(s => s.BeaconX), sensors.Min(s => s.SensorX));
            int maxX = Math.Max(sensors.Max(s => s.BeaconX), sensors.Max(s => s.SensorX));

            int answer = 0;

            int yRow = 2000000;

            // For each x in row y = yRow, check if the distance from any of the sensors is <= the distance to the closest beacon
            for (int x = 2*minX; x <= 2*maxX; x++) {
                foreach (SensorBeaconPair sensor in sensors) {
                    int diffX = Math.Abs(x - sensor.SensorX);
                    int diffY = Math.Abs(yRow - sensor.SensorY);
                    int distance = diffX + diffY;

                    if (distance <= sensor.Distance && !(x == sensor.BeaconX && yRow == sensor.BeaconY)) {
                        answer++;
                        break;
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 15, example);
            List<SensorBeaconPair> sensors = lines.QuickRegex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)").ToInts().Select(sensorBeaconPair => new SensorBeaconPair(){
                SensorX = sensorBeaconPair[0],
                SensorY = sensorBeaconPair[1],
                BeaconX = sensorBeaconPair[2],
                BeaconY = sensorBeaconPair[3]
            }).ToList();

            foreach (SensorBeaconPair sensor in sensors)
            {
                int diffX = Math.Abs(sensor.BeaconX - sensor.SensorX);
                int diffY = Math.Abs(sensor.BeaconY - sensor.SensorY);
                sensor.Distance = diffX + diffY;
            }

            // Wowee this is a big answer
            ulong answer = 0;

            int upperLimit = 4000000;

            for (int x = 0; x <= upperLimit; x++) {
                for (int y = 0; y <= upperLimit; y++) {
                    bool outOfRange = true;

                    foreach (SensorBeaconPair sensor in sensors) {
                        int diffX = Math.Abs(x - sensor.SensorX);
                        int diffY = Math.Abs(y - sensor.SensorY);
                        int distance = diffX + diffY;

                        if (distance <= sensor.Distance) {
                            // Try and move out of the sensor range
                            y += sensor.Distance - distance;
                            outOfRange = false;
                            break;
                        }
                    }

                    if (outOfRange) {
                        answer = (ulong)x * (ulong)upperLimit + (ulong)y;
                        // Let's get out of here!
                        x = upperLimit + 1;
                        break;
                    }
                }
            }

            return answer.ToString();
        }
    }
}