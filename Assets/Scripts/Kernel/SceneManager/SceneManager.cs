namespace Domo
{
    public class SceneManager
    {
        public IScene GlobalScene { get; private set; }
        public IScene CurrentScene { get; private set; }
    }
}