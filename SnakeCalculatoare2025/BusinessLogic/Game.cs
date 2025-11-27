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

        private const int MAX_LIVES = 4;
        private int lives = MAX_LIVES;
        public int Lives => lives;
        private bool isGameOver = false;

        public Game(IScreen screen)
        {
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

        public void LoseLife()
        {
            if (lives <= 0) {
                return;
            }
            lives--;
        }

        public void ResetLives()
        {
            lives = MAX_LIVES;
            isGameOver = false;
        }

        public void Update(ConsoleKey? key){
            if (isGameOver) return;

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
                LoseLife();
                if (Lives > 0){
                    snake.InitializeSnake();
                }else{
                    isGameOver = true;
                }
            }else
            {
                snake.Update(newDirection);
            }
        }

        public void Draw() {
            playArea.Draw(screen);
            reward.Draw(screen);
            snake.Draw(screen);

            try
            {
                string filled = "█";
                string lost = "X";
                string label = "Lives:";
                int spacing = 1;
                int paddingFromLeft = 2;

                Position2D areaPos = playArea.Position;
                int areaWidth = playArea.Width;
                int areaHeight = playArea.Height;

                int y = areaPos.y;

                int startX = areaPos.x + paddingFromLeft;

                int interiorWidth = areaWidth - 1 - paddingFromLeft;

                if (label.Length >= interiorWidth)
                {
                    // not enough room: cancel drawing
                    return;
                }

                screen.Write(new Position2D(startX, y), label, ScreenColor.White, ScreenColor.Black);

                int marksStartX = startX + label.Length + 1;

                for (int i = 0; i < MAX_LIVES; i++)
                {
                    int x = marksStartX + i * (1 + spacing);

                    if (i < Lives)
                    {
                        screen.Write(new Position2D(x, y), filled, ScreenColor.Red, ScreenColor.Black);
                    }
                    else
                    {
                        screen.Write(new Position2D(x, y), lost, ScreenColor.Red, ScreenColor.Black);
                    }
                }

                if (isGameOver)
                {
                    string msg = "GAME OVER";

                    int innerLeft = areaPos.x + 1;
                    int innerTop = areaPos.y + 1;
                    int innerWidth = Math.Max(1, areaWidth - 1);
                    int innerHeight = Math.Max(1, areaHeight - 1);

                    int x = innerLeft + Math.Max(0, (innerWidth - msg.Length) / 2);
                    int yMsg = innerTop + innerHeight / 2;

                    if (x < innerLeft) x = innerLeft;
                    if (yMsg < innerTop) yMsg = innerTop;

                    screen.Write(new Position2D(x, yMsg), msg, ScreenColor.White, ScreenColor.Black);
                }
            }
            catch
            {
                // if anything fails, cancel drawing 
            }
        }
    }
}