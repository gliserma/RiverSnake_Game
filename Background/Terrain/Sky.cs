using System;
using SnakeApp.Background;
using SnakeApp.Background.BoxTypes;

namespace SnakeApp.Background.Terrain
{
    /// <summary>
    /// Represents the space above the distant horizon.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Sky : Box
    {
        public Sky(int width, int height) : base(width, height)
        {
            Properties = new BoxProperties(false, ConsoleColor.White, false);
        }
    }
}
