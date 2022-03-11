using System;
using UnityEngine;

namespace ScriptableObjects.Events.Channels
{
    [CreateAssetMenu(fileName = "New PassableDataEventChannelSo", menuName = "Events/PassableDataEventChannelSo")]
    public class PassableDataEventChannel : ScriptableObject, IEventChannelBase<IPassableData>
    {
        public Action<IPassableData> OnEventRaised { get; set; }

        public void Raise(IPassableData param) => OnEventRaised?.Invoke(param);

    }
}