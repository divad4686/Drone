using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroneNamespace
{
    public enum Direction {N,E,S,W}
    public enum Movement {L,R,M}

    public class Drone
    {
        public readonly Direction Direction;
        public readonly (int x,int y) Position;

        public Drone(Direction direction,(int x,int y) position)
        {
            Direction = direction;
            Position = position;
        }
    }

    public static class DroneHelpers
    {
            public static Direction RotateLeft(Direction current){ 
            switch (current)
            {
                case Direction.N:
                    return Direction.W;
                case Direction.W:
                    return Direction.S;
                case Direction.S:
                    return Direction.E;
                case Direction.E:
                    return Direction.N;
            }
            throw new Exception("Something wrong");
        }

        public static Direction RotateRight(Direction current){ 
            switch (current)
            {
                case Direction.N:
                    return Direction.E;
                case Direction.W:
                    return Direction.N;
                case Direction.S:
                    return Direction.W;
                case Direction.E:
                    return Direction.S;
            }
            throw new Exception("Something wrong");
        }

        public static Drone MoveForward (Drone current,(int w, int h) area)
        {
            (int x,int y) newPosition = (0,0); 
            switch(current.Direction)
            {
                case Direction.N:
                    newPosition = (current.Position.x, current.Position.y + 1);
                    break;
                case Direction.E:
                    newPosition = (current.Position.x + 1, current.Position.y);
                    break;
                case Direction.S:
                    newPosition = (current.Position.x, current.Position.y - 1);
                    break;
                case Direction.W:
                    newPosition = (current.Position.x - 1, current.Position.y);
                    break;
            }

            if(newPosition.x >=0 && newPosition.y >= 0 && newPosition.x <= area.w && newPosition.y <= area.h)
                return new Drone(current.Direction,newPosition);

            return current;
        }

        public static Drone MoveDrone(Drone current, Movement move, (int w,int h) area)
        {
            switch(move)
            {
                case Movement.L:
                    return new Drone(RotateLeft(current.Direction),current.Position);
                case Movement.R:
                    return new Drone(RotateRight(current.Direction),current.Position);
                case Movement.M:
                    return MoveForward(current,area);
            }

            throw new Exception("invalid movement");
        }

        public static (int w,int h) ParseArea(string area)
        {
            var values = area.Split(" ");
            return (int.Parse(values[0]), int.Parse(values[1]));
        }

        public static Drone ParseDrone(string data)
        {
            var splitData = data.Split(" ");
            return new Drone(Enum.Parse<Direction>(splitData[2]),(int.Parse(splitData[0]), int.Parse(splitData[1])));
        }

        public static List<Movement> ParseMovements(string movements) =>
            movements.Select(m => Enum.Parse<Movement>(m.ToString())).ToList();

        public static string ExecuteDrone(Drone drone,List<Movement> movements,(int w,int h)area)
        {
            foreach(var move in movements)
            {
                drone = MoveDrone(drone,move,area);
            }

            return drone.DroneToString();
        }

        public static string ExecuteProgram(string input)
        {
            StringBuilder result = new StringBuilder();
            var data = input.Split(Environment.NewLine);
            var area = ParseArea(data[0]);
            for(int i = 1;i<data.Count();i++)
            {
                var drone = ParseDrone(data[i]);
                i++;
                var movements = ParseMovements(data[i]);

                var droneResult = ExecuteDrone(drone,movements,area);

                result.AppendLine(droneResult);
            }

            return result.ToString();
        }
        public static string DroneToString(this Drone drone) => $"{drone.Position.x} {drone.Position.y} {drone.Direction.ToString()}";
    }
}