using Base;
using Tile;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Timer _timer;
    [SerializeField] private Reward _reward;
    [SerializeField] private TileBoard _tileBoard;


    private void Start()
    {
        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(LoadBackButton);
        _timer.SetCallback(OnFinishTimer);
        _tileBoard.SetCallback(OnWin);
        _reward.OnCloseSetCallback(LoadBackButton);
    }

    private void OnFinishTimer()
    {
        _reward.Show("You Lose!", "No Reward");
    }

    private void OnWin()
    {
        _timer.StopTimer();
        _reward.Show("Well Done!", $"+{Constants.GameConstant.RewardGold}G");
        GameConfig.Instance.AddGold(Constants.GameConstant.RewardGold);
    }

    private void LoadBackButton()
    {
        SceneManager.LoadScene(Constants.SceneNames.MainScene);
    }
}