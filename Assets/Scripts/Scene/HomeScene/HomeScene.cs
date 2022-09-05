using Global.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.HomeScene
{
    public class HomeScene : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _storeButton;

        private void Start()
        {
            SetCallbacks();
        }

        private void SetCallbacks()
        {
            _playButton.onClick?.RemoveAllListeners();
            _storeButton.onClick?.RemoveAllListeners();
            _playButton.onClick?.AddListener(StartGame);
            _storeButton.onClick?.AddListener(OpenTheme);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(Consts.SceneNames.GameScene);
        }

        private void OpenTheme()
        {
            SceneManager.LoadScene(Consts.SceneNames.ThemeScene);
        }
    }
}