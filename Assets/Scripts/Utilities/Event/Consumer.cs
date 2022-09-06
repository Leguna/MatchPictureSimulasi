using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Event
{
    public class Consumer : MonoBehaviour
    {
        private int _coins;

        private void OnEnable()
        {
            EventManager.StartListening("addCoins", OnAddCoins);
        }

        private void OnDisable()
        {
            EventManager.StopListening("addCoins", OnAddCoins);
        }

        private void OnAddCoins(Dictionary<string, object> message)
        {
            var amount = (int)message["amount"];
            _coins += amount;
        }
    }
}