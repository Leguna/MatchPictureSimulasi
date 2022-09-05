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

        public int gold;
        public ThemeType selectedTheme = ThemeType.Fruit;
        [HideInInspector] public ThemeDatabase themeDatabase = new();
        public UnlockedTheme boughtTheme = new();

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
    }
}