using System;
using SnakeApp.Movement;

namespace SnakeApp.GameObjects.Structures
{
    /// <summary>
    /// Array-based Implementation of a Queue data structure.
    /// </summary>
    public class CircularQueue
    {
        private int front; // Array location for next element to be removed.
        private int back; // Array location for next element to be added.
        private int n; // Number of items in the Queue
        private int capacity; // Maximum number of items allowed in queue
        private Space[] segments;

        /// <summary>
        /// Constructs a new Circular Queue with the capacity specified by the parameter
        /// </summary>
        /// <param name="capacity"></param>
        public CircularQueue(int capacity)
        {
            // INITIALIZE METADATA
            this.capacity = capacity;
            this.front = -1;
            this.back = -1;
            this.n = 0;

            // INITIALIZE the ARRAY
            this.segments = new Space[this.capacity];
        }

        /// <summary>
        /// Returns the number of items in the queue.
        /// </summary>
        /// <returns></returns>
        public int Size() {return this.n;}

        /// <summary>
        /// Reveals if the queue currently has any elements.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() {return (n == 0); }

        /// <summary>
        /// Reveals if the queue is filled to capacity.
        /// </summary>
        /// <returns></returns>
        public bool IsFull() {return (n == capacity); }

        /// <summary>
        /// Adds a new element to the queue
        /// </summary>
        /// <param name="element">string to add to the queue</param>
        public void Enqueue(Space element)
        {
            // CORNER CASES
            if (IsEmpty()) {this.front = 0;}
            else if (this.n == capacity) {throw new NotSupportedException("Cannot add to a full queue."); }

            this.back = ++this.back % this.capacity; // SHIFT TAIL INDICE

            this.n++; // INCREMENT QUEUE SIZE
            segments[this.back] = element; // PUT SPACE IN ARRAY
        }

        /// <summary>
        /// Removes an element from the queue
        /// </summary>
        /// <returns>the string added to the queue before the other elements</returns>
        public Space Dequeue()
        {
            // IS THERE AN ITEM TO REMOVE?
            if (IsEmpty()) { throw new NotSupportedException("Cannot remove from an empty queue"); }

            // REMOVE ELEMENT
            Space element = segments[front]; 
            segments[this.front] = null;

            this.n--; // DECREMENT QUEUE SIZE

            // SHIFT HEAD INDICE
            if (IsEmpty())
            {
                this.back = -1;
                this.front = -1;
            }
            else {this.front = ++this.front % capacity; }

            return element;
        }

        /// <summary>
        /// Shows the space at the front without removing it
        /// </summary>
        /// <returns></returns>
        public Space PeekAtFront() {return segments[front]; }

        /// <summary>
        /// Shows the space at the back without removint it
        /// </summary>
        /// <returns></returns>
        public Space PeekAtBack() { return segments[back]; }
    }
}
