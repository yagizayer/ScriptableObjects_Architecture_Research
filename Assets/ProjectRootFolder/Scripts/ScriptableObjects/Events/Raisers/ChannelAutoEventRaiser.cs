using System;
using Helpers;
using UnityEngine;

namespace ScriptableObjects
{
    public class ChannelAutoEventRaiser : MonoBehaviour
    {
        [SerializeField] private ScriptableObject eventChannel;

        [SerializeField] private UnitySpecificEvents unitySpecificEvents;
        [SerializeField] private bool forceVoidEvent = false;

        private void OnValidate()
        {
            if (eventChannel != null && !(eventChannel is IEventChannel))
            {
                Debug.LogError($"{eventChannel.name} must implement IEventChannelBase");
                eventChannel = null;
            }
        }

        public void RaiseEvent()
        {
            var channel = (eventChannel as IEventChannelBase);
            channel?.Raise();
        }

        private void RaiseEvent<T>(T param)
        {
            if(forceVoidEvent)
            {
                RaiseEvent();
                return;
            }
            var channel = (eventChannel as IEventChannelBase<T>);
            channel?.Raise(param);
        }

        private void Awake()
        {
            if (unitySpecificEvents == UnitySpecificEvents.Awake)
                RaiseEvent();
        }

        private void Start()
        {
            if (unitySpecificEvents == UnitySpecificEvents.Start)
                RaiseEvent();
        }

        private void OnEnable()
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnEnable)
                RaiseEvent();
        }

        private void OnDisable()
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnDisable)
                RaiseEvent();
        }

        private void OnDestroy()
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnDestroy)
                RaiseEvent();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnTriggerEnter)
                RaiseEvent(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnTriggerStay)
                RaiseEvent(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnTriggerExit)
                RaiseEvent(other);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnCollisionEnter)
                RaiseEvent(other);
        }

        private void OnCollisionStay(Collision other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnCollisionStay)
                RaiseEvent(other);
        }

        private void OnCollisionExit(Collision other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnCollisionExit)
                RaiseEvent(other);
        }

        private void Update()
        {
            if (unitySpecificEvents == UnitySpecificEvents.Update)
                RaiseEvent();
        }

        private void FixedUpdate()
        {
            if (unitySpecificEvents == UnitySpecificEvents.FixedUpdate)
                RaiseEvent();
        }

        private void LateUpdate()
        {
            if (unitySpecificEvents == UnitySpecificEvents.LateUpdate)
                RaiseEvent();
        }
    }
}