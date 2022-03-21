using System;
using Helpers;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

namespace ProjectRootFolder.Scripts.Managers
{
    [Serializable]
    public class PoolsDictionary : SerializableDictionaryBase<string, ObjectPool>
    {
    }

    public sealed class PoolManager : Singleton<PoolManager>
    {
        [SerializeField] private PoolsDictionary pools = new PoolsDictionary();

        private void Awake() => Initialize();


        private void Initialize()
        {
            CreateInstance(this);
            foreach (var kvPair in pools)
            {
                kvPair.Value.Initialize();
            }
        }

        public static GameObject GetObject(string poolName, Action<GameObject> action = null)
        {
            if (!Instance.pools.ContainsKey(poolName))
                throw new ArgumentException($"Pool with name {poolName} not found");
            var obj = Instance.pools[poolName].Get(action);
            return obj;
        }
        
        public static void ResetPools()
        {
            foreach (var kvPair in Instance.pools)
                kvPair.Value.ResetDequeued();
        }
    }
}