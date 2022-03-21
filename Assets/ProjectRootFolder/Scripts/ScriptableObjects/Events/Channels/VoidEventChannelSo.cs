using System;
using UnityEngine;

namespace ScriptableObjects.Events.Channels
{
    [CreateAssetMenu(fileName = "New VoidEventChannelSo", menuName = "Events/VoidEventChannelSo")]
    public class VoidEventChannelSo : ScriptableObject,IEventChannelBase
    {
        public Action OnEventRaised { get; set; }
        public void Raise() => OnEventRaised?.Invoke();
    }
}