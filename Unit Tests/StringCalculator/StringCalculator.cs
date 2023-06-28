

namespace Unit_Tests.StringCalculator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return 0;

            List<int> negatives = new();
            string[] delimiters = GetDelimiters(numbers, out string sanitizedNumbers);

            int sum = 0;
            string[] numberList = sanitizedNumbers.Split(delimiters, StringSplitOptions.None);

            foreach (string number in numberList)
            {
                int value = int.Parse(number);
                if (value < 0)
                    negatives.Add(value);
                else if (value <= 1000)
                    sum += value;
            }

            if (negatives.Any())
            {
                throw new Exception("Negatives not allowed: " + string.Join(", ", negatives));
            }

            return sum;
        }

        private static string[] GetDelimiters(string numbers, out string sanitizedNumbers)
        {
            if (numbers.StartsWith("//"))
            {
                int delimiterEndIndex = numbers.IndexOf('\n');
                string delimiterLine = numbers[2..delimiterEndIndex];
                sanitizedNumbers = numbers[(delimiterEndIndex + 1)..];
                return ExtractDelimiters(delimiterLine);
            }
            else if (!char.IsDigit(numbers[0]) && numbers[0]!= '-' && !char.IsDigit(numbers[1]))
            {
                string delimiterLine = numbers[0].ToString();
                numbers = numbers[1..];
                sanitizedNumbers = numbers.Replace("\n", "");
                return new string[] { delimiterLine };
            }

            sanitizedNumbers = numbers;
            return new string[] { ",", "\n" };
        }

        private static string[] ExtractDelimiters(string delimiterLine)
        {
            List<string> delimiters = new();

            int startPos = 0;
            int endPos = delimiterLine.IndexOf("][");

            if (endPos == -1)
            {
                delimiters.Add(GetDelimiter(delimiterLine.Trim('[', ']')));
            }
            else
            {
                while (endPos != -1)
                {
                    string delimiter = delimiterLine.Substring(startPos + 1, endPos - startPos - 1).Trim('[', ']');
                    delimiters.Add(GetDelimiter(delimiter));
                    startPos = endPos;
                    endPos = delimiterLine.IndexOf("][", startPos + 1);
                }

                delimiters.Add(GetDelimiter(delimiterLine.Substring(startPos + 1, delimiterLine.Length - startPos - 2).Trim('[', ']')));
            }

            return delimiters.ToArray();
        }

        private static string GetDelimiter(string delimiter)
        {
            if (delimiter.StartsWith("[") && delimiter.EndsWith("]"))
                return delimiter[1..^1];
            else
                return delimiter;
        }
    }
}