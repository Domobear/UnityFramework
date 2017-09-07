using UnityEngine;

namespace Domo
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    public class Kernel : MonoBehaviour
    {
        public static Kernel Instance { get; private set; }

        private CoreStateManager coreStateManager;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Instance = this;

            CreateAllInstance();
        }

        private void OnEnable()
        {
            // TODO:Set all kernel.
        }

        private void Start()
        {
            coreStateManager = CoreStateManager.Instance;
        }

        private void Update()
        {
            coreStateManager.OnUpdate();
        }

        private void CreateAllInstance()
        {
            ResourceManager.CreateInstance();
            CoreStateManager.CreateInstance();
        }
    }
}