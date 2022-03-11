using System;
using UnityEngine;

namespace ScriptableObjects.Events.Channels
{
    [CreateAssetMenu(fileName = "New FloatEventChannelSo", menuName = "Events/FloatEventChannelSo")]
    public class FloatEventChannelSo : ScriptableObject,IEventChannelBase<float>
    {
        public Action<float> OnEventRaised { get; set; }
        public void Raise(float message) => OnEventRaised?.Invoke(message);
    }
}