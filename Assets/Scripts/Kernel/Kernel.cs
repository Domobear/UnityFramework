using UnityEngine;

namespace Domo
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    public class Kernel : MonoBehaviour
    {
        public static Kernel Instance { get; private set; }

        public CoreStateConfig coreStateConfig;

        private TimerManager timerManager;
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

        private void Start()
        {
            coreStateManager.ChangeGlobalState(coreStateConfig.firstGlobalState);
            coreStateManager.ChangeState(coreStateConfig.firstState);
        }

        private void Update()
        {
            timerManager.OnUpdate();
            coreStateManager.OnUpdate();
        }

        private void CreateAllInstance()
        {
            ResourceManager.CreateInstance();
            TimerManager.CreateInstance();
            CoreStateManager.CreateInstance();

            timerManager = TimerManager.Instance;
            coreStateManager = CoreStateManager.Instance;
        }
    }
}