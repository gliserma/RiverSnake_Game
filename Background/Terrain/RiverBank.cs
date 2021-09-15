using System;
using SnakeApp.Background;
using SnakeApp.Background.BoxTypes;

namespace SnakeApp.Background.Terrain
{
    /// <summary>
    /// Represents the ground along the top and bottom of the river
    /// that contain the Snake's area of movement.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class RiverBank : Box
    {
        public RiverBank(int width, int height) : base(width, height)
        {
            Properties = new BoxProperties(false, ConsoleColor.DarkGreen, false);
        }
    }
}
