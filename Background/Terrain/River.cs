using System;
using SnakeApp.Background;
using SnakeApp.Background.BoxTypes;

namespace SnakeApp.Background.Terrain
{
    /// <summary>
    /// Represents the river where the Snake can move freely (except
    /// in the case of obstacles).
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class River : Box
    {
        public River(int width, int height) : base(width, height)
        {
            Properties = new BoxProperties(true, ConsoleColor.DarkCyan, false);
        }
    }
}
