using System;
using Helpers;
using UnityEngine;

namespace ScriptableObjects
{
    public class ChannelEventRaiser : CustomChannelEventRaiser
    {
        [SerializeField] private UnitySpecificEvents unitySpecificEvents;

        public override void RaiseEvent()
        {
            var channel = (eventChannel as IEventChannelBase<IPassableData>);
            channel.Raise(passableData as IPassableData);
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
                RaiseEvent();
        }

        private void OnTriggerStay(Collider other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnTriggerStay)
                RaiseEvent();
        }

        private void OnTriggerExit(Collider other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnTriggerExit)
                RaiseEvent();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnCollisionEnter)
                RaiseEvent();
        }

        private void OnCollisionStay(Collision other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnCollisionStay)
                RaiseEvent();
        }

        private void OnCollisionExit(Collision other)
        {
            if (unitySpecificEvents == UnitySpecificEvents.OnCollisionExit)
                RaiseEvent();
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