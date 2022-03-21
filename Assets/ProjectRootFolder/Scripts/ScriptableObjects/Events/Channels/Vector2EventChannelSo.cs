using System;
using UnityEngine;

namespace ScriptableObjects.Events.Channels
{
    [CreateAssetMenu(fileName = "New Vector2EventChannelSo", menuName = "Events/Vector2EventChannelSo")]
    public class Vector2EventChannelSo : ScriptableObject, IEventChannelBase<Vector2>
    {
        public Action<Vector2> OnEventRaised { get; set; }
        public void Raise(Vector2 message) => OnEventRaised?.Invoke(message);
    }
}