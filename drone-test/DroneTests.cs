using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using DroneNamespace;
using static DroneNamespace.DroneHelpers;


namespace drone_test
{
    
    public class DronteTests
    {
    

        [Fact]
        public void TestRotateLeft()
        {
            var currentDirection = Direction.N;
            currentDirection = RotateLeft(currentDirection);
            Assert.Equal(Direction.W,currentDirection);

            currentDirection = RotateLeft(currentDirection);
            Assert.Equal(Direction.S,currentDirection);

            currentDirection = RotateLeft(currentDirection);
            Assert.Equal(Direction.E,currentDirection);

            currentDirection = RotateLeft(currentDirection);
            Assert.Equal(Direction.N,currentDirection);
        }

        [Fact]
        public void TestRotateRight()
        {
            var currentDirection = Direction.N;
            currentDirection = RotateRight(currentDirection);
            Assert.Equal(Direction.E,currentDirection);

            currentDirection = RotateRight(currentDirection);
            Assert.Equal(Direction.S,currentDirection);

            currentDirection = RotateRight(currentDirection);
            Assert.Equal(Direction.W,currentDirection);

            currentDirection = RotateRight(currentDirection);
            Assert.Equal(Direction.N,currentDirection);
        } 

        

        [Fact]
        public void TestMoveForward()
        {
            var area = (w: 4,h: 4);

            Drone current = new Drone(Direction.N,(x: 3,y: 3));
            current = MoveForward(current,area);
            Assert.Equal((3,4),current.Position);
            Assert.Equal(Direction.N,current.Direction);

            current = MoveForward(current,area); // Can't move anymore because area boundaries
            Assert.Equal((3,4),current.Position);
            Assert.Equal(Direction.N,current.Direction);

        }

        

        [Fact]
        public void TestMoveDrone()
        {
            var area = (w: 5,h: 5);
            Drone current = new Drone(Direction.N,(x: 3,y: 3));
            current = MoveDrone(current,Movement.L,area);
            Assert.Equal(Direction.W,current.Direction);

            current = MoveDrone(current,Movement.M,area);
            Assert.Equal((2,3),current.Position);

            current = MoveDrone(current,Movement.R,area);
            Assert.Equal(Direction.N,current.Direction);
        }

        

        [Fact]
        public void TestParseArea()
        {
            string area = "5 4";
            var parsedArea = ParseArea(area);
            Assert.Equal(5,parsedArea.w);
            Assert.Equal(4,parsedArea.h);
        }

        
        
        [Fact]
        public void ParseRobot()
        {
            string data = "3 2 E";
            var drone = ParseDrone(data);
            Assert.Equal(3,drone.Position.x);
            Assert.Equal(2,drone.Position.y);
            Assert.Equal(Direction.E,drone.Direction);                        
        }

        

        [Fact]
        public void TestParseMovements()
        {
            string movements = "MLR";
            var movementsParse = ParseMovements(movements);
            Assert.Equal(Movement.M,movementsParse[0]);
            Assert.Equal(Movement.L,movementsParse[1]);
            Assert.Equal(Movement.R,movementsParse[2]);
        }

        [Fact]
        public void TestDroneToString()
        {
            var drone = new Drone(Direction.S,(3,4));
            var droneString = drone.DroneToString();

            Assert.Equal("3 4 S",droneString);
        }
    
        

        [Fact]
        public void TestExecuteDrone()
        {
            var area = (5,5);
            var drone = new Drone(Direction.E,(3,3));
            var movements = new List<Movement> {Movement.M,Movement.M,Movement.R,Movement.M,Movement.M,Movement.R,Movement.M,Movement.R,Movement.R,Movement.M};
            var result = ExecuteDrone(drone,movements,area);

            Assert.Equal("5 1 E",result);
        }



        [Fact]
        public void TestExecuteProgram()
        {
            var input = @"5 5
3 3 E
L
3 3 E
MMRMMRMRRM
1 2 N
LMLMLMLMMLMLMLMLMM";

            var result = ExecuteProgram(input);

            var expected = @"3 3 N
5 1 E
1 4 N
";

            Assert.Equal(expected,result);
        }
    }

    public static class DroneHelpers
    {
        

    }
}
