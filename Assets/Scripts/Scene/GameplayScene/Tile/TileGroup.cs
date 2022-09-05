using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Global;
using Global.Base;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using UnityRandom = UnityEngine.Random;

namespace Scene.GameplayScene.Tile
{
    public class TileGroup : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup gridLayout;
        private TileObject _tilePrefab;
        private readonly List<TileObject> _openedTiles = new();
        private bool _canClick = true;
        private int _lockedTiles;


        private Action _onWin;

        public void SetCallback(Action onWin)
        {
            _onWin = onWin;
        }

        private void Start()
        {
            gridLayout.constraintCount = Consts.GameConstant.TileBoardWidth;
            _tilePrefab = Resources.Load<TileObject>(Consts.Resources.Tile);
            SpawnTiles(Consts.GameConstant.TileCount);
        }

        private void SpawnTiles(int tileCount)
        {
            if (tileCount % 2 != 0 || tileCount < 2) return;

            var prefix = "Sprites/";
            var textureName = prefix + Enum.GetName(typeof(ThemeType), Currency.Instance.saveData.selectedTheme)?.ToLower();
            var textures = Resources.LoadAll<Sprite>(textureName);

            if (textures.Length == 0)
            {
                textureName = prefix + Enum.GetName(typeof(ThemeType), ThemeType.Fruit)?.ToLower();
                textures = Resources.LoadAll<Sprite>(textureName);
            }

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

            for (int i = 0; i < listTileData.Count; i++)
            {
                listTileData[i].SetPos(y: i % Consts.GameConstant.TileBoardWidth,
                    x: i / Consts.GameConstant.TileBoardWidth);

                TileObject tileObject = Instantiate(_tilePrefab, gridLayout.transform);
                tileObject.SetNewModel(listTileData[i]);
                tileObject.SetOnClickListener(OnTileClicked);
            }
        }

        private void OnTileClicked(TileObject obj)
        {
            if (_openedTiles.Contains(obj)) return;
            if (_canClick == false) return;
            _openedTiles.Add(obj);
            obj.OpenTile();
            if (_openedTiles.Count < 2) return;
            _canClick = false;
            StartCoroutine(EnableClickAndCheck(obj));
        }


        private IEnumerator EnableClickAndCheck(TileObject obj)
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
            if (_lockedTiles != Consts.GameConstant.TileCount) return;
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