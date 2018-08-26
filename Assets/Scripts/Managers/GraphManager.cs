using Design_Patterns;
using UnityEngine;

namespace Tones.Managers
{
    public class GraphManager : Singleton<GraphManager>
    {
        public void AddSession(Session.Session session)
        {
            float normalizeTo = session.TonePlayEvents.Pairs[0].Start;

            float toneMaxDuration = session.TonePlayEvents.GetLongestDuration();
            float pacientMaxDuration = session.PacientPushEvents.GetLongestDuration();
            float sessionDuration = 0;

            if (pacientMaxDuration > toneMaxDuration)
                sessionDuration = pacientMaxDuration;
            else
                sessionDuration = toneMaxDuration;

            Debug.Log("Session Duration: \t\t\t" + sessionDuration);
            Debug.Log("Tone Event: \t" + session.TonePlayEvents.Normalized(normalizeTo).ToString());
            Debug.Log("Pacient push Event: \t" + session.PacientPushEvents.Normalized(normalizeTo).ToString());

        }
    }
}
