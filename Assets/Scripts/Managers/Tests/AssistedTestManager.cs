using Tones.Session;
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
            currentSession = new Assisted(CurrentFrequency, currentVolume, timeBetweenSessionsAssisted, this, isLeftEar);
        }
    }
}
