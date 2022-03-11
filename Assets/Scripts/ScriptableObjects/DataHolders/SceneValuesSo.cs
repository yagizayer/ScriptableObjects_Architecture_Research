using UnityEngine;

namespace ScriptableObjects.DataHolders
{
	[CreateAssetMenu(fileName = "New SceneValuesSo", menuName = "DataHolders/SceneValuesSo")]
	public class SceneValuesSo : ScriptableObject, IPassableData
	{
		public string sceneName;
		public int sceneIndex;
	}
}
