using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Services
{
    public class LogService
    {
        public static void Log(string message, [System.Runtime.CompilerServices.CallerMemberName] string sourceMethod = "unknown class")
        {
            Console.WriteLine($"[{DateTime.Now}] [{sourceMethod}] {message}");
        }
    }
}
