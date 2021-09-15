using System;
using SnakeApp.GameObjects.Structures;
using SnakeApp.Movement;
using SnakeApp.Movement.MovementController;

namespace SnakeApp.GameObjects
{
    /// <summary>
    /// Represents a tree that scrolls by in the background.
    ///
    /// Moves slowly in comparison with the snake and logs to create a sense of
    /// parallax, which adds depth to the scene.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Tree : CompositeLine
    {
        public Tree(Space origin) : base(origin, false, false)
        {
            // GREENERY
            base.AddLine(new Coordinates(0,0), 6, new SinglePath(Directions.GetDirection(DirectionName.Left)), false, ConsoleColor.DarkGreen, 1);
            base.AddLine(new Coordinates(0,1), 8, new SinglePath(Directions.GetDirection(DirectionName.Left)), false, ConsoleColor.DarkGreen, 0);
            base.AddLine(new Coordinates(0,2), 8, new SinglePath(Directions.GetDirection(DirectionName.Left)), false, ConsoleColor.DarkGreen, 0);

            // BASE
            base.AddLine(new Coordinates(0, 3), 2, new SinglePath(Directions.GetDirection(DirectionName.Left)), false, ConsoleColor.DarkYellow, 3);
            base.AddLine(new Coordinates(0, 4), 2, new SinglePath(Directions.GetDirection(DirectionName.Left)), false, ConsoleColor.DarkYellow, 3);
        }
    }
}