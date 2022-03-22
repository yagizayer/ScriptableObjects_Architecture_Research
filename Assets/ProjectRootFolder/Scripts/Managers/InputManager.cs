using System;
using Helpers;
using ProjectRootFolder.Scripts.ScriptableObjects.DataHolders;
using ScriptableObjects;
using UnityEngine;

namespace ProjectRootFolder.Scripts.Managers
{
    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private ScriptableObject floatingJoystick;
        private FloatingJoystickHandler _floatingJoystick;
        
        private void OnValidate()
        {
            if(!floatingJoystick.ValidateInterface(typeof(IInputHandler)))return;
            _floatingJoystick = (FloatingJoystickHandler) floatingJoystick;
        }
        
        private void OnEnable()
        {
            _floatingJoystick.EnableInput();
        }
        
        private void Update() => _floatingJoystick.Update();
    }
}