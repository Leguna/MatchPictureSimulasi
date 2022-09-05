using System;
using Scene.GameplayScene.InputRaycast;
using UnityEngine;
using UnityEngine.UI;

namespace Scene.GameplayScene.Tile
{
    public class TileObject : MonoBehaviour, IRaycastable
    {
        [SerializeField] private Image tileImage;
        private TileData _tileData;
        private Action<TileObject> _onClick;
        private bool _interactable = true;

        private void UpdateSprite()
        {
            tileImage.sprite = _tileData.TileSprite;
        }

        public void SetOnClickListener(Action<TileObject> onClick)
        {
            _onClick = onClick;
        }

        public bool IsImageSame(TileObject tileObject)
        {
            return _tileData.TileSprite.name.Equals(tileObject._tileData.TileSprite.name);
        }

        private void SetTileState(TileState newState)
        {
            _tileData.State = newState;
            switch (_tileData.State)
            {
                case TileState.Open:
                    gameObject.SetActive(true);
                    _interactable = false;
                    tileImage.gameObject.SetActive(true);
                    break;
                case TileState.Closed:
                    _interactable = true;
                    gameObject.SetActive(true);
                    tileImage.gameObject.SetActive(false);
                    break;
                case TileState.Locked:
                    gameObject.SetActive(true);
                    _interactable = false;
                    tileImage.gameObject.SetActive(true);
                    break;
                default:
                    gameObject.SetActive(false);
                    _interactable = false;
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

        public void SetOnClick()
        {
            if (_interactable)
                _onClick?.Invoke(this);
        }
    }
}