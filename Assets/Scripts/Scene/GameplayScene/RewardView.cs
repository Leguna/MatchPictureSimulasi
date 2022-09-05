using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scene.GameplayScene
{
    public class RewardView : MonoBehaviour
    {
        [SerializeField] private Image _reward;
        [SerializeField] private GameObject _rewardGroup;
        [SerializeField] private TMP_Text _rewardTitle;
        [SerializeField] private TMP_Text _rewardDescription;
        [SerializeField] private Button _onClickButton;

        private void Start()
        {
            Hide();
        }

        private void Hide()
        {
            _reward.enabled = false;
            _onClickButton.interactable = false;
            _rewardGroup.SetActive(false);
        }

        public void Show(string rewardTitle, string rewardDescription)
        {
            _rewardTitle.text = rewardTitle;
            _rewardDescription.text = rewardDescription;
            _reward.enabled = true;
            _onClickButton.interactable = true;
            _rewardGroup.SetActive(true);
        }

        public void OnCloseSetCallback(Action callback)
        {
            _onClickButton.onClick.RemoveAllListeners();
            _onClickButton.onClick.AddListener(() => callback());
        }
    }
}