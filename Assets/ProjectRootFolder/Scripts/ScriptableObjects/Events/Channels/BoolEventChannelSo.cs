using System;
using UnityEngine;

namespace ScriptableObjects.Events.Channels
{
    [CreateAssetMenu(fileName = "New BoolEventChannelSo", menuName = "Events/BoolEventChannelSo")]
    public class BoolEventChannelSo : ScriptableObject,IEventChannelBase<bool>
    {
        public Action<bool> OnEventRaised { get; set; }
        public void Raise(bool message) => OnEventRaised?.Invoke(message);
    }
}