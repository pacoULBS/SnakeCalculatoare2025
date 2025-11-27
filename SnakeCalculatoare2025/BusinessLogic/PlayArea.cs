using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic
{
    class PlayArea : ICollidable
    {
        ScreenColor color = ScreenColor.Green;
        Position2D topLeft;
        int width = 10;
        int height = 5;

        public PlayArea(
            Position2D position,
            int width,
            int height)
        {
            this.topLeft = position;
            this.width = width;
            this.height = height;
        }

        public bool CollidesWith(Position2D position)
        {
            return position.x <= topLeft.x
                    || position.x >= topLeft.x + width
                    || position.y <= topLeft.y
                    || position.y >= topLeft.y + height;
        }

        public void Draw(IScreen screen)
        {
            for (int x = 0; x < width; x++)
            {
                screen.Write(
                    new Position2D(topLeft.x + x, topLeft.y),
                    "█", color);
                screen.Write(
                    new Position2D(topLeft.x + x, topLeft.y + height),
                    "█", color);
            }
            for (int y = 0; y <= height; y++)
            {
                screen.Write(
                    new Position2D(topLeft.x, topLeft.y + y),
                    "█", color);
                screen.Write(
                    new Position2D(topLeft.x + width, topLeft.y + y),
                    "█", color);
            }
        }

        // Expose minimal read-only properties so Game can position HUD elements inside the border
        public Position2D Position => this.topLeft;
        public int Width => this.width;
        public int Height => this.height;
    }
}