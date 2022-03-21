using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Helpers.MetaClasses;
using MyBox;
using UnityEngine;

namespace PirateDefence.Scripts.Helpers.Components
{
    public class FracturedParent : MonoBehaviour
    {
        [SerializeField] private bool initializeOnAwake = true;

        [ConditionalField(nameof(initializeOnAwake), true), SerializeField]
        private bool initializeOnStart = false;

        [ConditionalField(nameof(initializeOnAwake), true), SerializeField]
        private bool initializeOnFirstUpdate = false;

        [ConditionalField(nameof(initializeOnAwake), true), SerializeField]
        private bool initializeManually = false;

        private List<MeshFilter> _childFilters = new List<MeshFilter>();
        private readonly List<Rigidbody> _childRigidbodies = new List<Rigidbody>();

        private readonly Dictionary<Transform, TransformValues> _childTransforms =
            new Dictionary<Transform, TransformValues>();


        //--------------------------------------------------------------------------------------------------------------
        // Helper Methods

        internal void InitializeManually() => Initialize();

        internal void ResetChildren()
        {
            ResetTransforms();
            ResetRigidbodies();
        }

        internal void Explode() =>
            _childRigidbodies.ForEach(rb =>
            {
                rb.gameObject.SetActive(true);
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.ExplodeRandomly(50);
            });

        //--------------------------------------------------------------------------------------------------------------
        // Unity Methods

        private void Awake()
        {
            if (!initializeOnAwake) return;
            Initialize();
        }

        private void Start()
        {
            if (!initializeOnStart) return;
            Initialize();
        }

        private void Update()
        {
            if (!initializeOnFirstUpdate) return;
            Initialize();
        }

        //--------------------------------------------------------------------------------------------------------------
        // Private Methods

        private void Initialize()
        {
            // work once
            if (_childFilters.Count != 0) return;

            _childFilters = new List<MeshFilter>(GetComponentsInChildren<MeshFilter>());
            _childFilters.Remove(GetComponent<MeshFilter>());
            foreach (var child in _childFilters)
            {
                child.gameObject.SetActive(true);
                var childTransform = child.transform;

                if (!child.gameObject.HasComponent<Rigidbody>(out var childRigidBody))
                {
                    childRigidBody.isKinematic = true;
                    childRigidBody.useGravity = false;
                }

                if (!child.gameObject.HasComponent<MeshCollider>(out var childCollider))
                {
                    childCollider.isTrigger = true;
                    childCollider.convex = true;
                    childCollider.sharedMesh = child.sharedMesh;
                }

                _childRigidbodies.Add(childRigidBody);
                _childTransforms.Add(childTransform, new TransformValues(childTransform));

                child.gameObject.SetActive(false);
            }

            StartCoroutine(CheckFallingCo());
        }


        private void ResetTransforms() =>
            _childTransforms.ForEach(kv =>
            {
                kv.Key.ToLocalTransform(kv.Value);
                kv.Key.gameObject.SetActive(false);
            });

        private void ResetRigidbodies() =>
            _childRigidbodies.ForEach(rb =>
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
            });

        private IEnumerator CheckFallingCo()
        {
            var count = 0;
            const int checkIntervalInSeconds = 1;
            while (count < _childTransforms.Count)
            {
                yield return new WaitForSeconds(checkIntervalInSeconds);
                foreach (var kv in _childTransforms)
                {
                    if(kv.Key.gameObject.activeSelf) continue;
                    var falling = kv.Key.position.y >= -20;
                    kv.Key.gameObject.SetActive(falling);
                    count += falling ? 1 : 0;
                }
            }
        }
    }
}