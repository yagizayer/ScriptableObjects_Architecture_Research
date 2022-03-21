using ScriptableObjects.Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events.Listeners
{
    public class PassableDataEventListener : MonoBehaviour
    {
        public PassableDataEventChannel eventChannel;
        public UnityEvent<IPassableData> onEventRaised;

        private void OnEnable() => eventChannel.OnEventRaised += onEventRaised.Invoke;

        private void OnDisable() => eventChannel.OnEventRaised -= onEventRaised.Invoke;
    }
}