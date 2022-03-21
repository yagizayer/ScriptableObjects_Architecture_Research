using Nano_ZombieBiker_01.Scripts.ScriptableObjects.DataHolders;
using ScriptableObjects.Events.Channels;
using UnityEngine;

namespace Nano_ZombieBiker_01.Scripts.MetaClasses
{
    public sealed class ChangeScene : MonoBehaviour
    {
        [SerializeField] private LevelDataSo targetLevelData;
        [SerializeField] private PassableDataEventChannel sceneChangedEventChannel;

        public void ChangeSceneRaise() => sceneChangedEventChannel.Raise(targetLevelData);
    }
}