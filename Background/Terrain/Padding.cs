using System;
using SnakeApp.Background;
using SnakeApp.Background.BoxTypes;

namespace SnakeApp.Background.Terrain
{
    /// <summary>
    /// Represents the void around the active area of the game, including
    /// the source and target of the river flow.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Padding : Box
    {
        public Padding(int width, int height) : base(width, height)
        {
            Properties = new BoxProperties(false, ConsoleColor.Black, true);
        }
    }
}
