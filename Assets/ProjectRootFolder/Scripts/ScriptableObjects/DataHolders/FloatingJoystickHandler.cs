using System;
using MyBox;
using ScriptableObjects;
using ScriptableObjects.Events.Channels;
using UnityEngine;

namespace ProjectRootFolder.Scripts.ScriptableObjects.DataHolders
{
    [CreateAssetMenu(fileName = "New FloatingJoystickHandlerSo",
        menuName = "ScriptableObjects/FloatingJoystickHandlerSo")]
    public class FloatingJoystickHandler : ScriptableObject, IInputHandler
    {
        [Foldout("Broadcasting Channels", true), SerializeField]
        private Vector2EventChannelSo touchStartedEventChannel;

        [SerializeField] private Vector2EventChannelSo touchMovedEventChannel;
        [SerializeField] private Vector2EventChannelSo touchStationaryEventChannel;
        [SerializeField] private Vector2EventChannelSo touchEndedEventChannel;

        [Foldout("Listening Channels", true), SerializeField]
        private Vector2EventChannelSo joystickMoveEventChannel;

        public bool InputEnabled { get; private set; }


        public Vector2EventChannelSo TouchStartedEventChannel => touchStartedEventChannel;
        public Vector2EventChannelSo TouchMovedEventChannel => touchMovedEventChannel;
        public Vector2EventChannelSo TouchStationaryEventChannel => touchStationaryEventChannel;
        public Vector2EventChannelSo TouchEndedEventChannel => touchEndedEventChannel;


        private Vector2 _joystickDirection;

        //--------------------------------------------------------------------------------------------------------------

        private void OnEnable()
        {
            if (joystickMoveEventChannel == null)
                joystickMoveEventChannel = Resources.Load<Vector2EventChannelSo>("Events/JoystickMoveEventChannel");

            joystickMoveEventChannel.OnEventRaised += OnJoystickMove;
        }

        //--------------------------------------------------------------------------------------------------------------

        public void Update() => GetTouch();
        public void EnableInput() => InputEnabled = true;
        public void DisableInput() => InputEnabled = false;

        //--------------------------------------------------------------------------------------------------------------

        private void GetTouch()
        {
            if (Input.touches.Length <= 0) return;

            var touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartedEventChannel.Raise(touch.position);
                    break;
                case TouchPhase.Moved:
                    touchMovedEventChannel.Raise(_joystickDirection);
                    break;
                case TouchPhase.Stationary:
                    touchStationaryEventChannel.Raise(touch.position);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    touchEndedEventChannel.Raise(touch.position);
                    break;
                default:
                    touchEndedEventChannel.Raise(touch.position);
                    break;
            }
        }

        private void OnJoystickMove(Vector2 dir) => _joystickDirection = dir;
    }
}