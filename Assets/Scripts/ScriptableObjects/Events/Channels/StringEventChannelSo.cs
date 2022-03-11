using System;
using UnityEngine;

namespace ScriptableObjects.Events.Channels
{
    [CreateAssetMenu(fileName = "New StringEventChannelSo", menuName = "Events/StringEventChannelSo")]
    public class StringEventChannelSo : ScriptableObject, IEventChannelBase<string>
    {

        public Action<string> OnEventRaised { get; set; }
        public void Raise(string message) => OnEventRaised?.Invoke(message);
    }
}