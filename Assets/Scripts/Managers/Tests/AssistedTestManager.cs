using Tones.Sessions;
using UnityEngine;

namespace Tones.Managers
{
    public class AssistedTestManager : TestManager
    {
        private float timeBetweenSessionsAssisted = 5;

        protected Sessions.Session preLimitFailedSession = null;
        protected Sessions.Session onLimitSucceedSession = null;
        protected Sessions.Session postLimitSucceedSession = null;

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Assisted Classic test Started.");
            currentSession = new Assisted(currentFrequencyIndex, currentVolume, timeBetweenSessionsAssisted, this, ear);
        }

        public override void SessionEnd(bool sessionSucceded)
        {
            throw new System.NotImplementedException();
        }
    }
}
