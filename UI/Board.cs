namespace GameOfLife;


public class Board
{
    private Texture _texture;
    public void LoadTextures()
    {
        var image = Raylib.LoadImage("images/blank.png");
        _texture = Raylib.LoadTextureFromImage(image);
    }



    private int[,] boardStates;
    private DateTime _lastUpdate = DateTime.Now;
    public Board()
    {

    }

    public void ClearBoard()
    {
        // +2 for easier calculations
        boardStates = GetInitializedBoard();
    }

    public void Update()
    {
        if (GameManager.Instance.GameState == GameState.Stopped) return;
    }

    public void Draw()
    {
        var pos = Raylib.GetMousePosition();

        if (GameManager.Instance.GameState == GameState.Running && DateTime.Now.Subtract(_lastUpdate).TotalSeconds > 0.5)
        {
            _lastUpdate = DateTime.Now;
            NextGeneration();
        }

        var X = Constants.BOARD_OFFSET;

        for (int x = 1; x <= Constants.NUM_X_TILES; x++)
        {
            var Y = Constants.UI_OFFSET;
            for (int y = 1; y <= Constants.NUM_Y_TILES; y++)
            {
                if (boardStates[x, y] == 0)
                    Raylib.DrawTexture(_texture, X, Y, Raylib.WHITE);

                if (GameManager.Instance.GameState == GameState.Stopped && TestCollision(pos, X, Y))
                {
                    Raylib.DrawRectangleLines(X, Y, Constants.SQUARE_SIZE, Constants.SQUARE_SIZE, Raylib.YELLOW);
                }

                Y += Constants.SQUARE_SIZE;
            }

            X += Constants.SQUARE_SIZE;
        }
    }

    private bool TestCollision(Vector2 pos, int x, int y)
    {
        return Raylib.CheckCollisionPointRec(pos, new Rectangle(x, y, Constants.SQUARE_SIZE, Constants.SQUARE_SIZE));
    }

    public void Randomize()
    {
        ClearBoard();
        var rnd = new Random();
        var amount = Constants.NUM_X_TILES * Constants.NUM_Y_TILES / rnd.Next(2, 5);
        for (int i = 0; i < amount; i++)
        {
            var x = rnd.Next(1, Constants.NUM_X_TILES);
            var y = rnd.Next(1, Constants.NUM_Y_TILES);
            boardStates[x, y] = 1;
        }
    }

    public void TestInput()
    {
        var pos = Raylib.GetMousePosition();
        if (GameManager.Instance.GameState == GameState.Running || !Raylib.IsMouseButtonReleased(Raylib.MOUSE_LEFT_BUTTON)) return;

        var X = Constants.BOARD_OFFSET;

        for (int x = 1; x <= Constants.NUM_X_TILES; x++)
        {
            var Y = Constants.UI_OFFSET;
            for (int y = 1; y <= Constants.NUM_Y_TILES; y++)
            {

                if (TestCollision(pos, X, Y))
                {
                    boardStates[x, y] = boardStates[x, y] == 0 ? 1 : 0;
                    return;
                }

                Y += Constants.SQUARE_SIZE;
            }

            X += Constants.SQUARE_SIZE;
        }

    }

    private int[,] GetInitializedBoard()
    {
        var b = new int[Constants.NUM_X_TILES + 2, Constants.NUM_Y_TILES + 2];
        for (int i = 0; i < Constants.NUM_X_TILES + 2; i++)
            for (int k = 0; k < Constants.NUM_Y_TILES + 2; k++)
                b[i, k] = 0;

        return b;
    }
    private void NextGeneration()
    {
        var newGeneration = GetInitializedBoard();

        for (int x = 1; x < Constants.NUM_X_TILES; x++)
            for (int y = 1; y < Constants.NUM_Y_TILES; y++)
            {
                var neighborCount = GetNeighborCount(x, y);
                if (boardStates[x, y] == 0 && neighborCount == 3)
                    newGeneration[x, y] = 1;
                else if (boardStates[x, y] == 1 && (neighborCount < 2 || neighborCount > 3))
                    newGeneration[x, y] = 0;
                else
                    newGeneration[x, y] = boardStates[x, y];
            }

        boardStates = newGeneration;
    }

    private int GetNeighborCount(int x, int y)
    {
        return boardStates[x - 1, y - 1] + boardStates[x, y - 1] + boardStates[x + 1, y - 1] +
        boardStates[x - 1, y] + boardStates[x + 1, y] +
        boardStates[x - 1, y + 1] + boardStates[x, y + 1] + boardStates[x + 1, y + 1];
    }
}