using System;
using System.Collections.Generic;
using Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Theme
{
    public class ThemeSelection : MonoBehaviour
    {
        [SerializeField] private Transform themeGroup;
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private Button backButton;
        private readonly List<ThemeItem> _themeItems = new();
        private ThemeItem _themeItemPrefab;

        private void Start()
        {
            _themeItemPrefab = Resources.Load<ThemeItem>(ResourcesPathConstants.ThemeItemPath);
            if (_themeItemPrefab == null) Debug.LogError("ThemeItem not found");

            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(() => gameObject.SetActive(false));
            SpawnButton();
            UpdateGold();
        }

        private void UpdateGold()
        {
            var gold = GameConfig.Instance.gold;
            goldText.text = $"{gold}G";
        }

        private void SpawnButton()
        {
            for (int i = 0; i < Enum.GetNames(typeof(ThemeType)).Length; i++)
            {
                var themeItem = Instantiate(_themeItemPrefab, themeGroup);
                themeItem.SetTheme((ThemeType)i);
                themeItem.SetOnClick(OnThemeSelected);
                themeItem.SetSelected((ThemeType)i == GameConfig.Instance.selectedType);
                _themeItems.Add(themeItem);
            }
        }

        private void OnThemeSelected(ThemeType obj)
        {
            if (!GameConfig.Instance.IsThemeUnlocked(obj))
            {
                var isBuy = TryBuy(obj);
                if (!isBuy) return;
            }

            UpdateGold();
            foreach (var themeItem in _themeItems)
            {
                themeItem.SetSelected(false);
                if (themeItem.ThemeType == obj)
                {
                    themeItem.SetSelected(true);
                    GameConfig.Instance.SetSelectedTheme(obj);
                }
            }
        }

        private bool TryBuy(ThemeType themeType)
        {
            return GameConfig.Instance.BuyTheme(themeType);
        }
    }
}