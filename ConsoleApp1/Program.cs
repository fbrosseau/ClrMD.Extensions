using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClrMD.Extensions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ClrMDSession s = ClrMDSession.LoadCrashDump(@"E:\Dumps\GenetecMediaRouterexe_190122_123002\GenetecMediaRouter.exe_190122_123002.dmp");
            object yo = s.EnumerateDynamicObjects("System.Net.IPEndPoint").Take(25).Select(o => o.SimpleDisplayValue).ToList();
            yo = s.EnumerateDynamicObjects("System.Net.DnsEndPoint").Select(o => o.SimpleDisplayValue).ToList();
        }
    }
}
