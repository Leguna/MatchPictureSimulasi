using Global;
using Global.Base;
using Scene.GameplayScene.Tile;
using UnityEngine;
using UnityEngine.Events;

namespace Scene.GameplayScene
{
    public class GameFlow : MonoBehaviour
    {
        [SerializeField] private GameTimer _gameTimer;
        [SerializeField] private RewardView _rewardView;
        [SerializeField] private TileGroup _tileGroup;
        [SerializeField] private UnityEvent BackToMainMenuEvent;

        private GameState _state;

        private void Start()
        {
            var saveData = SaveData.Instance.Load();
            _gameTimer.OnTimeOver(() => SetGameOverState(GameState.Lose));
            _tileGroup.SetCallback(() => SetGameOverState(GameState.Win));
            _tileGroup.SpawnBoard(saveData.selectedTheme);
            _rewardView.OnCloseSetCallback(BackToMainMenuEvent.Invoke);
        }

        private void SetGameOverState(GameState state)
        {
            switch (state)
            {
                case GameState.Lose:
                    OnLose();
                    break;
                case GameState.Win:
                    OnWin();
                    break;
                case GameState.Play:
                    break;
                default:
                    Debug.Log("No State");
                    break;
            }
        }

        private void OnLose()
        {
            Debug.Log("Game Over, You Lose!");
            _rewardView.Show("You Lose!", "No Reward");
        }

        private void OnWin()
        {
            Debug.Log("Game Over, You Win! Get 100G as Reward");
            _gameTimer.StopTimer();
            Currency.Instance.AddGold(Consts.GameConstant.RewardGold);
            _rewardView.Show("Well Done!", $"+{Consts.GameConstant.RewardGold}G");
        }
    }
}