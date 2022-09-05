using Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _storeMenu;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _storeButton;

    private void Start()
    {
        SetCallbacks();
    }

    private void SetCallbacks()
    {
        _playButton.onClick?.AddListener(StartGame);
        _storeButton.onClick?.AddListener(OpenStore);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(Constants.SceneNames.GameScene);
    }

    private void OpenStore()
    {
        _storeMenu.SetActive(true);
    }
}