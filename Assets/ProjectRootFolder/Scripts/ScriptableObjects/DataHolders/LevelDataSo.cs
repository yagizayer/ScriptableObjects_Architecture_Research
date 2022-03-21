using ScriptableObjects;
using UnityEngine;

namespace Nano_ZombieBiker_01.Scripts.ScriptableObjects.DataHolders
{
	[CreateAssetMenu(fileName = "New LevelDataSo", menuName = "ScriptableObjects/LevelDataSo")]
	public class LevelDataSo : ScriptableObject, IPassableData
	{
		public string targetSceneName;
		
		public bool Equals(IPassableData other)
		{
			return other is LevelDataSo levelData &&
			       targetSceneName == levelData.targetSceneName;
		}
	}
}
