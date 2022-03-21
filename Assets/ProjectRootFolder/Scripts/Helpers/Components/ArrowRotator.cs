using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Helpers;
using Helpers.MetaClasses;
using MyBox;
using PirateDefence.Scripts.Helpers;
using UnityEngine.UI;

namespace Helpers.Components
{
    [RequireComponent(typeof(RectTransform))]
    public class ArrowRotator : MonoBehaviour
    {
        [MustBeAssigned] public Transform target;
        [SerializeField, MustBeAssigned] private Image pointerArrow;
        [SerializeField, MustBeAssigned] private Camera topDownCamera;
        public bool Enabled;

        [SerializeField, Range(0f, 100f)] private float distanceFromCenter = 50f;
        [SerializeField] private Vector2 centerOffset = new Vector2(0f, 0f);

        private Transform _arrowTransform;
        private readonly Vector2 _center = new Vector2((float) Screen.width / 2, (float) Screen.height / 2);
        private readonly Vector3 _rotationOffset = new Vector3(90f, 0f, -90f);

        private void Awake()
        {
            _arrowTransform = pointerArrow.transform;
            // if (!topDownCamera.transform.TopDown())
                // Debug.LogWarning("TopDownCamera is not set to TopDown mode");
        }

        private void Update()
        {
            pointerArrow.enabled = Enabled;
            if (Enabled) LookTarget();
        }

        private void LookTarget()
        {
            var scaledDistance = distanceFromCenter.Remap(0, 100, 0, 1080);
            var targetPosition = target.position;

            // screen position of the arrow
            var targetScreenPoint = (Vector2) topDownCamera.WorldToScreenPoint(targetPosition);
            var direction = (targetScreenPoint - _center).normalized;
            ((RectTransform) _arrowTransform).anchoredPosition = direction * scaledDistance + centerOffset;

            // rotation of the arrow
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ((RectTransform) _arrowTransform).localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
