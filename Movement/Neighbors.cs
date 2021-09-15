using System;
namespace SnakeApp.Movement
{
    public class Neighbors
    {
        private Neighbor[] neighbors;

        public Neighbors(Neighbor up, Neighbor down, Neighbor left, Neighbor right)
        {
            neighbors = new Neighbor[] {up, down, left, right };
        }

        public Space getNeighbor(DirectionName direction)
        {
            return neighbors[(int)direction].TargetSpace;
        }
    }
}
