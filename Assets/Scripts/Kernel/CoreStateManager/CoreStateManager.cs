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

        public int LastStateID
        {
            get
            {
                CoreStateAttribute attr = Attribute.GetCustomAttribute(lastState.GetType(), typeof(CoreStateAttribute)) as CoreStateAttribute;
                return attr.id;
            }
        }

        private Dictionary<int, ICoreState> stateDic;

        private ICoreState globalState;
        private ICoreState currentState;

        private ICoreState lastState;
        private ICoreState nextState;

        private CoreStateManager()
        {
            stateDic = new Dictionary<int, ICoreState>(8);
        }

        public void Initialize()
        {
            globalState = new IdleCoreState();
            currentState = new IdleCoreState();

            lastState = new IdleCoreState();
        }

        public void OnUpdate()
        {
            globalState.OnUpdate();
            currentState.OnUpdate();

            if(nextState == null)
            {
                return;
            }

            currentState.OnDisable();

            lastState = currentState;
            currentState = nextState;
            nextState = null;

            currentState.OnEnable();
        }

        public T GetState<T>() where T : ICoreState
        {
            CoreStateAttribute attr = Attribute.GetCustomAttribute(typeof(T), typeof(CoreStateAttribute)) as CoreStateAttribute;

            if(attr == null)
            {
                return null;
            }

            if(stateDic.ContainsKey(attr.id))
            {
                return stateDic[attr.id] as T;
            }

            return null;
        }

        public void ChangeGlobalState(int iStateID)
        {
            if(stateDic.ContainsKey(iStateID))
            {
                globalState.OnDisable();
                globalState = stateDic[iStateID];
                globalState.OnEnable();
                return;
            }

            Debug.LogErrorFormat("[CoreStatemanager] ICoreState isn't exsist.", iStateID);
        }

        public void ChangeState(int iStateID)
        {
            if(nextState != null)
            {
                Debug.LogFormat("[Core] NextState already exsist: {0}", nextState.ToString());
                return;
            }

            stateDic.TryGetValue(iStateID, out nextState);
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

                AddState(type);
            }
        }

        private void AddState(Type iType)
        {
            ICoreState state = Activator.CreateInstance(iType) as ICoreState;
            if(state == null)
            {
                return;
            }

            CoreStateAttribute attr = Attribute.GetCustomAttribute(iType, typeof(CoreStateAttribute)) as CoreStateAttribute;
            if(attr == null)
            {
                Debug.LogErrorFormat("[CoreStatemanager] ICoreState: {0} has no CoreStateAttribute.", state.GetType().FullName);
                return;
            }

            if(stateDic.ContainsKey(attr.id))
            {
                Debug.LogErrorFormat("[CoreStatemanager] ICoreState: {0} is duplicated.", attr.id);
                return;
            }

            stateDic.Add(attr.id, state);
        }
    }
}