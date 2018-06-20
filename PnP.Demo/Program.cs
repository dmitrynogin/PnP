using System;
using static System.Console;
using static System.String;

namespace PnP.Demo
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (var ports = new ComPortList())
            {
                WriteLine(Join("\n", ports));
                ports.Subscribe(WriteLine);                
                ReadLine();
            }
        }        
    }
}
