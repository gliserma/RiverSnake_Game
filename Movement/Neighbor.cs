
namespace SnakeApp.Movement
{
    /// <summary>
    /// Space adjacent to the the origin space in a specified direction.
    /// </summary>
    /// <author>Nicholas Gliserman</author>
    public class Neighbor
    {
        public Space TargetSpace { get; }
        private Direction direction;

        /// <summary>
        /// Constructs the neighbor object
        /// </summary>
        /// <param name="space">The neighboring target space</param>
        /// <param name="direction">
        /// the direction to reach the target space from the origin
        /// </param>
        public Neighbor(Space space, Direction direction)
        {
            this.TargetSpace = space;
            this.direction = direction;
        }
    }
}
