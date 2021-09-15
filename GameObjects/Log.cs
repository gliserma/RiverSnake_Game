using System;
using SnakeApp.GameObjects.Structures;
using SnakeApp.Movement;
using SnakeApp.Movement.MovementController;

namespace SnakeApp.GameObjects
{
    /// <summary>
    /// Represents an obstacle in the game for the snake to avoid.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Log : BendableLine
    {
        public Log(Space start, int length) :
            base(start, length, ConsoleColor.DarkGray,
                new SinglePath(Directions.GetDirection(DirectionName.Left)),
                false, true, false)
        {
            
        }
    }
}
