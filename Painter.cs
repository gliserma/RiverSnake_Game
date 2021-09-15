using System;
using SnakeApp.Movement;

namespace SnakeApp
{
    /// <summary>
    /// Handles the rendering for all classes that have a visual representation.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Painter
    {
        public Painter()
        {
        }

        /// <summary>
        /// Colors an rectangular area on the console
        /// </summary>
        /// <param name="color">ConsoleColor to use</param>
        /// <param name="start">Coordiantes representing
        /// upper-leftmost corner of the rectangle</param>
        /// <param name="width">Integer value representing length along the x-axis</param>
        /// <param name="height">Integer value representing length along the y-axis</param>
        public static void PaintArea(ConsoleColor color, Coordinates start, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(start.X, (start.Y + i));
                Console.BackgroundColor = color;
                string formatString = "{0," + (width) + "}";
                Console.Write(formatString, string.Empty);
                ResetBackgroundColor();
            }

            RestPosition();
        }

        /// <summary>
        /// Colors a rectangular area on the Console based on parameters.
        /// </summary>
        /// <param name="color">ConsoleColor to use</param>
        /// <param name="start">Space representing the
        /// upper-leftmost corner of the rectangle</param>
        /// <param name="width">Integer value representing length
        /// along the x-axis</param>
        /// <param name="height">Integer value representing
        /// length along the y-axis</param>
        public static void PaintArea(ConsoleColor color, Space start, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(start.Location.X, (start.Location.Y + i));
                Console.BackgroundColor = color;
                string formatString = "{0," + (width) + "}";
                Console.Write(formatString, string.Empty);
                ResetBackgroundColor();
            }

            RestPosition();
        }

        /// <summary>
        /// Colors a single space on the console
        /// </summary>
        /// <param name="color">ConsoleColor the space will be</param>
        /// <param name="space">The space to paint</param>
        public static void PaintSpace(ConsoleColor color, Space space)
        {
            PaintSpace(color, color, space, ' ');
        }

        /// <summary>
        /// Colors and draws a char on a single space
        /// </summary>
        /// <param name="backgroundColor">ConsoleColor the space will be</param>
        /// <param name="foregroundColor">ConsoleColor the char will be</param>
        /// <param name="space">The space to paint</param>
        /// <param name="character">The char to be drawn</param>
        public static void PaintSpace(ConsoleColor backgroundColor,
            ConsoleColor foregroundColor, Space space, char character)
        {
            // CHECK IF WE CAN PAINT OVER THE SPACE
            if (!space.Box.Properties.Foreground)
            {
                Console.SetCursorPosition(space.Location.X, space.Location.Y);
                Console.BackgroundColor = backgroundColor;
                Console.ForegroundColor = foregroundColor;
                Console.Write(character);
                ResetBackgroundColor();
                RestPosition();
            }
        }

        /// <summary>
        /// Draws an entire string on the Console, starting in a particular space.
        /// </summary>
        /// <param name="backgroundColor">ConsoleColor the spaces will be</param>
        /// <param name="foregroundColor">ConsoleColor the string will be</param>
        /// <param name="space">Space where the string will start</param>
        /// <param name="text">the string to be printed to the console</param>
        public static void PaintString(ConsoleColor backgroundColor,
            ConsoleColor foregroundColor, Space space, string text)
        {
            Console.SetCursorPosition(space.Location.X, space.Location.Y);
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(text);
            ResetBackgroundColor();
            RestPosition();
        }

        private static void ResetBackgroundColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void RestPosition()
        {
            Console.SetCursorPosition(50, 50);
        }

    }
}
