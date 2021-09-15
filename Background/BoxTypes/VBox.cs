using System;
using System.Collections.Generic;

namespace SnakeApp.Background.BoxTypes
{
    /// <summary>
    /// An aggregation of boxes, aligned vertically; VBox is short for
    /// Vertical Box.
    /// 
    /// A class that both inerhits from Box and is composed of them.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class VBox : Box
    {
        LinkedList<Box> boxes = new LinkedList<Box>();

        /// <summary>
        /// Constructs a new, empty VBox object.
        /// </summary>
        /// <param name="height"></param>
        public VBox(int width) : base(width, 0)
        {
        }

        /// <summary>
        /// Prepends a new Box to this Vertical Box.
        ///
        /// Each box added will appear at the top of the overall
        /// box structure. The box's width will be adjusted to match
        /// the width of the VBox.
        /// </summary>
        /// <param name="box"></param>
        public void addBox(Box box)
        {
            box.Width = base.Width;
            base.Height = base.Height + box.Height;
            boxes.AddFirst(box);
            SetStartPoints(base.Start);
        }

        /// <summary>
        /// For all of the boxes contained in this Vertical Box (VBox),
        /// determines where each one should start
        /// 
        /// </summary>
        /// <param name="start"></param>
        override
        public void SetStartPoints(Coordinates start)
        {
            // SET WHERE THE BASE BOX BEGINS
            base.SetStartPoints(start);

            // VBOX ELEMENTS BEGIN SAME START POINT
            int tempStartX = base.Start.X;
            int tempStartY = base.Start.Y;

            // ITERATE THROUGH EACH BOX and CALCULATE NEW START POINT
            foreach(Box item in this.boxes)
            {
                item.SetStartPoints(new Coordinates(tempStartX, tempStartY));
                tempStartY += item.Height; // ADJUST Y FOR NEXT BOX's STARTPOINT
            }

            base.Height = tempStartY;
        }

        /// <summary>
        /// Given a location, finds the box inside this VBox that contains
        /// that location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>A Box contained inside this VBox</returns>
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
                // Because this is an VBox, we care most about the y coordinate
                if (location.Y >= item.Start.Y
                    && location.Y < (item.Start.Y + item.Height))
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
            foreach (Box item in this.boxes)
            {
                item.Paint();
            }
        }
    }
}
