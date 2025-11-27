using SnakeCalculatoare2025.BusinessLogic;
using SnakeCalculatoare2025.BusinessLogic.Screen;


IScreen screen = new ConsoleScreen();

Game game = new Game(screen);

while (true) {
    ConsoleKey? keyPressed = null;
    if (Console.KeyAvailable) {
        ConsoleKeyInfo key = Console.ReadKey();
        keyPressed = key.Key;
        switch (keyPressed) {
            case ConsoleKey.Escape:
                return;
        }
    }

    game.Update(keyPressed);

    screen.Clear();
    game.Draw();
    screen.Flush();

    Thread.Sleep(100);
}

