using UnityEngine;

namespace ScriptableObjects
{
    public class CustomChannelEventRaiser : MonoBehaviour
    {
        [SerializeField] protected ScriptableObject eventChannel;
        [SerializeField] protected ScriptableObject passableData;


        private void OnValidate()
        {
            if (eventChannel != null && !(eventChannel is IEventChannelBase<IPassableData>))
            {
                Debug.LogError($"{eventChannel.name} must implement IEventChannelBase<IPassableData>");
                eventChannel = null;
            }

            if (passableData != null && !(passableData is IPassableData))
            {
                Debug.LogError($"{passableData.name} must implement IPassableData");
                passableData = null;
            }
        }

        public virtual void RaiseEvent() =>
            (eventChannel as IEventChannelBase<IPassableData>).Raise(passableData as IPassableData);
    }
}