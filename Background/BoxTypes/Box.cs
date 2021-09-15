using System;
namespace SnakeApp.Background.BoxTypes
{
    /// <summary>
    /// Represents a stationary area of the console defined by height, width,
    /// color, and starting point.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Box
    {
        public Coordinates Start { get; set; } = new Coordinates(0, 0);
        public int Width { get; set; }
        public int Height { get; set; }
        public BoxProperties Properties { get; set; }

        /// <summary>
        /// Constructs a new Box based on width and height.
        ///
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Box(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Sets the UpperLeft-most Location of the Box.
        /// 
        /// A setter method that can be overriden by derived classes.
        /// </summary>
        /// <param name="startCoords">UpperLeft-most Location of the Box</param>
        public virtual void SetStartPoints(Coordinates startCoords)
        {
            Start = startCoords;
        }

        /// <summary>
        /// If there is a box inside of this box at the given location,
        /// this method will find and return it.
        ///
        /// This method was created for classes that extend box and are composed of
        /// other boxes. This version of the method in the base class, which
        /// returns itself, is the way to end the recursion.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual Box GetInnerBox(Coordinates location)
        {
            // IS THIS LOCATION INSIDE THIS BOX?
            if (location.X < Start.X || location.X > (Start.X + Width)
                || location.Y < Start.Y || location.Y > (Start.Y + Height))
            {
                throw new ArgumentOutOfRangeException("Location is outside of this box");
            }

            // RETURN THIS INSTANCE of BOX
            return this;
        }

        /// <summary>
        /// Draws the Box in the Console
        /// </summary>
        public virtual void Paint()
        {
            if (this.Properties != null)
            {
                Painter.PaintArea(this.Properties.Color, this.Start, this.Width, this.Height);
            }
        }
    }
}
