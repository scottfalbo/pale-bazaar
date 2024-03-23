// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.NumerusObscura;

public class ObscuraBoard
{
    public ObscuraTile[,] GameBoard => _gameBoard;

    public bool IsSolved => _markedTileCount == _tileCount;

    private int _columns { get; set; }

    private ObscuraTile[,] _gameBoard { get; set; }

    private int _markedTileCount { get; set; }

    private int _rows { get; set; }

    private int _tileCount { get; set; }

    private ObscuraBoard(int rows, int columns)
    {
        _gameBoard = new ObscuraTile[rows, columns];
        _rows = rows;
        _columns = columns;

        _tileCount = rows * columns;
    }

    public static ObscuraBoard Conjure(int rows, int columns)
    {
        var board = new ObscuraBoard(rows, columns);

        return board;
    }

    private void ConstructBoard(ObscuraBoard board)
    {
        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                _gameBoard[row, column] = new ObscuraTile
                {
                    Row = row,
                    Column = column,
                    State = TileState.Conjuring
                };
            }
        }
    }
}