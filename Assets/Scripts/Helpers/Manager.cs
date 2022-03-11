using UnityEngine;

namespace Helpers
{
    public class Manager<T> : MonoBehaviour
    {
        public static T Instance { get; private set; }
        public static bool IsInitialized { get; private set; }

        protected virtual void Singleton(T classType)
        {
            if (Instance == null)
            {
                Instance = classType;
                IsInitialized = true;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
