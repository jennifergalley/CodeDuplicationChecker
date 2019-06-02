namespace Models
{
    public struct Location
    {
        public readonly int row;
        public readonly int column;

        public Location(int row, int col)
        {
            this.row = row;
            this.column = col;
        }
    }
}
