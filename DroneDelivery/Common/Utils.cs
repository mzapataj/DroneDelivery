using System.Diagnostics;
using System.Text;


namespace DroneDelivery.Common
{
    public static class Utils
    {
        public static void OpenFile(string pathFileTxt, string programExe = "\\notepad.exe")
        {
            string notepadPath = Environment.SystemDirectory + programExe;

            var startInfo = new ProcessStartInfo(notepadPath)
            {
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = pathFileTxt
            };

            Process.Start(startInfo);
        }


        public static string GenerateRandomInput(int droneNumbers = 100, int locationNumbers = 1000, int maxWeight = 1000)
        {
            var random = new Random();

            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            int[] sequence = Enumerable.Range(1, 100_000).ToArray();

            var x = alpha.Length * sequence.Length;

            var strBuilder = new StringBuilder();

            strBuilder.Append($"[DroneA1], [{maxWeight}]");

            if (1 < droneNumbers - 1)
            {
                strBuilder.Append(", ");
            }

            for (int i = 1; i < droneNumbers; i++)
            {
                var droneName = $"Drone{alpha[i % alpha.Length]}{sequence[(int)Math.Floor((float)i / alpha.Length)]}";
                var randomWeight = 1 + random.NextDouble() * (maxWeight - 1);

                strBuilder.Append($"[{droneName}], [{randomWeight}]");

                if (i < droneNumbers - 1)
                {
                    strBuilder.Append(", ");
                }
            }

            strBuilder.AppendLine();

            for (int i = 0; i < locationNumbers; i++)
            {
                var locationName = $"Location{alpha[i % alpha.Length]}{sequence[(int)Math.Floor((float)i / alpha.Length)]}";
                var randomWeight = 1 + random.NextDouble() * (maxWeight - 1);

                strBuilder.Append($"[{locationName}], [{randomWeight}]");

                if (i < locationNumbers - 1)
                {
                    strBuilder.AppendLine();
                }
            }


            File.WriteAllText("RandomInput.txt", strBuilder.ToString());

            return strBuilder.ToString();
        }
    }
}
