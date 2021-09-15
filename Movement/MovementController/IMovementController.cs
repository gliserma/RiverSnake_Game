using System;
using SnakeApp.GameObjects.Structures;

namespace SnakeApp.Movement.MovementController
{
    /// <summary>
    /// Interface that encapsulates the logic for how
    /// a BendableLine in the game can move.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public interface IMovementController
    {
        abstract BendableLine Bendable { get; set; }
        abstract Direction CurrentDirection { get; set; }

        public abstract void ChangeDirection();
        public abstract Space GetNextSpace();
    }
}
