using System;
using SnakeApp.Background;

namespace SnakeApp.GameManagement
{
    /// <summary>
    /// Displays the score for the player, namely
    /// how many seconds they lasted and how many
    /// obstacles they avoided.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class ScoreState : IGameState
    {
        public GameManager gameManager;
        public GameArea gameArea;

        /// <summary>
        /// Creates a new Score State object
        /// </summary>
        /// <param name="gm">Game Manager</param>
        public ScoreState(GameManager gm)
        {
            this.gameManager = gm;
            this.gameArea = gm.GameArea;

            // DISPLAY SCORE & PLAY AGAIN MESSAGE
            string scoreMessage =
                $"You survived for {gm.CurrentGameTime} seconds " +
                $"and dodged {gm.DodgedObstacles} logs!)";
            string enterToPlay = "Press Enter to Play Again";
            Painter.PaintString(ConsoleColor.White, ConsoleColor.DarkRed,
                gameArea.TitleSpace, scoreMessage);
            Painter.PaintString(ConsoleColor.White, ConsoleColor.Black,
                gameArea.InstructionSpace, enterToPlay);

            // LISTEN for ENTER PRESSED EVENT
            ConsoleKey key = ConsoleKey.T;
            do
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }
            }
            while (key != ConsoleKey.Enter);

            // LEADS INTO GAME STATE
            gameManager.currentGameState = NextState();
        }

        /// <summary>
        /// When the user has pressed enter, the
        /// score screen leads back into the play state.
        /// </summary>
        /// <returns>IGameState</returns>
        public IGameState NextState()
        {
            return new PlayState(gameManager);
        }
    }
}
