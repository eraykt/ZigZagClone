using UnityEngine;

namespace kechigames.Singleton
{
    public abstract class Persistent<T> : MonoBehaviour where T : Persistent<T>
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}