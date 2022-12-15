using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Services
{
    public class Solution2015_04Service : ISolutionDayService
    {
        public Solution2015_04Service() { }

        public async Task<string> FirstHalf(bool send)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2015_04.txt"));
            data = data.Remove(data.Length - 1, 1); // Remove the newline character

            int addedDigit = 1;
            MD5 md5 = MD5.Create();

            while (true)
            {
                string calculatedString = $"{data}{addedDigit}";
                byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(calculatedString));

                StringBuilder hex = new(result.Length * 2);
                foreach (byte b in result)
                {
                    _ = hex.AppendFormat("{0:x2}", b);
                }

                string hexString = hex.ToString();

                // Check for 5 leading 0s in hexadecinal
                if (hexString[0] == '0' &&
                hexString[1] == '0' &&
                hexString[2] == '0' &&
                hexString[3] == '0' &&
                hexString[4] == '0')
                {
                    break;
                }

                addedDigit++;
            }

            return await Task.FromResult($"The lowest number {data} combines with to make an MD5 hash starting with five zeroes is {addedDigit}.");
        }

        public async Task<string> SecondHalf(bool send)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2015_04.txt"));
            data = data.Remove(data.Length - 1, 1); // Remove the newline character

            int addedDigit = 1;
            MD5 md5 = MD5.Create();

            while (true)
            {
                string calculatedString = $"{data}{addedDigit}";
                byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(calculatedString));

                StringBuilder hex = new(result.Length * 2);
                foreach (byte b in result)
                {
                    _ = hex.AppendFormat("{0:x2}", b);
                }

                string hexString = hex.ToString();

                // Check for 5 leading 0s in hexadecinal
                if (hexString[0] == '0' &&
                hexString[1] == '0' &&
                hexString[2] == '0' &&
                hexString[3] == '0' &&
                hexString[4] == '0' &&
                hexString[5] == '0')
                {
                    break;
                }

                addedDigit++;
            }

            return await Task.FromResult($"The lowest number {data} combines with to make an MD5 hash starting with six zeroes is {addedDigit}.");
        }
    }
}
