using UnityEngine;

namespace Tile
{
    public struct TileData
    {
        public TileData(TileState state, Sprite tileSprite)
        {
            State = state;
            TileSprite = tileSprite;
        }

        public Sprite TileSprite { get; }
        public TileState State { get; set; }
    }
}