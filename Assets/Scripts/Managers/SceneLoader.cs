using ScriptableObjects;
using ScriptableObjects.DataHolders;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]private SceneValuesSo currentSceneValues;

        private void Awake() => SceneManager.LoadSceneAsync(currentSceneValues.sceneName, LoadSceneMode.Additive);

        public void RefreshScene()
        {
            SceneManager.UnloadSceneAsync(currentSceneValues.sceneName);
            SceneManager.LoadSceneAsync(currentSceneValues.sceneName, LoadSceneMode.Additive);
        }

        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

        public void LoadScene(IPassableData scene)
        {
            var sceneValues = scene as SceneValuesSo;
            SceneManager.UnloadSceneAsync(currentSceneValues.sceneName);
            SceneManager.LoadSceneAsync(sceneValues!.sceneName, LoadSceneMode.Additive);
            currentSceneValues = sceneValues;
        }
        
        public void Test() => Debug.Log("test");
        
    }
}