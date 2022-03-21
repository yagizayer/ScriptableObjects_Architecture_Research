using Helpers;
using Helpers.MetaClasses;
using MyBox;
using ProjectRootFolder.Scripts.Managers;
using ScriptableObjects;
using UnityEngine;

namespace ProjectRootFolder.Scripts.Helpers
{
    public sealed class PoolLoader : MonoBehaviour
    {
        [SerializeField] private string poolName;
        [SerializeField] private ScriptableObject saveFile;

        private ISave<TransformValues> _saveFile;

        private void OnValidate()
        {
            if (!saveFile.ValidateInterface(typeof(ISave<TransformValues>))) saveFile = null;
            _saveFile = saveFile as ISave<TransformValues>;
        }

        private void Start() => Initialize();

        private void Initialize()
        {
            foreach (var savedData in _saveFile.Data)
                PoolManager.GetObject(poolName, obj =>
                {
                    obj.transform.ToLocalTransform(savedData); 
                    obj.transform.SetParent(transform);
                });
        }
        
        // debug
        [ButtonMethod]
        private void ResetChildren() => PoolManager.ResetPools();
    }
}