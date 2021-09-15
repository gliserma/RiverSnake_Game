using System;
using SnakeApp.GameObjects.Structures;
using SnakeApp.Movement.MovementController;
using SnakeApp.Movement;

namespace SnakeApp.GameObjects
{
    /// <summary>
    /// Represents a snake with a head and tail of two different colors.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Snake : CompositeLine
    {
        /// <summary>
        /// Constructs a new Snake.
        ///
        /// The tail automatically follows the head so the only IMovementController
        /// needed is for the snake's head.
        /// </summary>
        /// <param name="start">Space where the Snake is generated</param>
        /// <param name="headController">The IMovementController for the Snake's head</param>
        public Snake(Space start, IMovementController headController) : base(start, true, true)
        {
            int snakeHead = 2;
            base.AddLine(new Coordinates(0, 0), snakeHead, headController,
                true, ConsoleColor.DarkYellow, 0);

            base.AddLine(new Coordinates(0, 0), 20,
                new Follower(base.Head),
                false, ConsoleColor.DarkRed, snakeHead);
        }
    }
}
