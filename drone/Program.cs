using System;
using System.IO;
using System.Linq;
using System.Text;
using static DroneNamespace.DroneParser;

namespace DroneNamespace
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var result = ExecuteProgram(input);
            Console.WriteLine(result);
        }

        public static string ExecuteProgram(string input)
        {
            StringBuilder result = new StringBuilder();
            var data = input.Split(Environment.NewLine);
            var area = ParseArea(data[0]);
            for (int i = 1; i < data.Count(); i++)
            {
                var drone = ParseDrone(data[i]);
                i++;
                var movements = ParseMovements(data[i]);

                var droneResult = drone.ExecuteMovements(movements, area);

                result.AppendLine(droneResult);
            }

            return result.ToString();
        }
    }
}
