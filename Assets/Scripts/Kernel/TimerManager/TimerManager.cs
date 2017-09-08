using System;
using System.Collections.Generic;
using UnityEngine;

namespace Domo
{
    public class TimerManager : IUpdatable
    {
        public static TimerManager Instance { get; private set; }

        public static void CreateInstance()
        {
            if(Instance != null)
            {
                Instance = new TimerManager();
            }
        }

        private List<Timer> timerList;
        
        private TimerManager()
        {
            timerList = new List<Timer>(32);
        }

        public Timer New(float iInterval, Action iCallback)
        {
            Timer timer = new Timer(iInterval, iCallback);
            timerList.Add(timer);
            return timer;
        }

        public void OnUpdate()
        {
            for(int i=timerList.Count-1; i>=0; i--)
            {
                if(timerList[i].Tick(Time.deltaTime))
                {
                    timerList.RemoveAt(i);
                }
            }
        }
    }
}