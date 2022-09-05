using System;
using Global.Base;
using TMPro;
using UnityEngine;

namespace Scene.GameplayScene
{
    public class GameTimer : MonoBehaviour
    {
        private float _time = Consts.GameConstant.Timer;
        [SerializeField] private TMP_Text _timerText;
        private float _currentTime;
        private Action _onTimerFinish;
        private bool _isCounting = true;

        private void Start()
        {
            ResetTimeAndStart(_time);
        }

        public void OnTick()
        {
            if (!_isCounting) return;
            _currentTime -= 1;
            _timerText.text = $"{_currentTime:F0}s";

            if (!(_currentTime <= 0)) return;
            _onTimerFinish?.Invoke();
            StopTimer();
        }

        public void StopTimer()
        {
            _isCounting = false;
            CancelInvoke(nameof(OnTick));
        }

        public void OnTimeOver(Action callback)
        {
            _onTimerFinish = callback;
        }

        private void ResetTimeAndStart(float time)
        {
            _time = time;
            _currentTime = _time;
            InvokeRepeating(nameof(OnTick), 0, 1);
        }
    }
}