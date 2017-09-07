using System;
using System.Collections.Generic;
using UnityEngine;

namespace Domo
{
    public class CoreStateManager : IUpdatable
    {
        public static CoreStateManager Instance { get; private set; }

        public static void CreateInstance()
        {
            if(Instance != null)
            {
                Instance = new CoreStateManager();
            }
        }

        private CoreStateManager() { }

        private ICoreState globalState;
        private ICoreState currentState;

        //private IState lastState;
        private ICoreState nextState;

        public void OnUpdate()
        {
            globalState.OnUpdate();
            currentState.OnUpdate();

            if(nextState == null)
            {
                return;
            }

            if(currentState != null)
            {
                currentState.OnDisable();
            }

            //lastState = currentState;
            currentState = nextState;
            nextState = null;

            currentState.OnEnable();
        }

        public void ChangeState()
        {

        }

        private void CreateAllState()
        {
            Type baseType = typeof(ICoreState);
            Type[] types = GetType().Assembly.GetTypes();
            foreach(Type type in types)
            {
                if(type.BaseType != baseType)
                {
                    continue;
                }

                ICoreState state = Activator.CreateInstance(type) as ICoreState;
                CoreStateAttribute attr = Attribute.GetCustomAttribute(type, typeof(CoreStateAttribute)) as CoreStateAttribute;

                //AddState(attr, state);
            }
        }
    }
}