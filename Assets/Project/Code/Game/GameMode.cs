namespace Game
{
    public class GameMode
    {
        public int Rows { get; }
        public int Columns { get; }
        public int PairsCount { get; }

        public GameMode(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            PairsCount = rows * columns / 2;
        }
    }
}
