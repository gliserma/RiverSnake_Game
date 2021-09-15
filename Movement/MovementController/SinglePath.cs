using System;
using SnakeApp.GameObjects.Structures;

namespace SnakeApp.Movement.MovementController
{
    /// <summary>
    /// A trajectory in which an object will only move in one direction,
    /// never changing course.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class SinglePath : IMovementController
    {
        public BendableLine Bendable { get; set; }
        public Direction CurrentDirection { get; set; }

        /// <summary>
        /// Constructs a new SinglePath controller with the direction,
        /// which will never change.
        /// </summary>
        /// <param name="initialDirection"></param>
        public SinglePath(Direction initialDirection)
        {
            CurrentDirection = initialDirection;
        }

        /// <summary>
        /// By definition, the direciton will never change in this implementation
        /// of the interface.
        /// </summary>
        public void ChangeDirection() { }

        /// <summary>
        /// Determines which space to enter next.
        /// </summary>
        /// <returns>Space</returns>
        public Space GetNextSpace()
        {
            Space current = Bendable.GetLeadSpace();
            return current.GetNeighbor(CurrentDirection);
        }
    }
}
