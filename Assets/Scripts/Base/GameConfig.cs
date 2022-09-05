using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Base
{
    [Serializable]
    public class GameConfig : SingletonMonoBehaviour<GameConfig>
    {
        public int gold;
        public ThemeType selectedType = ThemeType.Fruit;

        public UnlockedTheme unlockedTheme = new();

        public Dictionary<ThemeType, int> themePrice = new()
        {
            { ThemeType.Food, 0 }, { ThemeType.Fruit, 0 }, { ThemeType.Weapon, 100 },
            { ThemeType.Random, 200 }
        };

        private const int VersionNumber = 1;
        private string _databaseKey = $"GameConfig_{VersionNumber}";

        protected override void Awake()
        {
            base.Awake();
            Load();
        }

        public void Save()
        {
            var jsonGameConfig = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(_databaseKey, jsonGameConfig);
            PlayerPrefs.Save();
        }

        public GameConfig Load()
        {
            var jsonGameConfig = PlayerPrefs.GetString(_databaseKey);
            JsonUtility.FromJsonOverwrite(jsonGameConfig, this);
            return this;
        }

        public bool BuyTheme(ThemeType themeType)
        {
            if (unlockedTheme.items.Contains(themeType)) Debug.Log("Already Buy");
            if (gold < themePrice[themeType]) return false;
            gold -= themePrice[themeType];
            unlockedTheme.items.Add(themeType);
            return true;
        }

        public void SetSelectedTheme(ThemeType themeType)
        {
            if (!unlockedTheme.items.Contains(themeType)) Debug.Log("Not Buy");
            selectedType = themeType;
            Save();
        }

        public int AddGold(int amount)
        {
            gold += amount;
            return gold;
        }

        public bool IsThemeUnlocked(ThemeType themeType)
        {
            return unlockedTheme.items.Contains(themeType);
        }
    }
}