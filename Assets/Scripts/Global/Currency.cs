using System;

namespace Global
{
    [Serializable]
    public class Currency
    {
        public int gold;

        public int AddGold(int amount)
        {
            gold += amount;
            return gold;
        }

        public int SpendCoin(int amount)
        {
            gold -= amount;
            return gold;
        }
    }
}