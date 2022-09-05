using Scene.GameplayScene.Tile;
using UnityEngine;

namespace Scene.GameplayScene.InputRaycast
{
    public class InputRaycast : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider == null) return;

            var obj = hit.collider.GetComponent<TileObject>();
            if (obj != null) obj.SetOnClick();
        }
    }
}