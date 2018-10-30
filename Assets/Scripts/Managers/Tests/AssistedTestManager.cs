using Tones.Sessions;
using UnityEngine;

namespace Tones.Managers
{
    public class AssistedTestManager : TestManager
    {
        private float timeBetweenSessionsAssisted = 5;

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Assisted Classic test Started.");
            currentSession = new Assisted(toneManager.freqIndex, toneManager.currentDB, timeBetweenSessionsAssisted, this, ear);
        }

        public override void SessionEnd(bool sessionSucceded)
        {
            if (sessionSucceded)
            {

            }
            else
            {

            }
        }
    }
}
