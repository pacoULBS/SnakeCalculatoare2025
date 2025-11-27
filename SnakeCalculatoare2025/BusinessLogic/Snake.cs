using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic
{
    class Snake : ICollidable
    {
        ScreenColor color = ScreenColor.Red;
        SnakeDirection direction;
        Queue<Position2D> positions;
        Position2D headPosition;

        SnakeDirection initialDirection;
        int initialLength;
        Position2D initialPosition;

        public Snake(Position2D initialPosition,
            int initialLength,
            SnakeDirection direction) {
            this.initialDirection = direction;            
            this.initialLength = initialLength;
            this.initialPosition = initialPosition;
            positions = new Queue<Position2D>();
            this.InitializeSnake();
        }

        public void InitializeSnake() {
            this.direction = initialDirection;
            this.positions.Clear();
            this.headPosition = this.initialPosition;
            for (int i = 0; i < initialLength; i++) {
                positions.Enqueue(initialPosition);
            }
        }

        public void Update(SnakeDirection? newDirection) {
            if (newDirection != null) {
                this.direction = newDirection.Value;
            }
            Position2D newPosition = ComputeNextPosition(this.direction);

            this.headPosition = newPosition;
            positions.Enqueue(newPosition);
            positions.Dequeue();
        }

        public Position2D GetHeadPosition() {
            return this.headPosition; 
        }

        public Position2D ComputeNextPosition(SnakeDirection? newDirection) {
            if (newDirection == null) {
                newDirection = this.direction;
            }
            switch (newDirection) {
                case SnakeDirection.Left:
                    return new Position2D(headPosition.x - 1, headPosition.y);
                case SnakeDirection.Right:
                    return new Position2D(headPosition.x + 1, headPosition.y);
                case SnakeDirection.Up:
                    return new Position2D(headPosition.x, headPosition.y - 1);
                case SnakeDirection.Down:
                    return new Position2D(headPosition.x, headPosition.y + 1);
                default:
                    throw new NotImplementedException();
            }
        }        

        public void Draw(IScreen screen) {
            foreach (Position2D position in positions) {
                screen.Write(position, "█", color);
            }
        }

        public bool CollidesWith(Position2D position) {
            foreach (Position2D bodyPosition in positions) {
                if (bodyPosition.Equals(position)) {
                    return true;
                }
            }
            return false;
        }
    }
}
