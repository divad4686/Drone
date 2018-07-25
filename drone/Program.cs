using System;
using System.IO;
using DroneNamespace;

namespace drone
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var result = DroneHelpers.ExecuteProgram(input);
            Console.WriteLine(result);
        }
    }
}
