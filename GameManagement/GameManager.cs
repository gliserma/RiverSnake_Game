using System;
using System.Threading;

using SnakeApp.Background;
using SnakeApp.GameManagement;


namespace SnakeApp
{
    /// <summary>
    /// Maintains the game's area, current state, and player metrics.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class GameManager
    {
        public GameArea GameArea { get; set;}
        public IGameState currentGameState;
        public int CurrentGameTime { get; set; }
        public int DodgedObstacles { get; set; }

        /// <summary>
        /// Launches the application, creates the game area,
        /// and starts the game in its title state.
        /// </summary>
        public GameManager()
        {
            GameArea = new GameArea();
            currentGameState = new TitleState(this);
        }
    }
}
