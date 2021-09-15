using System;
namespace SnakeApp.GameManagement
{
    /// <summary>
    /// Defines how one game state should provide the
    /// next game state.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public interface IGameState
    {
        /// <summary>
        /// When one game state has completed, it should
        /// return the next game state.
        /// </summary>
        /// <returns></returns>
        public IGameState NextState();
    }
}
