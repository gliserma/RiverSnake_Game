using System;
using SnakeApp.GameManagement;
using SnakeApp.Movement;
using SnakeApp.Movement.MovementController;

namespace SnakeApp.GameObjects.Structures
{

    /// <summary>
    /// A line that can change its shape as it moves but
    /// maintains the same length.
    /// 
    /// Composed of spaces on the console; as the spaces
    /// change, each element in the line follows the path of the
    /// previous element. Essentially, this means they follow the
    /// path of the head element.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class BendableLine
    {
        public CircularQueue Segments { get; }
        public ConsoleColor Color { get; }
        public bool CollisionEndsGame { get; }
        public bool Collidable { get; }
        private bool Followed { get; }
        public bool Alive { get; set; }
        public bool TotallyDead { get; set; }
        public IMovementController movementController;
        public Space ExitedSpace { get; private set; }

        /// <summary>
        /// Constructs a new Bendable Line object.
        /// </summary>
        /// <param name="firstSpace">Space where the BendableLine starts</param>
        /// <param name="size">The length of the BendableLine</param>
        /// <param name="color">The ConsoleColor to be displayed</param>
        /// <param name="mc">The IMovementController that will determine where this
        /// bendable line will move</param>
        /// <param name="followed">true if this BendableLine is followed by another
        /// BendableLine, otherwise false</param>
        /// <param name="collidable">true if this object, upon entering a space,
        /// changes the empty state of that space, otherwise false</param>
        /// <param name="collisionEndsGame">true if game ends when this object
        /// enters a non-empty space, otherwise false</param>
        public BendableLine(Space firstSpace, int size,
            ConsoleColor color, IMovementController mc,
            bool followed, bool collidable, bool collisionEndsGame)
        {
            this.Segments = new CircularQueue(size);
            this.Segments.Enqueue(firstSpace);
            this.Color = color;
            this.Followed = followed;
            this.Collidable = collidable;
            this.CollisionEndsGame = collisionEndsGame;
            Alive = true;
            TotallyDead = false;
            this.movementController = mc;
            mc.Bendable = this;
        }

        /// <summary>
        /// Advances the head of the BendableLine into a new space and
        /// exits the tail from an old space.
        ///
        /// An algorithm encapsulated within the IMovementController determines
        /// where the BendableLine will move. This method executes that
        /// decision, handling the entering and exiting. It also handles
        /// situations in which the BendableLine enters a non-empty space,
        /// determining if a game-ending collision has occurred.
        /// </summary>
        public void Move()
        {
            // STAY COURSE?
             movementController.ChangeDirection();
            // EXIT SPACE
            ExitSpace(!Followed);
            // ENTER NEXT SPACE
            EnterSpace();
            // GONE OFF SCREEN?
            if (GetLeadSpace().Box.Properties.Foreground)
            {
                Alive = false;
            }
        }

        private void ExitSpace(bool restoreColor)
        {
            // IF QUEUE is FULL, DEQUEUE SPACE
            if (Segments.IsFull() || (!Alive && !TotallyDead))
            {
                ExitedSpace = Segments.Dequeue();
                ExitedSpace.ExitSpace(restoreColor);
                // POTENTIALLY RETURN SPACE to BACKGROUND COLOR
            }            
        }

        private void EnterSpace()
        {
            if (Alive)
            {
                Space nextSpace = movementController.GetNextSpace();
                if (nextSpace.CanEnterSpace(this))
                {
                    nextSpace.EnterSpace(this);
                    Painter.PaintSpace(Color, nextSpace);
                    this.Segments.Enqueue(nextSpace);
                }
                else
                {
                    PlayState.GameOver = true;
                }
            }
            else
            {
                if (Segments.IsEmpty())
                {
                    TotallyDead = true;
                }
            }
        }

        /// <summary>
        /// Returns the head space of the BendableLine without
        /// removing it from the data structure.
        ///
        /// Handles cases where the BendableLine is only length 1 and
        /// could be in the middle of entering a new space (which involves
        /// discarding the old space first).
        /// </summary>
        /// <returns>Space</returns>
        public Space GetLeadSpace()
        {
            if (Segments.IsEmpty()) { return ExitedSpace; }
            else { return Segments.PeekAtBack(); }
        }

        /// <summary>
        /// This method is used when the way a BendableLine moves needs to change
        /// </summary>
        /// <param name="mc"></param>
        public void ChangeMovementController(IMovementController mc) { this.movementController = mc; }
    }
}
