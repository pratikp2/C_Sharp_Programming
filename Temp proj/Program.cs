using Microsoft.Win32;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HP
{
    class Program
    {
        static void Main()
        {
            string arg = "2.5";
            double? tp = null;
            tp = Convert.ToDouble(arg);
            return;
        }

    }
}