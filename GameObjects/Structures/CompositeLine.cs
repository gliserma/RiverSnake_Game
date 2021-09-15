using System;
using System.Collections.Generic;
using SnakeApp.Movement.MovementController;
using SnakeApp.Movement;

namespace SnakeApp.GameObjects.Structures
{
    /// <summary>
    /// Joins together multiple BendableLines into a single object.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class CompositeLine
    {
        Space Origin { get; set; }
        bool Collidable { get; set; }
        bool CollisionEndsGame { get; set; }
        public bool TotallyDead { get; set; }
        public BendableLine Head { get; set; }
        private int turn = 0;
        private LinkedList<LinePosition> lines = new LinkedList<LinePosition>();

        /// <summary>
        /// Constructs a new, empty CompositeLine in a given space.
        /// </summary>
        /// <param name="origin">Space in the Game Area Where the CompositeLine should be generated</param>
        public CompositeLine(Space origin, bool collidable, bool collisionEndsGame)
        {
            TotallyDead = false;
            Origin = origin;
            Collidable = collidable;
            CollisionEndsGame = collisionEndsGame;
        }

        /// <summary>
        /// Creates a new BendableLine and adds it to this CompositeLine
        /// </summary>
        /// <param name="location">Location of the BendableLine's origin
        /// to the CompositeLine's origin</param>
        /// <param name="size"></param>
        /// <param name="movement">IMovementController</param>
        /// <param name="followed">True if this BendableLine will be followed by
        /// any other BendableLines</param>
        /// <param name="color">ConsoleColor for this BendableLine</param>
        /// <param name="startTurn">How many turns should this segment wait before generating</param>
        public void AddLine(Coordinates location, int size, IMovementController movement,
            bool followed, ConsoleColor color, int startTurn)
        {
            // FIND START SPACE for BENDABLE LINE
            int x = location.X;
            int y = location.Y;
            Space starting = Origin;
            Direction direction;

            if (y != 0)
            {
                if (y < 0) { direction = Directions.GetDirection(DirectionName.Up); } // GO UP
                else { direction = Directions.GetDirection(DirectionName.Down);} // GO DOWN

                // ITERATE THROUGH THE SPACES
                for (int i = 0; i < Math.Abs(y); i++) { starting = starting.GetNeighbor(direction); }
            }

            if (x != 0)
            {
                if (x < 0) { direction = Directions.GetDirection(DirectionName.Left); } // GO LEFT  
                else { direction = Directions.GetDirection(DirectionName.Right); } // GO RIGHT

                // ITERATE THROUGH THE SPACES
                for (int i = 0; i < Math.Abs(x); i++) { starting = starting.GetNeighbor(direction);}
            }

            BendableLine bendable = new BendableLine(starting, size, color, movement, followed, Collidable, CollisionEndsGame);
            if (followed) { Head = bendable; }
            lines.AddLast(new LinePosition(bendable, location, startTurn));
        }

        /// <summary>
        /// The CompositeLine enters a new space.
        ///
        /// Coordinates the movement for all the BendableLines within the
        /// CompositeLine. Checkis if any of the BendableLines have
        /// triggered a game-ending collision.
        /// </summary>
        public void Move()
        {
            TotallyDead = true;

            foreach (LinePosition lp in lines)
            {
                if (lp.StartTurn <= this.turn)
                {
                    lp.Line.Move();
                    if (!lp.Line.TotallyDead) { TotallyDead = false; }
                }
            }
            this.turn++;
        }

        /// <summary>
        /// A class that pairs a BendableLine with its location
        /// relative to the origin of the CompositeLine.
        /// </summary>
        /// <author>Nicholas Gliserman</author>
        class LinePosition
        {
            public BendableLine Line { get; set; }
            // LOCATION of this BendableLine in the CompositeLine (0, 0)
            public Coordinates Location { get; set; }
            public int StartTurn { get; set; }

            /// <summary>
            /// Constructs a new LinePosition -- which is used in adding a new BendableLine
            /// to the CompositeLine.
            /// </summary>
            /// <param name="line"></param>
            /// <param name="location"></param>
            /// <param name="startTurn"></param>
            public LinePosition(BendableLine line, Coordinates location, int startTurn)
            {
                Line = line;
                Location = location;
                StartTurn = startTurn;
            }
        }
    }
}
