using Tones.Session;
using UnityEngine;

namespace Tones.Managers
{
    public class ExperimentalTestManager : TestManager
    {

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Experimental test Started.");
            currentSession = new Experimental(CurrentFrequency, currentVolume, this, isLeftEar);
        }
    }
}
