using System;
using UnityEngine;

namespace ProjectRootFolder.Scripts.MetaClasses
{
	public sealed class WallsBehaviour : MonoBehaviour
	{
		[SerializeField]private bool showWalls = true;
		[SerializeField,Range(1,50)]private float wallsSize = 10;

		[SerializeField] private Renderer[] wallRenderers;
		
		private void OnValidate()
		{
			transform.localScale = Vector3.one * wallsSize;
			if(wallRenderers == null || wallRenderers.Length == 0)
				wallRenderers = GetComponentsInChildren<Renderer>();

			foreach (var wallRenderer in wallRenderers)
				wallRenderer.enabled = showWalls;
		}
	}
}
