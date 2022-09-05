using UnityEngine;

namespace Scene.GameplayScene.Tile
{
    public class TileData
    {
        public TileData(TileState state, Sprite tileSprite)
        {
            State = state;
            TileSprite = tileSprite;
            Position = new TilePos();
        }

        public Sprite TileSprite { get; }
        public TileState State { get; set; }

        public TilePos Position { get; private set; }

        public void SetPos(int x,int y)
        {
            Position = new TilePos(x, y);
        }
    }
}