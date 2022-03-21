using ScriptableObjects.Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events.Listeners
{
    public class Vector3EventListener : MonoBehaviour
    {
        public Vector3EventChannelSo eventChannel;
        public UnityEvent<Vector3> onEventRaised;

        private void OnEnable() => eventChannel.OnEventRaised += onEventRaised.Invoke;

        private void OnDisable() => eventChannel.OnEventRaised -= onEventRaised.Invoke;
    }
}