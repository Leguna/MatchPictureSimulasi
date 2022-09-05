using System;
using System.Collections.Generic;
using Global;
using Global.Base;
using TMPro;
using UnityEngine;

namespace Scene.ThemeScene.ThemeList
{
    public class ThemeList : MonoBehaviour
    {
        [SerializeField] private Transform themeGroup;
        [SerializeField] private TMP_Text goldText;
        private readonly List<ThemeItem> _themeItems = new();
        private ThemeItem _themeItemPrefab;

        private void Start()
        {
            _themeItemPrefab = Resources.Load<ThemeItem>(Consts.Resources.ThemeItemPath);
            if (_themeItemPrefab == null) Debug.Log("ThemeItem not found");

            SpawnButton();
            UpdateGold();
        }

        private void UpdateGold()
        {
            var gold = SaveData.Instance.currency.gold;
            goldText.text = $"{gold}G";
        }

        private void SpawnButton()
        {
            for (int i = 0; i < Enum.GetNames(typeof(ThemeType)).Length; i++)
            {
                ThemeItem themeItem = Instantiate(_themeItemPrefab, themeGroup);
                themeItem.SetTheme((ThemeType)i);
                themeItem.SetOnClick(OnThemeSelected);
                themeItem.SetSelected((ThemeType)i == SaveData.Instance.selectedTheme);
                _themeItems.Add(themeItem);
            }
        }

        private void OnThemeSelected(ThemeType obj)
        {
            if (!SaveData.Instance.IsThemeUnlocked(obj))
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
                    SaveData.Instance.SetSelectedTheme(obj);
                }
            }
        }

        private bool TryBuy(ThemeType themeType)
        {
            return SaveData.Instance.TryBuyTheme(themeType);
        }
    }
}