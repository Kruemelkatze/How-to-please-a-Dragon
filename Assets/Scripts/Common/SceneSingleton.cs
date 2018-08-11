namespace UnityEngine
{
    public class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>

    {
        private static T _instance;
        public static T Instance => _instance;

        void Awake()
        {
            _instance = GetComponent<T>();
        }
    }
}