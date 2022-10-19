namespace GameOfLife;
public class Constants
{
    public const string GAME_TITLE = "Conway's Game of Life";
    public const int SQUARE_SIZE = 32;
    public const int BOARD_OFFSET = 10;
    public const int NUM_X_TILES = 30;
    public const int NUM_Y_TILES = 20;
    public const int BUTTON_STANDARD_HEIGHT = 50;

    public const int SCREEN_WIDTH = (2 * BOARD_OFFSET) + (NUM_X_TILES * SQUARE_SIZE);
    public const int SCREEN_HEIGHT = (3 * BOARD_OFFSET) + BUTTON_STANDARD_HEIGHT + (NUM_Y_TILES * SQUARE_SIZE);

    public const int UI_OFFSET =  (2 * BOARD_OFFSET) + BUTTON_STANDARD_HEIGHT;
}