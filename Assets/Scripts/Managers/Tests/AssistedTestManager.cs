using Tones.Session;
using UnityEngine;

namespace Tones.Managers
{
    public class AssistedTestManager : TestManager
    {

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Assisted Classic test Started.");
            currentSession = new Assisted(CurrentFrequency, currentVolume, timeBetweenSessionsAssisted, this);
        }
    }
}
