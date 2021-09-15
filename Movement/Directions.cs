using System;
namespace SnakeApp.Movement
{
    /// <summary>
    /// Holds all the possible directions with
    /// their corresponding enum, x & y values.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public static class Directions
    {
        private static Direction[] directions = new Direction[] {
                new Direction(0, -1, DirectionName.Up),
                new Direction(0, 1, DirectionName.Down),
                new Direction(-1, 0, DirectionName.Left),
                new Direction(1, 0, DirectionName.Right)
            };

        /// <summary>
        /// Returns the Direction object (with x & y values) for the given
        /// name of that direction.
        /// </summary>
        /// <param name="name">Enum with desired direction name</param>
        /// <returns></returns>
        public static Direction GetDirection(DirectionName name)
        {
            return directions[(int)name];
        }

        /// <summary>
        /// Given the axis of the current direction (i.e. up-down or left-right),
        /// returns the two directions along the other axis.
        ///
        /// Useful for AI to make decisions about whether to keep moving forward
        /// or to change directions (n.b. full stop reverse of directions would
        /// result in a collision, hence why we only want directions along the
        /// other axis).
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static Direction[] GetDirectionsOnOtherAxis(Direction current)
        {
            Direction[] otherDirections = new Direction[2];
            if (current.X == 1 || current.X == -1) // CURRENTLY MOVING LEFT or RIGHT
            {
                otherDirections[0] = Directions.GetDirection(DirectionName.Up);
                otherDirections[1] = Directions.GetDirection(DirectionName.Down);
            }
            else // CURRENTLY MOVING UP or DOWN
            {
                otherDirections[0] = Directions.GetDirection(DirectionName.Left);
                otherDirections[1] = Directions.GetDirection(DirectionName.Right);
            }
            return otherDirections;
        }
    }

    /// <summary>
    /// Vector to move from one space to a neighbor, either
    /// horizontally or vertically.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public struct Direction
    {
        public int X { get; }
        public int Y { get; }
        public DirectionName Name { get; }
      
        public Direction(int x, int y, DirectionName name)
        {
            this.X = x;
            this.Y = y;
            this.Name = name;
        }

        override
        public string ToString()
        {
            return $"{Name} ({this.X},{this.Y})";
        }
      
    }

    /// <summary>
    /// Represents the possible movement pathways in this game. 
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public enum DirectionName { Up, Down, Left, Right }


}
