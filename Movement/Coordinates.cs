using System;
namespace SnakeApp
{
    /// <summary>
    /// Stores an x, y point as a location on the console.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public struct Coordinates
    {
        public int X { get; }
        public int Y { get; }

        /// <summary>
        /// Constructor takes two integer values representing
        /// positions along the x- and y- axes.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
