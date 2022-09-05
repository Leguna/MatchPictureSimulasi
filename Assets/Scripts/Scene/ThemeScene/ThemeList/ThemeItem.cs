using System;
using Global;
using Global.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scene.ThemeScene.ThemeList
{
    public class ThemeItem : MonoBehaviour
    {
        [SerializeField] private GameObject frontBg;
        [SerializeField] private TMP_Text nameTag;
        [SerializeField] private TMP_Text priceTag;
        [SerializeField] private GameObject checkMark;
        [SerializeField] private Button _button;

        public ThemeType ThemeType { get; private set; }

        private void UpdateView()
        {
            SetNameTag(ThemeType.ToString());
            SetPrice(SaveData.Instance.themeDatabase.themePrice?[ThemeType]);
            SetUnlocked(SaveData.Instance.IsThemeUnlocked(ThemeType));
        }

        private void SetNameTag(string themeTypeName)
        {
            nameTag.text = $"{themeTypeName}";
        }

        public void SetTheme(ThemeType themeType)
        {
            ThemeType = themeType;
            UpdateView();
        }

        public void SetPrice(int? i)
        {
            priceTag.text = i == null ? "FREE" : $"{i}G";
        }

        public void SetSelected(bool value)
        {
            checkMark.gameObject.SetActive(value);
            UpdateView();
        }

        public void SetUnlocked(bool value)
        {
            frontBg.SetActive(!value);
            priceTag.gameObject.SetActive(!value);
        }

        public void SetOnClick(Action<ThemeType> onClick)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => onClick(ThemeType));
        }
    }
}