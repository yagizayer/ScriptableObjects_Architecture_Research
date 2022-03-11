using ScriptableObjects.Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events.Listeners
{
    public class VoidEventListener : MonoBehaviour
    {
        public VoidEventChannelSo eventChannel;
        public UnityEvent onEventRaised;

        private void OnEnable() => eventChannel.OnEventRaised += onEventRaised.Invoke;
        private void OnDisable() => eventChannel.OnEventRaised += onEventRaised.Invoke;
    }
}