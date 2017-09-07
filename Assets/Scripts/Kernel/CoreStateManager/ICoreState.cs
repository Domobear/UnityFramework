namespace Domo
{
    public abstract class ICoreState : IActivatable, IUpdatable
    {
        public abstract void OnEnable();
        public abstract void OnDisable();

        public abstract void OnUpdate();
    }

    public enum ECoreState
    {
        Idle
    }
}