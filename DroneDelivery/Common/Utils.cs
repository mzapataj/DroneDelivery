using System.Diagnostics;

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
    }
}
