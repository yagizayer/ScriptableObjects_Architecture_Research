using System;
using UnityEngine;

namespace ScriptableObjects.Events.Channels
{
    [CreateAssetMenu(fileName = "New Vector3EventChannelSo", menuName = "Events/Vector3EventChannelSo")]
    public class Vector3EventChannelSo : ScriptableObject, IEventChannelBase<Vector3>
    {
        public Action<Vector3> OnEventRaised { get; set; }
        public void Raise(Vector3 message) => OnEventRaised?.Invoke(message);
    }
}