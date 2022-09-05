using Global.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.ThemeScene
{
    public class ThemeScene : MonoBehaviour
    {
        [SerializeField] private Button backButton;

        private void Start()
        {
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(BackToHome);
        }

        private void BackToHome()
        {
            SceneManager.LoadScene(Consts.SceneNames.Home);
        }
    }
}