using ScriptableObjects.Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events.Listeners
{
    public class StringEventListener : MonoBehaviour
    {
        public StringEventChannelSo eventChannel;
        public UnityEvent<string> onEventRaised;

        private void OnEnable() => eventChannel.OnEventRaised += onEventRaised.Invoke;

        private void OnDisable() => eventChannel.OnEventRaised -= onEventRaised.Invoke;
    }
}