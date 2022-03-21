using Nano_ZombieBiker_01.Scripts.ScriptableObjects.DataHolders;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectRootFolder.Scripts.Managers
{
    public sealed class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelDataSo startLevelData;

        public LevelDataSo CurrentLevelData { get; private set; }


        private void Start()
        {
            CurrentLevelData = startLevelData;
            LoadScene(startLevelData);
        }

        public void ChangeScene(IPassableData data)
        {
            var levelData = data as LevelDataSo;

            if (levelData!.Equals(CurrentLevelData)) return;

            UnloadScene(CurrentLevelData);
            LoadScene(levelData);
        }

        private void LoadScene(LevelDataSo newLevel)
        {
            var load = !SceneManager.GetSceneByName(CurrentLevelData.targetSceneName).isLoaded;
            if (load)
                SceneManager.LoadSceneAsync(newLevel.targetSceneName, LoadSceneMode.Additive);
            
            CurrentLevelData = newLevel;
        }

        private void UnloadScene(LevelDataSo oldLevel) => SceneManager.UnloadSceneAsync(oldLevel.targetSceneName);
    }
}