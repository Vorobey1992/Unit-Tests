

namespace Unit_Tests.LCDDisplay
{
    internal class LCDDisplay
    {
        private readonly string[] digits = {
            "._.\n|.|\n|_|",    // 0
            "...\n..|\n..|",    // 1
            "._.\n._|\n|_.",    // 2
            "._.\n._|\n._|",    // 3
            "...\n|_|\n..|",     // 4
            "._.\n|_.\n._|",    // 5
            "._.\n|_.\n|_|",    // 6
            "._.\n..|\n..|",    // 7
            "._.\n|_|\n|_|",    // 8
            "._.\n|_|\n..|"     // 9
        };

        public string GenerateLCDDisplay(int number)
        {
            string numberString = number.ToString();
            string lcdDisplay = "";

            for (int i = 0; i < 3; i++)
            {
                foreach (char digitChar in numberString)
                {
                    int digit = int.Parse(digitChar.ToString());
                    string digitDisplay = digits[digit];
                    string[] lines = digitDisplay.Split('\n');

                    if (i < lines.Length)
                        lcdDisplay += lines[i] + " ";
                }

                lcdDisplay += "\n";
            }

            return lcdDisplay.TrimEnd('\n');
        }
    }
}
