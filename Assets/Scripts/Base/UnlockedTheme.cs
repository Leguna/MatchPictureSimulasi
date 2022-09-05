using System;
using System.Collections.Generic;

namespace Base
{
    [Serializable]
    public class UnlockedTheme
    {
        public List<ThemeType> items = new() { ThemeType.Food, ThemeType.Fruit };
    }
}