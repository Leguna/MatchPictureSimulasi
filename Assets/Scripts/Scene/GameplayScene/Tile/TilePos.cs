namespace Scene.GameplayScene.Tile
{
    public struct TilePos
    {
        public TilePos(int y, int x)
        {
            Y = y;
            X = x;
        }

        public int X { get; }
        public int Y { get; }
    }
}