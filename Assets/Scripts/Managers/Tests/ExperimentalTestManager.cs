using Tones.Sessions;
using UnityEngine;

namespace Tones.Managers
{
    public class ExperimentalTestManager : TestManager
    {
        public override void SessionEnd(bool sessionSucceded)
        {
            throw new System.NotImplementedException();
        }

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Experimental test Started.");
            currentSession = new Experimental(currentFrequencyIndex, currentVolume, this, ear);
        }
    }
}
