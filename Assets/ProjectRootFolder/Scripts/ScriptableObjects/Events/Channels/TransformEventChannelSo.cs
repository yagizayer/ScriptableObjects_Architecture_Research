using System;
using UnityEngine;

namespace ScriptableObjects.Events.Channels
{
    [CreateAssetMenu(fileName = "New TransformEventChannelSo", menuName = "Events/TransformEventChannelSo")]
    public class TransformEventChannelSo : ScriptableObject, IEventChannelBase<Transform>
    {
        public Action<Transform> OnEventRaised { get; set; }
        public void Raise(Transform param) => OnEventRaised?.Invoke(param);
    }
}