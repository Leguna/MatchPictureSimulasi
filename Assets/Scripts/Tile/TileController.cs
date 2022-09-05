using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tile
{
    public class TileController : MonoBehaviour
    {
        [SerializeField] private Image tileImage;
        [SerializeField] private Button button;
        private TileData _tileData;

        private void UpdateSprite()
        {
            tileImage.sprite = _tileData.TileSprite;
        }

        public void SetOnClickListener(Action<TileController> onClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick.Invoke(this));
        }

        public bool IsImageSame(TileController controller)
        {
            return _tileData.TileSprite.name.Equals(controller._tileData.TileSprite.name);
        }

        private void SetTileState(TileState newState)
        {
            _tileData.State = newState;
            switch (_tileData.State)
            {
                case TileState.Open:
                    gameObject.SetActive(true);
                    button.interactable = false;
                    tileImage.gameObject.SetActive(true);
                    break;
                case TileState.Closed:
                    button.interactable = true;
                    gameObject.SetActive(true);
                    tileImage.gameObject.SetActive(false);
                    break;
                case TileState.Locked:
                    gameObject.SetActive(true);
                    button.interactable = false;
                    tileImage.gameObject.SetActive(true);
                    break;
                default:
                    gameObject.SetActive(false);
                    button.interactable = false;
                    tileImage.gameObject.SetActive(false);
                    break;
            }
        }

        public void SetNewModel(TileData newTileData)
        {
            _tileData = newTileData;
            SetTileState(_tileData.State);
            UpdateSprite();
        }

        public void OpenTile()
        {
            SetTileState(TileState.Open);
        }

        public void CloseTile()
        {
            SetTileState(TileState.Closed);
        }

        public void LockTile()
        {
            SetTileState(TileState.Locked);
        }
    }

    public enum TileState
    {
        Open,
        Closed,
        Locked
    }
}