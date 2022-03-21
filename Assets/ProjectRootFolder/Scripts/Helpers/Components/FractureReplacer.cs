using System;
using System.Collections.Generic;
using Helpers;
using Helpers.MetaClasses;
using MyBox;
using UnityEngine;

namespace PirateDefence.Scripts.Helpers.Components
{
    public class FractureReplacer : MonoBehaviour
    {
        [SerializeField, MustBeAssigned] private GameObject _wholeObject;
        [SerializeField, MustBeAssigned] private FracturedParent _fracturedParent;


        internal void ResetObject() => Initialize();
        internal void ExplodeObject() => Explode();

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            _fracturedParent.ResetChildren();
            _fracturedParent.gameObject.SetActive(false);
            _wholeObject.SetActive(true);
        }

        private void Explode()
        {
            _wholeObject.SetActive(false);
            _fracturedParent.gameObject.SetActive(true);
            _fracturedParent.Explode();
        }
    }
}