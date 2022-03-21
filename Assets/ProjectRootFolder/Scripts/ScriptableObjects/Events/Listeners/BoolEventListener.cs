using ScriptableObjects.Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events.Listeners
{
    public class BoolEventListener : MonoBehaviour
    {
        public BoolEventChannelSo eventChannel;
        public UnityEvent<bool> onEventRaised;

        private void OnEnable() => eventChannel.OnEventRaised += onEventRaised.Invoke;

        private void OnDisable() => eventChannel.OnEventRaised -= onEventRaised.Invoke;
    }
}