using System;
using System.Collections.Generic;

namespace Global.Base
{
    [Serializable]
    public class UnlockedTheme
    {
        public List<ThemeType> items = new() { ThemeType.Food, ThemeType.Fruit };
    }
}