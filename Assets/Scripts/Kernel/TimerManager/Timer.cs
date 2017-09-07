using System;

namespace Domo
{
    public class Timer
    {
        private Action callback;
        
        private float interval;
        private float elapsedTime;
        
        public Timer(float iInterval, Action iCallback)
        {
            interval = iInterval;
            callback = iCallback;
        }
        
        public bool Tick(float iMsec)
        {
            elapsedTime += iMsec;
            if(elapsedTime > interval)
            {
                if(callback != null)
                {
                    callback();
                }
        
                return true;
            }
        
            return false;
        }
    }
}