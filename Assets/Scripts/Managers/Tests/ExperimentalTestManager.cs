using Tones.Sessions;
using UnityEngine;

namespace Tones.Managers
{
    public class ExperimentalTestManager : TestManager
    {
        protected Session preLimitFailedSession = null;
        protected Session onLimitSucceedSession = null;
        protected Session postLimitSucceedSession = null;


        public override void SessionEnd(bool sessionSucceded)
        {
            throw new System.NotImplementedException();
        }

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Experimental test Started.");
            currentSession = new Experimental(toneManager.freqIndex, toneManager.currentDB, this, ear);
        }
    }
}
