using UnityEngine;

namespace Domo
{
    /// <summary>
    /// Wrap resource related classes of Unity3D.
    /// </summary>
    public class ResourceManager
    {
        public static ResourceManager Instance { get; private set; }

        public static void CreateInstance()
        {
            if(Instance != null)
            {
                Instance = new ResourceManager();
            }
        }

        private ResourceManager() { }

        public T Load<T>(string iPath) where T : Object
        {
            // TODO:Cache item.
            return Resources.Load<T>(iPath);
        }
    }
}