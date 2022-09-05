using System;
using Global.Base;
using UnityEngine;
using Utilities;

namespace Global
{
    [Serializable]
    public class SaveData : SingletonMonoBehaviour<SaveData>
    {
        private const int VersionNumber = 1;
        private string _databaseKey = $"PlayerData_{VersionNumber}";

        public Currency currency = new();
        public ThemeType selectedTheme = ThemeType.Fruit;
        [HideInInspector] public ThemeDatabase themeDatabase = new();
        public UnlockedTheme boughtTheme = new();

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

        public SaveData Load()
        {
            var jsonGameConfig = PlayerPrefs.GetString(_databaseKey);
            if (jsonGameConfig != null)
            {
                JsonUtility.FromJsonOverwrite(jsonGameConfig, this);
            }
            else
            {
                boughtTheme = new UnlockedTheme();
                Save();
            }

            return this;
        }

        public void SetSelectedTheme(ThemeType themeType)
        {
            if (!boughtTheme.items.Contains(themeType)) Debug.Log("Not Bought");
            else selectedTheme = themeType;
            Save();
        }

        public bool IsThemeUnlocked(ThemeType themeType)
        {
            return boughtTheme.items.Contains(themeType);
        }

        public bool TryBuyTheme(ThemeType themeType)
        {
            if (boughtTheme.items.Contains(themeType)) return false;
            themeDatabase.themePrice.TryGetValue(themeType, out var price);
            if (currency.gold < price) return false;
            currency.SpendCoin(price ?? 0);
            boughtTheme.items.Add(themeType);
            Save();
            return true;
        }
    }
}