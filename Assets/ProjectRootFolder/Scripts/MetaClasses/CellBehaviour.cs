using System;
using System.Collections.Generic;
using Helpers;
using MyBox;
using UnityEngine;

namespace ProjectRootFolder.Scripts.MetaClasses
{
    public sealed class CellBehaviour : MonoBehaviour
    {
        [SerializeField, Range(0.01f, 10)] private float divisionInterval = 5;

        private bool _isDivisible = false;

        private List<Renderer> _renderers;
        private Rigidbody _rigidbody;

        //--------------------------------------------------------------------------------------------------------------
        
        private void Start() => Initialize();

        private void FixedUpdate()
        {
            SlowDown();
        }

        //--------------------------------------------------------------------------------------------------------------
        
        [ButtonMethod]
        private void Divide()
        {
            _rigidbody.ExplodeRandomly(1, false);
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
        private void Initialize()
        {
            _renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void SlowDown()
        {
            if (_rigidbody.velocity.magnitude < 0.1f)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
            else
                _rigidbody.velocity *= .99f;
        }
    }
}