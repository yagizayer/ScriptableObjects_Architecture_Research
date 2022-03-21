using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Helpers;
using MyBox;

namespace Helpers.Components
{
	public sealed class LookAt : MonoBehaviour
	{
		[SerializeField] private Transform _target;
		private void Start() => Initialize();


		private void Initialize()
		{
			if(_target == null)_target = Camera.main.transform;
		}

		private void Update() {
			if (_target == null)
				return;

			transform.LookAt(_target);
		}

		[ButtonMethod]
		public void LookTarget() => transform.LookAt(_target);
	}
}
