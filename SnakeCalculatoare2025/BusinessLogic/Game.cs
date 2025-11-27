using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic
{
    class Game
    {
        int score;
        Snake snake;
        PlayArea playArea;
        Reward reward;
        IScreen screen;

        public Game(IScreen screen) {
            this.screen = screen;
            this.playArea = new PlayArea(
                new Position2D(1, 3),
                width: 30,
                height: 15
                );
            this.snake = new Snake(
                initialPosition: new Position2D(10, 10),
                initialLength: 10,
                direction: SnakeDirection.Right
                );
            this.reward = new Reward(
                new Position2D(12, 10),
                points: 3
                );            
        }

        public void Update(ConsoleKey? key) {
            SnakeDirection? newDirection = null;
            if (key != null) {
                switch (key) {
                    case ConsoleKey.LeftArrow:
                        newDirection = SnakeDirection.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        newDirection = SnakeDirection.Right;
                        break;
                    case ConsoleKey.UpArrow:
                        newDirection = SnakeDirection.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        newDirection = SnakeDirection.Down;
                        break;
                }
            }

            Position2D nextPosition = snake.ComputeNextPosition(newDirection);

            if (playArea.CollidesWith(nextPosition)
                || snake.CollidesWith(nextPosition)
                ) {
                snake.InitializeSnake();
            } else {
                snake.Update(newDirection);
            }
        }

        public void Draw() {
            playArea.Draw(screen);
            reward.Draw(screen);
            snake.Draw(screen);            
        }
    }
}
