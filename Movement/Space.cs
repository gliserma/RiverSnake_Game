using System;
using SnakeApp.Background.BoxTypes;
using SnakeApp.GameObjects.Structures;

namespace SnakeApp.Movement
{
    /// <summary>
    /// Responsible for maintaining the state of a delimited
    /// portion of the game area given by its coordinates.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Space
    {
        public Coordinates Location { get; }
        public Box Box { get; }
        public bool Empty { get; set; }
        public bool CollisionHereEndsGame { get; set; }
        public Neighbors Neighbors { get;  set; }

        /// <summary>
        /// Constructs a new Space at the inputted loaction.
        /// </summary>
        /// <param name="location"></param>
        public Space(Coordinates location, Box box)
        {
            this.Location = location;
            this.Box = box;
            this.Empty = this.Box.Properties.EmptyByDefault;
            this.CollisionHereEndsGame = false;
        }

        /// <summary>
        /// Tests whether an object is allowed to enter a space given
        /// the properties of the object and space.
        /// </summary>
        /// <param name="collisionEndsGame"></param>
        /// <returns>true if object can enter the space, false otherwise</returns>
        public bool CanEnterSpace(BendableLine bl)
        {
            // SCENARIO 1:
            // if the object entering the space is collidable and there
            // is already a game object here for which a collision ends
            // the game.
            if (bl.Collidable && CollisionHereEndsGame) { return false;  }
            // SECNARIO 2:
            // If the object entering the space will end the game upon
            // collision AND the space is not empty.
            else if (bl.CollisionEndsGame && !Empty) { return false; }
            return true;
        }

        /// <summary>
        /// Notes that an object has entered the space.
        /// </summary>
        /// <param name="bl"></param>
        public void EnterSpace(BendableLine bl)
        {
            Empty = bl.Collidable;
            CollisionHereEndsGame = bl.CollisionEndsGame;
        }

        /// <summary>
        /// Notes that an object has left the space and resets the status
        /// accordintly.
        /// </summary>
        /// <param name="repaint"></param>
        public void ExitSpace(bool repaint)
        {
            Empty = Box.Properties.EmptyByDefault;
            CollisionHereEndsGame = false;
            if (repaint) { Painter.PaintSpace(Box.Properties.Color, this); }
        }

        /// <summary>
        /// Facilitates movement between spaces in a given direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Space GetNeighbor(Direction direction)
        {
            return Neighbors.getNeighbor(direction.Name);
        }
    }
}
