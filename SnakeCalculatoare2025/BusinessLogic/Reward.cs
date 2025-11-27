using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic
{
    class Reward : ICollidable
    {
        ScreenColor color = ScreenColor.Blue;
        Position2D position;
        int points;
        public Reward(Position2D position, int points) {
            this.position = position;
            this.points = points;
        }

        public void Draw(IScreen screen) {
            screen.Write(position, "x", color);
        }

        public bool CollidesWith(Position2D position) {
            return this.position.Equals(position);
        }
    }
}
