using System;
using System.Collections.Generic;
using Global.Base;

namespace Global
{
    [Serializable]
    public class ThemeDatabase
    {
        public Dictionary<ThemeType?, int?> themePrice = new()
        {
            { ThemeType.Food, 0 }, { ThemeType.Fruit, 0 }, { ThemeType.Weapon, 100 },
            { ThemeType.Random, 200 }
        };
    }
}