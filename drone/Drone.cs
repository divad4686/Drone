using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroneNamespace
{
    public enum Direction { N, E, S, W }
    public enum Movement { L, R, M }

    public class Drone
    {
        public Direction Direction { get; private set; }
        public (int x, int y) Position { get; private set; }

        public Drone(Direction direction, (int x, int y) position)
        {
            Direction = direction;
            Position = position;
        }

        public void RotateLeft()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.W;
                    break;
                case Direction.W:
                    Direction = Direction.S;
                    break;
                case Direction.S:
                    Direction = Direction.E;
                    break;
                case Direction.E:
                    Direction = Direction.N;
                    break;
            }
        }

        public void RotateRight()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.E;
                    break;
                case Direction.W:
                    Direction = Direction.N;
                    break;
                case Direction.S:
                    Direction = Direction.W;
                    break;
                case Direction.E:
                    Direction = Direction.S;
                    break;
            }
        }

        public void MoveForward((int w, int h) area)
        {
            (int x, int y) newPosition = (0, 0);
            switch (Direction)
            {
                case Direction.N:
                    newPosition = (Position.x, Position.y + 1);
                    break;
                case Direction.E:
                    newPosition = (Position.x + 1, Position.y);
                    break;
                case Direction.S:
                    newPosition = (Position.x, Position.y - 1);
                    break;
                case Direction.W:
                    newPosition = (Position.x - 1, Position.y);
                    break;
            }

            if (newPosition.x >= 0 && newPosition.y >= 0 && newPosition.x <= area.w && newPosition.y <= area.h)
                Position = newPosition;
        }

        public void MoveDrone(Movement move, (int w, int h) area)
        {
            switch (move)
            {
                case Movement.L:
                    RotateLeft();
                    break;
                case Movement.R:
                    RotateRight();
                    break;
                case Movement.M:
                    MoveForward(area);
                    break;
            }
        }

        public string DroneToString() => $"{Position.x} {Position.y} {Direction.ToString()}";

        public string ExecuteMovements(List<Movement> movements, (int w, int h) area)
        {
            foreach (var move in movements)
            {
                MoveDrone(move, area);
            }

            return DroneToString();
        }
    }
}