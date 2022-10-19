namespace GameOfLife;

public class GameManager
{
    private static GameManager _instance = new GameManager();
    public static GameManager Instance => _instance;
    private GameManager()
    {
        Board = new Board();
        Board.ClearBoard();
    }

    public Board Board { get; set; }
    public GameState GameState = GameState.Stopped;
    public void Start()
    {


        Raylib.InitWindow(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT, Constants.GAME_TITLE);
        Raylib.SetTargetFPS(60);
        Board.LoadTextures();

        // Main game loop
        var done = false;
        while (!done && !Raylib.WindowShouldClose()) // Detect window close button or ESC key
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            var action = UIManager.Instance.TestInput();
            switch (action)
            {
                case GameButtonActions.Exit:
                    done = true;
                    break;

                case GameButtonActions.StopGame:
                    GameState = GameState.Stopped;
                    break;

                case GameButtonActions.StartGame:
                    GameState = GameState.Running;
                    break;

                case GameButtonActions.RandomBoard:
                    Board.Randomize();
                    break;

                case GameButtonActions.ClearBoard:
                    Board.ClearBoard();
                    break;

            }
            Board.TestInput();

            UIManager.Instance.Draw();
            Board.Draw();
            Raylib.EndDrawing();

        }
        Raylib.CloseWindow();
    }

}