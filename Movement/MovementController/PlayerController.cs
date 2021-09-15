using System;
using SnakeApp.GameManagement;
using SnakeApp.GameObjects.Structures;

namespace SnakeApp.Movement.MovementController
{
    /// <summary>
    /// Lets the player control the movement of BendableLine
    /// by pressing a key down.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class PlayerController : IMovementController
    {
        public BendableLine Bendable { get; set; }
        public Direction CurrentDirection { get; set; }
        private readonly PlayState play;

        /// <summary>
        /// Constructs the PlayerController
        /// </summary>
        /// <param name="framesPerMove"></param>
        /// <param name="game"></param>
        /// <param name="ps">Play State</param>
        public PlayerController(PlayState ps)
        {
            this.play = ps;
            ChangeDirection();
        }

        /// <summary>
        /// Let's the player change the direction of the BendableLine
        /// </summary>
        public void ChangeDirection()
        {
            // GET DIRECTION FROM EVENT LISTENER in PLAY STATE
            CurrentDirection = this.play.Direction;
        }

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
