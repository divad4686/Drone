using System;
using System.Collections.Generic;
using System.Linq;

namespace DroneNamespace
{
    public class DroneParser
    {
        public static (int w, int h) ParseArea(string area)
        {
            var values = area.Split(" ");
            return (int.Parse(values[0]), int.Parse(values[1]));
        }

        public static Drone ParseDrone(string data)
        {
            var splitData = data.Split(" ");
            return new Drone(Enum.Parse<Direction>(splitData[2]), (int.Parse(splitData[0]), int.Parse(splitData[1])));
        }

        public static List<Movement> ParseMovements(string movements) =>
            movements.Select(m => Enum.Parse<Movement>(m.ToString())).ToList();
    }
}