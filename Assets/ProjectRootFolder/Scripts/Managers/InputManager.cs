using System;
using Nano_ZombieBiker_01.Scripts.ScriptableObjects.DataHolders;
using ProjectRootFolder.Scripts.ScriptableObjects.DataHolders;
using UnityEngine;

namespace ProjectRootFolder.Scripts.Managers
{
    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private InputHandlerSo inputHandlerSo;

        private void OnEnable()
        {
            inputHandlerSo.EnableAllInput();
        }

        private void FixedUpdate() => inputHandlerSo.Update();
    }
}