using System;
using UnityEngine;

namespace ScriptableObjects.Events.Channels
{
    [CreateAssetMenu(fileName = "New ColliderEventChannelSo", menuName = "Events/ColliderEventChannelSo")]
    public class ColliderEventChannelSo : ScriptableObject, IEventChannelBase<Collider>
    {
        public Action<Collider> OnEventRaised { get; set; }
        public void Raise(Collider collider) => OnEventRaised?.Invoke(collider);
    }
}