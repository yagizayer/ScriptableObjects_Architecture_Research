using ScriptableObjects.Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events.Listeners
{
    public class ColliderEventListener : MonoBehaviour
    {
        public ColliderEventChannelSo eventChannel;
        public UnityEvent<Collider> onEventRaised;

        private void OnEnable() => eventChannel.OnEventRaised += onEventRaised.Invoke;

        private void OnDisable() => eventChannel.OnEventRaised -= onEventRaised.Invoke;
    }
}