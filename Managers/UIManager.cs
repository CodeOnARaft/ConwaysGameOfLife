namespace GameOfLife;


public class UIManager
{
    private static UIManager _instance = new UIManager();
    public static UIManager Instance => _instance;
    private UIManager()
    {
        AddButtons();
    }

    private List<GameButton> _RunningButtons = new List<GameButton>();
    private List<GameButton> _StoppedButtons = new List<GameButton>();

    private void AddButtons()
    {
        var button = new GameButton(new Rectangle(Constants.BOARD_OFFSET, Constants.BOARD_OFFSET, 100
        , Constants.BUTTON_STANDARD_HEIGHT), "Start", GameButtonActions.StartGame);
        _StoppedButtons.Add(button);

        button = new GameButton(new Rectangle(100 + Constants.BOARD_OFFSET * 2, Constants.BOARD_OFFSET, 100
    , Constants.BUTTON_STANDARD_HEIGHT), "Clear", GameButtonActions.ClearBoard);
        _StoppedButtons.Add(button);

        button = new GameButton(new Rectangle(200 + Constants.BOARD_OFFSET * 3, Constants.BOARD_OFFSET, 100
     , Constants.BUTTON_STANDARD_HEIGHT), "Random", GameButtonActions.RandomBoard);
        _StoppedButtons.Add(button);

        button = new GameButton(new Rectangle(300 + Constants.BOARD_OFFSET * 4, Constants.BOARD_OFFSET, 100
     , Constants.BUTTON_STANDARD_HEIGHT), "Exit", GameButtonActions.Exit);
        _StoppedButtons.Add(button);

        button = new GameButton(new Rectangle(Constants.BOARD_OFFSET, Constants.BOARD_OFFSET, 100
     , Constants.BUTTON_STANDARD_HEIGHT), "Stop", GameButtonActions.StopGame);
        _RunningButtons.Add(button);
    }

    public void Draw()
    {
        var pos = Raylib.GetMousePosition();
        if (GameManager.Instance.GameState == GameState.Stopped)
        {
            _StoppedButtons.ForEach(x =>
            {
                x.TestHover(pos);
                x.Draw();
            });
        }
        else if (GameManager.Instance.GameState == GameState.Running)
        {
            _RunningButtons.ForEach(x =>
            {
                x.TestHover(pos);
                x.Draw();
            });
        }
    }

    public GameButtonActions TestInput()
    {
        if(!Raylib.IsMouseButtonReleased(Raylib.MOUSE_LEFT_BUTTON)) return GameButtonActions.Nothing;

        var pos = Raylib.GetMousePosition();
        if (GameManager.Instance.GameState == GameState.Stopped)
        {
            foreach (var button in _StoppedButtons)
            {
                if (button.TestCollision(pos))
                    return button.Action;
            }
        }
        else if (GameManager.Instance.GameState == GameState.Running)
        {
            foreach (var button in _RunningButtons)
            {
                if (button.TestCollision(pos))
                    return button.Action;
            }
        }
        return GameButtonActions.Nothing;
    }

}