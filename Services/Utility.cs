using System.Text.RegularExpressions;

namespace AdventOfCode.Services
{
    #region Usefull Classes
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
    }
    #endregion

    // Reminders of useful LINQ functions
    //    * Chunk(n) splits list into list of lists of length n
    //    * Except returns the list of values from the first not in the second
    //    * Intersect returns the list of values in both lists
    //    * Union returns a list of distinct elements between 2 lists
    //    * Zip combine each element of 2 lists 
    //    * Enumerable.Range(a, b) starting at a, increments with each element b times
    //    * Enumerable.Repeat(a, b) repeats a, b times

    /// <summary>
    /// This class holds a handful of custom helper functions to make solving these problems easier
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Converts 'a' to 1 and 'A' to 27
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetCharValue(this char value)
        {
            int asciiValue = value;

            int offset = char.IsLower(value) ? 'a' - 1 : 'A' - 27;

            return asciiValue - offset;
        }

        /// <summary>
        /// Splits a string by a given substring
        /// </summary>
        /// <param name="input"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static List<string> SplitSubstring(this string input, string split) {
            return input.Split(split).Where(l => l != split).ToList();
        }

        /// <summary>
        /// Chunks a list based on a predicate. The element that matches the predicate is not included in a chunk 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<List<T>> ChunkByExclusive<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            return list.ChunkBy(predicate, false);
        }

        /// <summary>
        /// Chunks a list based on a predicate. The element that matches the predicate is included in the current chunk
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<List<T>> ChunkByInclusive<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            return list.ChunkBy(predicate, true);
        }

        private static List<List<T>> ChunkBy<T>(this IEnumerable<T> list, Func<T, bool> predicate, bool inclusive)
        {
            List<List<T>> resultList = new();
            List<T> result = new();

            foreach (T item in list)
            {
                if (predicate(item))
                {
                    if (inclusive)
                    {
                        result.Add(item);
                    }

                    resultList.Add(result);
                    result = new();
                }
                else
                {
                    result.Add(item);
                }
            }

            // Add the last chunk if needed
            if (result.Any())
            {
                resultList.Add(result);
            }

            return resultList;
        }

        /// <summary>
        /// A quick version of regex that assumes we're receiving 1 line and only want the first match
        /// </summary>
        /// <param name="input"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static List<string> QuickRegex(this string input, string regex)
        {
            List<string> response = new();

            Regex rx = new(regex);

            MatchCollection match = rx.Matches(input);

            if (match.Any())
            {
                GroupCollection groups = match.First().Groups;

                // Not starting at i = 0 because that returns the whole match, we only care about the captures
                for (int i = 1; i < groups.Count; i++)
                {
                    Group group = groups[i];

                    response.Add(group.Value);
                }
            }

            return response;
        }

        /// <summary>
        /// A quick version of regex that assumes that each element of the list is 1 line and only want the first match
        /// </summary>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static List<List<string>> QuickRegex(this IEnumerable<string> input, string regex)
        {
            return input.Select(i => i.QuickRegex(regex)).ToList();
        }

        /// <summary>
        /// Parses a list of strings into a list of ints
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static List<int> ToInts(this IEnumerable<string> strings)
        {
            return strings.Select(int.Parse).ToList();
        }

        /// <summary>
        /// Parses a list of list of strings into a list of list of ints
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static List<List<int>> ToInts(this IEnumerable<IEnumerable<string>> strings)
        {
            return strings.Select(ToInts).ToList();
        }

        /// <summary>
        /// Parses a string to a list of ints where each char becomes an int
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> ToInts(this string input) {
            return input.Select(i => i.ToString()).ToInts();
        }

        /// <summary>
        /// Parses a list of strings into a list of longs
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static List<long> ToLongs(this IEnumerable<string> strings)
        {
            return strings.Select(long.Parse).ToList();
        }

        /// <summary>
        /// Pivot's a 2D list
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<List<T>> Pivot<T>(this IEnumerable<IEnumerable<T>> list)
        {
            return list
                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.ToList())
                .ToList();
        }

        /// <summary>
        /// Reverses a list in place
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ReverseInPlace<T>(this List<T> list)
        {
            list.Reverse();

            return list.ToList();
        }

        /// <summary>
        /// Mathematical mod (As opposed to C#'s remainder operator '%')
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int Mod(int num, int mod) {
            if (num >= 0) {
                return num % mod;
            }
            else {
                return num + (int)Math.Ceiling((double)Math.Abs(num) / mod) * mod;
            }
        }

        /// <summary>
        /// Mathematical mod (As opposed to C#'s remainder operator '%')
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static long Mod(long num, long mod) {
            if (num >= 0) {
                return num % mod;
            }
            else {
                return num + (long)Math.Ceiling((double)Math.Abs(num) / mod) * mod;
            }
        }

        /// <summary>
        /// Get all permutations for the list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            return length == 1
                ? list.Select(t => new T[] { t })
                : list.GetPermutations(length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        
        /// <summary>
        /// Simplified ToString for char array
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string CharsToString(this char[] chars) {
            return new string(chars);
        }

        /// <summary>
        /// Simplified ToString for char list
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string CharsToString(this IEnumerable<char> chars) {
            return new string(chars.ToArray());
        }

        /// <summary>
        /// Returns a list of indexes in a list based on a condition
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<int> FindIndexes<T>(this IEnumerable<T> list, Func<T, bool> predicate) {
            List<int> indexes = new();

            for (int i = 0; i < list.Count(); i++)
            {
                T item = list.ElementAt(i);

                if (predicate(item))
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }

        /// <summary>
        /// Given a string and desired height, print the ASCII and map it to a readable string
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="height"></param>
        /// <param name="emptyChar"></param>
        /// <param name="textChar"></param>
        /// <returns></returns>
        public static string ParseASCIILetters(IEnumerable<char> characters, int height, char emptyChar = '.', char textChar = '#') {
            string text = string.Join(string.Empty, characters);
            return ParseASCIILetters(text, height, emptyChar, textChar);
        }

        /// <summary>
        /// Given a string and desired height, print the ASCII and map it to a readable string
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="height"></param>
        /// <param name="emptyChar"></param>
        /// <param name="textChar"></param>
        /// <returns></returns>
        public static string ParseASCIILetters(string characters, int height, char emptyChar = '.', char textChar = '#') {
            List<int> availableHeights = new(){6, 10};
            if (!availableHeights.Contains(height)) {
                throw new Exception($"There is no mapping for height: {height}, only {string.Join(", ", availableHeights)}.");
            }

            string formattedOutput = characters.Replace(emptyChar, '.').Replace(textChar, '#');
            IEnumerable<char[]> outputRows = formattedOutput.Chunk(characters.Length/height);

            foreach (char[] outputLine in outputRows)
            {
                string value = new(outputLine);
                Console.WriteLine(value.Replace('#', '█').Replace('.', ' '));
            }

            IEnumerable<string> pivotedOutput = outputRows.Pivot().Select(r => r.CharsToString());
            List<List<string>> rotatedLetters = pivotedOutput.ChunkByExclusive(x => x == new string('.', height));
            List<List<string>> letters = rotatedLetters.Select(x => x.Pivot().Select(y => y.CharsToString()).ToList()).ToList();

            string parsedOutput = "";

            Dictionary<string, char> mapping = height == 6 ? ASCIIMap6 : ASCIIMap10;

            foreach (List<string> letter in letters) {
                string key = string.Join(string.Empty, letter);

                if (mapping.ContainsKey(key)) {
                    parsedOutput += mapping[key];
                }
                else {
                    parsedOutput += '?';
                }
            }

            return parsedOutput;
        }

        // ABCEFGHIJKLOPRSUYZ
        private static readonly Dictionary<string, char> ASCIIMap6 = new(){
            {
                """
                .##.
                #..#
                #..#
                ####
                #..#
                #..#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'A'
            },
            {
                """
                ###.
                #..#
                ###.
                #..#
                #..#
                ###.
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'B'
            },
            {
                """
                .##.
                #..#
                #...
                #...
                #..#
                .##.
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'C'
            },
            {
                """
                ####
                #...
                ###.
                #...
                #...
                ####
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'E'
            },
            {
                """
                ####
                #...
                ###.
                #...
                #...
                #...
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'F'
            },
            {
                """
                .##.
                #..#
                #...
                #.##
                #..#
                .###
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'G'
            },
            {
                """
                #..#
                #..#
                ####
                #..#
                #..#
                #..#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'H'
            },
            {
                """
                ###
                .#.
                .#.
                .#.
                .#.
                ###
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'I'
            },
            {
                """
                ..##
                ...#
                ...#
                ...#
                #..#
                .##.
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'J'
            },
            {
                """
                #..#
                #.#.
                ##..
                #.#.
                #.#.
                #..#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'K'
            },
            {
                """
                #...
                #...
                #...
                #...
                #...
                ####
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'L'
            },
            {
                """
                .##.
                #..#
                #..#
                #..#
                #..#
                .##.
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'O'
            },
            {
                """
                ###.
                #..#
                #..#
                ###.
                #...
                #...
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'P'
            },
            {
                """
                ###.
                #..#
                #..#
                ###.
                #.#.
                #..#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'R'
            },
            {
                """
                .###
                #...
                #...
                .##.
                ...#
                ###.
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'S'
            },
            {
                """
                #..#
                #..#
                #..#
                #..#
                #..#
                .##.
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'U'
            },
            {
                """
                #...#
                #...#
                .#.#.
                ..#..
                ..#..
                ..#..
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'Y'
            },
            {
                """
                ####
                ...#
                ..#.
                .#..
                #...
                ####
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'Z'
            }
        };
    
        // ABCEFGHJKLNPRXZ
        private static readonly Dictionary<string, char> ASCIIMap10 = new(){
            {
                """
                ..##..
                .#..#.
                #....#
                #....#
                #....#
                ######
                #....#
                #....#
                #....#
                #....#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'A'
            },
            {
                """
                #####.
                #....#
                #....#
                #....#
                #####.
                #....#
                #....#
                #....#
                #....#
                #####.
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'B'
            },
            {
                """
                .####.
                #....#
                #.....
                #.....
                #.....
                #.....
                #.....
                #.....
                #....#
                .####.
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'C'
            },
            {
                """
                ######
                #.....
                #.....
                #.....
                #####.
                #.....
                #.....
                #.....
                #.....
                ######
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'E'
            },
            {
                """
                ######
                #.....
                #.....
                #.....
                #####.
                #.....
                #.....
                #.....
                #.....
                #.....
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'F'
            },
            {
                """
                .####.
                #....#
                #.....
                #.....
                #.....
                #..###
                #....#
                #....#
                #...##
                .###.#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'G'
            },
            {
                """
                #....#
                #....#
                #....#
                #....#
                ######
                #....#
                #....#
                #....#
                #....#
                #....#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'H'
            },
            {
                """
                ...###
                ....#.
                ....#.
                ....#.
                ....#.
                ....#.
                ....#.
                #...#.
                #...#.
                .###..
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'J'
            },
            {
                """
                #....#
                #...#.
                #..#..
                #.#...
                ##....
                ##....
                #.#...
                #..#..
                #...#.
                #....#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'K'
            },
            {
                """
                #.....
                #.....
                #.....
                #.....
                #.....
                #.....
                #.....
                #.....
                #.....
                ######
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'L'
            },
            {
                """
                #....#
                ##...#
                ##...#
                #.#..#
                #.#..#
                #..#.#
                #..#.#
                #...##
                #...##
                #....#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'N'
            },
            {
                """
                #####.
                #....#
                #....#
                #....#
                #####.
                #.....
                #.....
                #.....
                #.....
                #.....
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'P'
            },
            {
                """
                #####.
                #....#
                #....#
                #....#
                #####.
                #..#..
                #...#.
                #...#.
                #....#
                #....#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'R'
            },
            {
                """
                #....#
                #....#
                .#..#.
                .#..#.
                ..##..
                ..##..
                .#..#.
                .#..#.
                #....#
                #....#
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'X'
            },
            {
                """
                ######
                .....#
                .....#
                ....#.
                ...#..
                ..#...
                .#....
                #.....
                #.....
                ######
                """.Replace("\r", string.Empty).Replace("\n", string.Empty)
                ,'Z'
            }
        };
    
        /// <summary>
        /// Get the input file line by line
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <param name="example"></param>
        /// <returns></returns>
        private static List<string> GetInputLines(int year, int day, bool example = false) {
            return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", year.ToString(), $"{day:D2}{(example ? "_example" : string.Empty)}.txt")).ToList();
        }
    }
}