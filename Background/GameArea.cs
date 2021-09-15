
using SnakeApp.Background.Terrain;
using SnakeApp.Background.BoxTypes;
using SnakeApp.Movement;

namespace SnakeApp.Background
{
    /// <summary>
    /// Builds a scene using terrain and padding elements.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class GameArea
    {
        public Space StartSpace { get; } // TEMP ONLY
        public Space TitleSpace { get; }
        public Space InstructionSpace { get; }
        public Space[] TreeGenerator { get; }
        public Space[] LogGenerator { get; }
        private Space[,] spaces;

        /// <summary>
        /// Constructs a new GameArea.
        /// </summary>
        public GameArea()
        {
            // CREATE the CONSTITUENT ELEMENTS of the SCENE
            Box topPadding = new Padding(1, 5);
            Box sky = new Sky(1, 15);
            Box topGrass = new RiverBank(1, 2);
            Box water = new River(1, 20);
            Box bottomGrass = new RiverBank(1, 5);
            Box bottomPadding = new Padding(1, 5);

            // STACK the ELEMENTS of the SCENE VERTICALLY
            VBox interiorGameArea = new VBox(150);
            interiorGameArea.addBox(bottomPadding);
            interiorGameArea.addBox(bottomGrass);
            interiorGameArea.addBox(water);
            interiorGameArea.addBox(topGrass);
            interiorGameArea.addBox(sky);
            interiorGameArea.addBox(topPadding);

            // PAD the VBOX
            Box leftPadding = new Padding(5, 1);
            Box rightPadding = new Padding(5, 1);

            // STACK the HORIZONTAL ELEMENTS
            HBox totalGameArea = new HBox(interiorGameArea.Height);
            totalGameArea.addBox(rightPadding);
            totalGameArea.addBox(interiorGameArea);
            totalGameArea.addBox(leftPadding);

            // DISPLAY the GAME AREA on the CONSOLE
            totalGameArea.Paint();

            // CREATE A 2D ARRAY OF SPACES
            this.spaces = new Space[totalGameArea.Height,totalGameArea.Width];

            // POPULATE THE 2D ARRAY of SPACES
            for (int i = 0; i < totalGameArea.Height; i++) // y
            {
                for (int j = 0; j < totalGameArea.Width; j++) // x
                {
                    Coordinates current = new Coordinates(totalGameArea.Start.X + j, totalGameArea.Start.Y + i);
                    this.spaces[i, j] = new Space(current, totalGameArea.GetInnerBox(current));
                }
            }

            // CREATE NEIGHBORS for the SPACES
            for (int i = 0; i < totalGameArea.Height; i++) // y
            {
                for (int j = 0; j < totalGameArea.Width; j++) // x
                {
                    Space currentSpace = this.spaces[i, j];

                    // UP
                    Neighbor upNeighbor;
                    if (i == 0)
                        upNeighbor = new Neighbor(null, Directions.GetDirection(DirectionName.Up));
                    else
                        upNeighbor = new Neighbor(this.spaces[(i - 1), j], Directions.GetDirection(DirectionName.Up));

                    // DOWN
                    Neighbor downNeighbor;
                    if (i == totalGameArea.Height - 1)
                        downNeighbor = new Neighbor(null, Directions.GetDirection(DirectionName.Down));
                    else
                        downNeighbor = new Neighbor(this.spaces[(i + 1), j], Directions.GetDirection(DirectionName.Down));

                    // LEFT
                    Neighbor leftNeighbor;
                    if (j == 0)
                        leftNeighbor = new Neighbor(null, Directions.GetDirection(DirectionName.Left));
                    else
                        leftNeighbor = new Neighbor(this.spaces[i, (j - 1)], Directions.GetDirection(DirectionName.Left));

                    // RIGHT
                    Neighbor rightNeighbor;
                    if (j == totalGameArea.Width - 1)
                        rightNeighbor = new Neighbor(null, Directions.GetDirection(DirectionName.Right));
                    else
                        rightNeighbor = new Neighbor(this.spaces[i, (j + 1)], Directions.GetDirection(DirectionName.Right));

                    // ASSIGN the NEIGHBORS
                    Neighbors currentNeighbors = new Neighbors(upNeighbor, downNeighbor, leftNeighbor, rightNeighbor);
                    currentSpace.Neighbors = currentNeighbors;
                }
            }

            // SPECIAL SPACES for GENERATING ELEMENTS
            this.StartSpace = spaces[32, 4]; // WHERE THE PLAYER STARTS
            this.TitleSpace = spaces[15, 50];
            this.InstructionSpace = spaces[18, 55];

            int xAxis = 155; // RIGHT EDGE of VISIBLE AREA

            // NON-PLAYER MOVING ELEMENTS
            this.LogGenerator = new Space[] { spaces[27, xAxis], spaces[28, xAxis],
                spaces[28, xAxis], spaces[29, xAxis], spaces[30, xAxis], spaces[31, xAxis],
                spaces[32, xAxis], spaces[33, xAxis], spaces[34, xAxis], spaces[35, xAxis]};

            this.TreeGenerator = new Space[] { spaces[15, xAxis], spaces[16, xAxis], };
        }
    }
}
