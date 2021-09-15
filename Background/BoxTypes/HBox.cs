using System;
using System.Collections.Generic;

namespace SnakeApp.Background.BoxTypes
{
    /// <summary>
    /// An aggregation of boxes, aligned horizontally; HBox is short for
    /// Horizontal Box.
    /// 
    /// A class that both inerhits from Box and is composed of them.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class HBox : Box
    {
        private LinkedList<Box> boxes = new LinkedList<Box>();

        /// <summary>
        /// Constructs a new, empty HBox object.
        /// </summary>
        /// <param name="height"></param>
        public HBox(int height) : base(0, height)
        {
        }

        /// <summary>
        /// Prepends a new Box to this Horizontal Box.
        ///
        /// Each new box added will appear at the leftmost part of the
        /// overall box structure. The box's height will be adjusted to
        /// match the height of the HBox.
        /// </summary>
        /// <param name="box"></param>
        public void addBox(Box box)
        {
            box.Height = base.Height;
            base.Width = base.Width + box.Width;
            boxes.AddFirst(box);
            SetStartPoints(base.Start);
        }

        /// <summary>
        /// For all of the boxes contained in this Horizontal Box (HBox),
        /// determines where each one should start.
        /// </summary>
        /// <param name="start"></param>
        override
        public void SetStartPoints(Coordinates start)
        {
            // SET WHERE THE BASE BOX BEGINS
            base.SetStartPoints(start);

            // VBOX ELEMENTS BEGIN SAME START POINT
            int tempStartX = start.X;
            int tempStartY = start.Y;

            // ITERATE THROUGH EACH BOX and CALCULATE NEW START POINT
            foreach(Box item in this.boxes)
            {
                item.SetStartPoints(new Coordinates(tempStartX, tempStartY));
                tempStartX = tempStartX + item.Width; // ADJUST X FOR NEXT BOX's STARTPOINT
            }
            base.Width = tempStartX;
        }

        /// <summary>
        /// Given a location, finds the box inside this HBox that contains
        /// that location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        override
        public Box GetInnerBox(Coordinates location)
        {
            // IS LOCATION NOT IN THIS BOX?
            if (location.X < base.Start.X
                || location.X > (base.Start.X + base.Width)
                || location.Y < base.Start.Y
                || location.Y > (base.Start.Y + base.Height))
            {
                throw new ArgumentOutOfRangeException
                    ("Location is outside of this box");
            }

            // ITERATE THROUGH INTERIOR BOXES for MATCH
            foreach (Box item in this.boxes)
            {
                // Because this is an HBox, we care about the x coordinate
                if (location.X >= item.Start.X
                    && location.X < (item.Start.X + item.Width))
                {
                    return item.GetInnerBox(location);
                }
            }

            return this; // CASE WERE THIS BOX IS EMPTY
        }

        /// <summary>
        ///Draws each of the the interior Boxes on the Console
        /// </summary>
        override
        public void Paint()
        {
            foreach (Box item in this.boxes) {item.Paint(); }
        }
    }
}
