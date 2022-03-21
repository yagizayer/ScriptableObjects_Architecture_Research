using System.Collections.Generic;
using UnityEngine;
using Helpers;
using MyBox;

namespace Helpers.Components
{
    public class Follow : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField, MustBeAssigned] private Transform _target;
        [SerializeField] private bool _followWholePosition = true;
        [SerializeField] private bool _followRotation = false;


        [Header("Position Settings")]
        [SerializeField, ConditionalField(nameof(_followWholePosition), true)] private bool _followXPos = false;
        [SerializeField, ConditionalField(nameof(_followWholePosition), true)] private bool _followYPos = false;
        [SerializeField, ConditionalField(nameof(_followWholePosition), true)] private bool _followZPos = false;
        [SerializeField, ConditionalField(nameof(_followWholePosition))] private bool _autoDetectOffset = true;
        [SerializeField, ConditionalField(nameof(_autoDetectOffset), true)] private Vector3 _offset = Vector3.zero;

        private void Start()
        {
            if (_autoDetectOffset)
                _offset = transform.position - _target.position;
        }
        private void Update()
        {
            if (_target == null) return;
            if (_followWholePosition)
                transform.position = _target.position + _offset;
            else
            {
                if (_followXPos)
                    transform.position = transform.position.Modify(Vector3Values.X, _target.position.x + _offset.x);
                if (_followYPos)
                    transform.position = transform.position.Modify(Vector3Values.Y, _target.position.y + _offset.y);
                if (_followZPos)
                    transform.position = transform.position.Modify(Vector3Values.Z, _target.position.z + _offset.z);
            }
            if (_followRotation)
                transform.rotation = _target.rotation;
        }
    }
}