using System;
namespace SnakeApp.Background.BoxTypes
{
    /// <summary>
    /// Manages the properties for spaces within the rectangle
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class BoxProperties
    {
        public bool EmptyByDefault { get; } // Can the Player ever enter the Space
        public ConsoleColor Color { get; } // Color for the spaces in the rectangle
        public bool Foreground { get; } // Can this box be painted over?
       
        /// <summary>
        /// Constructs 
        /// </summary>
        /// <param name="emptyByDefault">Can the player ever enter the space</param>
        /// <param name="color"></param>
        public BoxProperties(bool emptyByDefault, ConsoleColor color, bool foreground)
        {
            this.EmptyByDefault = emptyByDefault;
            this.Color = color;
            this.Foreground = foreground;
        }
    }
}
