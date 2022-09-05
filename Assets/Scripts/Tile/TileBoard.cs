using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using UnityRandom = UnityEngine.Random;

namespace Tile
{
    public class TileBoard : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup gridLayout;
        private TileController _tilePrefab;
        private readonly List<TileController> _openedTiles = new();
        private bool _canClick = true;
        private int _lockedTiles;


        private Action _onWin;

        public void SetCallback(Action onWin)
        {
            _onWin = onWin;
        }

        private void Start()
        {
            gridLayout.constraintCount = Constants.GameConstant.MaxColumn;
            _tilePrefab = Resources.Load<TileController>(Constants.GameResources.Tile);
            SpawnTiles(Constants.GameConstant.TileCount);
        }

        private void SpawnTiles(int tileCount)
        {
            if (tileCount % 2 != 0 || tileCount < 2) return;

            var textureName = "Sprites/" + Enum.GetName(typeof(ThemeType), GameConfig.Instance.selectedType)?.ToLower();
            var textures = Resources.LoadAll<Sprite>(textureName);
            List<TileData> listTileData = new();

            for (int i = 0; i < tileCount / 2; i++)
            {
                var randomIndexTexture = GetTextureNumber(textures);
                for (int j = 0; j < 2; j++)
                {
                    var newTile = new TileData(TileState.Closed, randomIndexTexture);
                    listTileData.Add(newTile);
                }
            }

            listTileData = RandomizeBoard(listTileData);

            foreach (var tileData in listTileData)
            {
                TileController tileController = Instantiate(_tilePrefab, transform);
                tileController.SetNewModel(tileData);
                tileController.SetOnClickListener(OnTileClicked);
            }
        }

        private void OnTileClicked(TileController obj)
        {
            if (_openedTiles.Contains(obj)) return;
            if (_canClick == false) return;
            _openedTiles.Add(obj);
            obj.OpenTile();
            if (_openedTiles.Count < 2) return;
            _canClick = false;
            StartCoroutine(EnableClickAndCheck(obj));
        }


        private IEnumerator EnableClickAndCheck(TileController obj)
        {
            yield return new WaitForSeconds(0.5f);
            if (obj.IsImageSame(_openedTiles[0]))
            {
                _openedTiles.ForEach(tile => { tile.LockTile(); });
                _lockedTiles += 2;
                CheckIsAllLocked();
            }
            else
                _openedTiles.ForEach(tile => { tile.CloseTile(); });

            _openedTiles.Clear();
            _canClick = true;
        }

        private void CheckIsAllLocked()
        {
            if (_lockedTiles != Constants.GameConstant.TileCount) return;
            _onWin?.Invoke();
            gameObject.SetActive(false);
        }

        private Sprite GetTextureNumber(Sprite[] textures)
        {
            return textures[UnityRandom.Range(0, textures.Length)];
        }

        private List<TileData> RandomizeBoard(List<TileData> listTileData)
        {
            var rand = new Random();
            return listTileData.OrderBy(_ => rand.Next()).ToList();
        }
    }
}