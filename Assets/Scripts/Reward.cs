using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    [SerializeField] private GameObject _reward;
    [SerializeField] private TMP_Text _rewardTitle;
    [SerializeField] private TMP_Text _rewardDescription;
    [SerializeField] private Button _onClickButton;

    public void Show(string rewardTitle, string rewardDescription)
    {
        _rewardTitle.text = rewardTitle;
        _rewardDescription.text = rewardDescription;
        _reward.SetActive(true);
    }

    public void OnCloseSetCallback(Action callback)
    {
        _onClickButton.onClick.RemoveAllListeners();
        _onClickButton.onClick.AddListener(() => callback());
    }
}