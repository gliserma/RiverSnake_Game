using System;
using System.Collections.Generic;
using System.Threading;

using SnakeApp.Background;
using SnakeApp.GameObjects;
using SnakeApp.Movement;
using SnakeApp.Movement.MovementController;

namespace SnakeApp.GameManagement
{
    /// <summary>
    /// Displays the active game and allows the player to input controls.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class PlayState : IGameState
    {
        public static bool GameOver { get; set; }
        public Direction Direction { get; set; }
        GameManager gm;
        Random rand = new Random(); // GENERATES ALL RANDOM VALUES in CLASS

        /// <summary>
        /// Starts a new game
        /// </summary>
        /// <param name="gm">GameManager</param>
        public PlayState(GameManager gm)
        {
            // INITIALIZE VARIABLES
            this.gm = gm;
            GameOver = false;
           
            int frame = 0; // COUNT for TIME SCORE
            int framesPerSec = 150; // NEEDED for CALCULATING TIME
            int frameSpeed = 1000 / framesPerSec; // HOW FAST IS THE GAME

            // METRICS for ELEMENTS in the SCENE
            int dodgedLogs = 0; // OBSTACLES AVOIDED -- SCORE METRIC
            int nextLogSpawn = 40; // SPAWN FIRST LOG
            int nextTreeSpawn = 20; // SPAWN FIRST TREE (BACKGROUND SCENERY)

            // LIST to HOLD OBSTACLES in RIVER
            LinkedList<Log> logs = new LinkedList<Log>(); 
            LinkedList<Log> removeLog;

            // LIST to HOLD BACKGROUND SCENERY
            LinkedList<Tree> trees = new LinkedList<Tree>();
            LinkedList<Tree> removeTree;

            // PAINT OVER TITLE / SCORE SCREEN
            Painter.PaintArea(ConsoleColor.White,
                this.gm.GameArea.TitleSpace, 100, 1);
            Painter.PaintArea(ConsoleColor.White,
                this.gm.GameArea.InstructionSpace, 100, 1);

            // CREATE A PLAYER-CONTROLLED SNAKE
            IMovementController player = new PlayerController(this);
            Snake snake = new GameObjects.Snake(this.gm.GameArea.StartSpace, player);
            int snakeFrames = 3;

            // SET INITIAL DIRECTION
            Direction = Directions.GetDirection(DirectionName.Right);
            ConsoleKey key = ConsoleKey.RightArrow;

            // GAME LOOP
            do
            {
                long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                // IF THERE IS USER INPUT && SNAKE MOVES in CURRENT FRAME
                if (Console.KeyAvailable && frame % snakeFrames == 0) 
                {
                    key = Console.ReadKey(true).Key;

                    // READ DIRECTION INPUT from USER
                    // n.b. OPPOSITE DIRECTION NOT ALLOWED (WOULD CAUSE IMMEDIATE GAME OVER)
                    if (key == ConsoleKey.UpArrow && Direction.Name != DirectionName.Down)
                        Direction = Directions.GetDirection(DirectionName.Up);
                    else if (key == ConsoleKey.DownArrow && Direction.Name != DirectionName.Up)
                        Direction = Directions.GetDirection(DirectionName.Down);
                    else if (key == ConsoleKey.LeftArrow && Direction.Name != DirectionName.Right)
                        Direction = Directions.GetDirection(DirectionName.Left);
                    else if (key == ConsoleKey.RightArrow && Direction.Name != DirectionName.Left)
                        Direction = Directions.GetDirection(DirectionName.Right);
                }

                // DETERMINE WHICH OBJECTS MOVE in a GIVEN FRAME
                // LOGS
                if (frame % 2 == 0)
                {
                    nextLogSpawn--;
                    removeLog = new LinkedList<Log>();
                    // MOVE or REMOVE
                    foreach (Log l in logs)
                    {
                        if (l.TotallyDead) { removeLog.AddFirst(l);}
                        else { l.Move(); }
                    }
                    // GARBAGE COLLECTION
                    foreach (Log l in removeLog) 
                    {
                        logs.Remove(l);
                        dodgedLogs++;
                    }
                    // SPAWN NEW OBSTACLE?
                    if (nextLogSpawn == 0)
                    {
                        logs.AddLast(GenerateLog());
                        nextLogSpawn = rand.Next(20, 150);
                    }
                }

                // BACKGROUND SCENERY
                if (frame % 4 == 0)
                {
                    nextTreeSpawn--;
                    removeTree = new LinkedList<Tree>();
                    foreach (Tree t in trees)
                    {
                        if (t.TotallyDead)
                        {
                            removeTree.AddFirst(t);
                        }
                        else { t.Move(); }
                    }
                    foreach (Tree t in removeTree) // GARBAGE COLLECTION
                    {
                        trees.Remove(t);
                    }
                    if (nextTreeSpawn == 0)
                    {
                        trees.AddLast(GenerateTree());
                        nextTreeSpawn = rand.Next(50, 200);
                    }
                }

                // PLAYER-CONTROLLED SNAKE
                if (frame % snakeFrames == 0) { snake.Move(); }

                // TIME: ENSURE FRAMES ARE EVENTLY TIMED
                int frameLength = (int) (DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime);
                if (frameLength < frameSpeed)
                {
                    Thread.Sleep(frameSpeed - frameLength);
                }
                
                frame++; // INCREMENT FRAME
            }
            while (!GameOver);

            Console.Beep(); // BEEP WHEN PLAYER HITS AN OBSTACLE
            
            // SCORE METRICS: SAVE to GAME MANAGER
            int totalSeconds = (int) Math.Round((double) frame / framesPerSec);
            this.gm.CurrentGameTime = totalSeconds;
            this.gm.DodgedObstacles = dodgedLogs;

            // BRIEF PAUSE for PLAYER to SEE CAUSE of DEATH
            Thread.Sleep(1000);

            // GO TO SCORE STATE
            this.gm.currentGameState = NextState();
        }

        /// <summary>
        /// Game State leads into Score State
        /// </summary>
        /// <returns>ScoreState</returns>
        public IGameState NextState()
        {
            this.gm.GameArea = new GameArea();
            return new ScoreState(gm);
        }

        /// <summary>
        /// Creates a new obstacle that the player must avoid.
        /// </summary>
        /// <returns>Log</returns>
        public Log GenerateLog()
        {
            Space randomSpace = gm.GameArea.LogGenerator[
                    rand.Next(0, gm.GameArea.LogGenerator.Length)];
            return new Log(randomSpace, rand.Next(4, 8));
        }

        /// <summary>
        /// Creates a new tree in the background that provides
        /// a sense of depth & movement.
        /// </summary>
        /// <returns>Tree</returns>
        public Tree GenerateTree()
        {
            Space randomSpace = gm.GameArea.TreeGenerator[rand.Next(0, 1)];
            return new Tree(randomSpace);
        }
    }
}
