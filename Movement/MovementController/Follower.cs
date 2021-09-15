using System;
using SnakeApp.GameObjects.Structures;

namespace SnakeApp.Movement.MovementController
{
    /// <summary>
    /// Instructs this BendableLine to follow another BendableLine.
    ///
    /// Useful in coordinating the movement within a CompositeLine.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Follower : IMovementController
    {
        public Direction CurrentDirection { get; set; }
        public BendableLine Bendable { get; set; }
        public BendableLine Following { get; }

        /// <summary>
        /// Constructor which specifies the BendableLine to follow.
        /// </summary>
        /// <param name="following">the BendableLine to follow</param>
        public Follower(BendableLine following)
        {
            this.Following = following;
            ChangeDirection();

            // GET DIRECTION from the FOLLOWED BendableLine
            //CurrentDirection = this.Following.movementController.CurrentDirection;
        }

        /// <summary>
        /// Chooses its next space by finding the space recently discarded by the
        /// BendableLine it is following.
        /// </summary>
        /// <returns></returns>
        public Space GetNextSpace() {return this.Following.ExitedSpace;}

        /// <summary>
        /// Gets directional information from the BendableLine being followed.
        /// </summary>
        public void ChangeDirection()
        {
            // GET CURRENT DIRECTION & FRAMES PER MOVE from following
            CurrentDirection = this.Following.movementController.CurrentDirection;
        }
    }
}
