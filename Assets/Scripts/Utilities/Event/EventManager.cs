using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Event
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, Action<Dictionary<string, object>>> _eventDictionary;
        private static EventManager _eventManager;

        private static EventManager Instance
        {
            get
            {
                if (_eventManager) return _eventManager;
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                    Debug.LogError(
                        "There needs to be one active EventManager script on a GameObject in your scene.");
                else
                {
                    _eventManager.Init();
                    DontDestroyOnLoad(_eventManager);
                }
                return _eventManager;
            }
        }

        private void Init()
        {
            _eventDictionary ??= new Dictionary<string, Action<Dictionary<string, object>>>();
        }

        public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent += listener;
                Instance._eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                Instance._eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            if (_eventManager == null) return;
            if (!Instance._eventDictionary.TryGetValue(eventName, out var thisEvent)) return;
            thisEvent -= listener;
            Instance._eventDictionary[eventName] = thisEvent;
        }

        public static void TriggerEvent(string eventName, Dictionary<string, object> message)
        {
            if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent)) thisEvent.Invoke(message);
        }
    }
}