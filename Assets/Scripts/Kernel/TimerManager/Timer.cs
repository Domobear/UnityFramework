using System;

namespace Domo
{
    public class Timer
    {
        public float Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        public float ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

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