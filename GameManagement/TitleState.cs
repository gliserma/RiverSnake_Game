using System;
using SnakeApp.Background;

namespace SnakeApp.GameManagement
{
    /// <summary>
    /// Displays the name of the game as well as brief instructions.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class TitleState : IGameState
    {
        public GameManager gameManager;
        public GameArea gameArea;

        /// <summary>
        /// Creates a new title state, the initial state in the game.
        /// </summary>
        /// <param name="gm">GameState</param>
        public TitleState(GameManager gm)
        {
            this.gameManager = gm;
            this.gameArea = gm.GameArea;

            // DISPLAY TITLE and INSTRUCTIONS
            string title = "RIVER SNAKE...Stay in the water and avoid all logs as long as you can!";
            string enterToPlay = "Press Enter to Play";
            Painter.PaintString(ConsoleColor.White, ConsoleColor.DarkRed, gameArea.TitleSpace, title);
            Painter.PaintString(ConsoleColor.White, ConsoleColor.Black, gameArea.InstructionSpace, enterToPlay);

            //LISTEN for ENTER PRESSED EVENT
            ConsoleKey key = ConsoleKey.T;
            do  {if (Console.KeyAvailable) { key = Console.ReadKey(true).Key; } }
            while (key != ConsoleKey.Enter);

            // START GAME STATE
            gameManager.currentGameState = NextState();
        }

        /// <summary>
        /// When the user has pressed enter, the
        /// title leads into the play state.
        /// </summary>
        /// <returns>IGameState</returns>
        public IGameState NextState()
        {
            return new PlayState(gameManager);
        }

    }
}
