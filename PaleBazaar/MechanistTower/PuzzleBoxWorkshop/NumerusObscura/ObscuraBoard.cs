// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.NumerusObscura;

public class ObscuraBoard
{
    public Dictionary<int, List<int>> ColumnNumbers => _columnNumbers;

    public ObscuraTile[,] GameBoard => _gameBoard;

    public bool IsSolved => _markedTileCount == _tileCount;

    public Dictionary<int, List<int>> RowNumbers => _rowNumbers;

    private Dictionary<int, List<int>> _columnNumbers { get; set; } = [];

    private int _columns { get; set; }

    private ObscuraTile[,] _gameBoard { get; set; }

    private int _markedTileCount { get; set; }

    private Dictionary<int, List<int>> _rowNumbers { get; set; } = [];

    private int _rows { get; set; }

    private int _tileCount { get; set; }

    private ObscuraBoard(int rows, int columns)
    {
        _gameBoard = new ObscuraTile[rows, columns];
        _rows = rows;
        _columns = columns;
        _tileCount = rows * columns;

        ConstructBoard();
    }

    public static ObscuraBoard Conjure(int rows, int columns) => new(rows, columns);

    private void AnalyzeSegments(Dictionary<int, List<ObscuraTile>> columns, Dictionary<int, List<ObscuraTile>> rows)
    {
        foreach (var column in columns)
        {
            _columnNumbers[column.Key] = Combulator.DetermineSequence(column.Value);
        }

        foreach (var row in rows)
        {
            _rowNumbers[row.Key] = Combulator.DetermineSequence(row.Value);
        }
    }

    private void ConstructBoard()
    {
        var percentageToMark = Combulator.RandomPercentage(30, 70);
        var position = 1;

        var columns = new Dictionary<int, List<ObscuraTile>>();
        var rows = new Dictionary<int, List<ObscuraTile>>();

        for (var row = 0; row < _rows; row++)
        {
            if (!rows.ContainsKey(row))
            {
                rows.Add(row, []);
            }

            for (var column = 0; column < _columns; column++)
            {
                if (!columns.ContainsKey(column))
                {
                    columns.Add(column, []);
                }

                var state = Combulator.MarkTile(percentageToMark) ? TileState.HasTile : TileState.Empty;

                var tile = new ObscuraTile(row, column, position, state);
                _gameBoard[row, column] = tile;

                rows[row].Add(tile);
                columns[column].Add(tile);

                position++;
            }
        }

        AnalyzeSegments(columns, rows);
    }
}