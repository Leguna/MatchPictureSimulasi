using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Event
{
    public class Producer : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            EventManager.TriggerEvent("addCoins", new Dictionary<string, object> { { "amount", 1 } });
        }
    }
}