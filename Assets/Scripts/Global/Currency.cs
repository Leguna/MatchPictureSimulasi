using System;
using Global.Base;
using Utilities;

namespace Global
{
    [Serializable]
    public class Currency : SingletonMonoBehaviour<Currency>
    {
        public SaveData saveData;

        protected override void Awake()
        {
            base.Awake();
            saveData.Load();
        }

        public int AddGold(int amount)
        {
            saveData.gold += amount;
            saveData.Save();
            return saveData.gold;
        }

        public int SpendCoin(int amount)
        {
            saveData.gold -= amount;
            saveData.Save();
            return saveData.gold;
        }


        public bool TryBuyTheme(ThemeType themeType)
        {
            if (saveData.boughtTheme.items.Contains(themeType)) return false;
            saveData.themeDatabase.themePrice.TryGetValue(themeType, out var price);
            if (saveData.gold < price) return false;
            SpendCoin(price ?? 0);
            saveData.boughtTheme.items.Add(themeType);
            saveData.Save();
            return true;
        }
    }
}