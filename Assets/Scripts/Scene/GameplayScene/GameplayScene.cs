using Global.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.GameplayScene
{
    public class GameplayScene : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        private void Start()
        {
            _backButton.onClick.RemoveAllListeners();
            _backButton.onClick.AddListener(BackToMenu);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(Consts.SceneNames.Home);
        }
    }
}