using Design_Patterns;
using Tones.Sessions;
using UnityEngine;

namespace Tones.Managers
{
    public class GraphManager : Singleton<GraphManager>
    {
        [SerializeField]
        private GameObject leftEarImagePrefab = null, rightEarImagePrefab = null;

        [SerializeField]
        private float imageSize = 25f;

        [SerializeField]
        private Transform imagesParent = null;

        [SerializeField]
        private float xPadding = 0, yPadding = 0, xHzIncrement = 0, yDbIncrement = 0;

        //public void AddSession(Session.Session session)
        //{
        //    float normalizeTo = session.TonePlayEvents.Pairs[0].Start;

        //    float toneMaxDuration = session.TonePlayEvents.GetLongestDuration();
        //    float pacientMaxDuration = session.PacientPushEvents.GetLongestDuration();
        //    float sessionDuration = 0;

        //    if (pacientMaxDuration > toneMaxDuration)
        //        sessionDuration = pacientMaxDuration;
        //    else
        //        sessionDuration = toneMaxDuration;

        //    Debug.Log("Session Duration: \t\t\t" + sessionDuration);
        //    Debug.Log("Tone Event: \t" + session.TonePlayEvents.Normalized(normalizeTo).ToString());
        //    Debug.Log("Pacient push Event: \t" + session.PacientPushEvents.Normalized(normalizeTo).ToString());

        //}

        private void Start()
        {
            Session testSession = new Manual(0, .1f, null, Tone.EarSide.Right);
            GraphSession(testSession);

            testSession = new Manual(1, .2f, null, Tone.EarSide.Left);
            GraphSession(testSession);

            testSession = new Manual(2, .3f, null, Tone.EarSide.Right);
            GraphSession(testSession);

            testSession = new Manual(3, .4f, null, Tone.EarSide.Left);
            GraphSession(testSession);

            testSession = new Manual(4, .5f, null, Tone.EarSide.Right);
            GraphSession(testSession);

            testSession = new Manual(5, .6f, null, Tone.EarSide.Left);
            GraphSession(testSession);

            testSession = new Manual(6, .7f, null, Tone.EarSide.Right);
            GraphSession(testSession);
        }

        public void GraphSession(Session session)
        {
            GameObject imageInstance = session.IsLeftEar ?
                                        Instantiate(leftEarImagePrefab) :
                                        Instantiate(rightEarImagePrefab);


            //imageInstance.transform.localPosition = new Vector3(0, 0);

            imageInstance.transform.localPosition = new Vector3(
                                        xPadding + (xHzIncrement * (session.Tone.FrequencyIndex + 1)) - imageSize / 2,    //x
                                        -yPadding - yDbIncrement * session.Tone.Volume * 15 + imageSize * .5f);             //y

            imageInstance.transform.SetParent(imagesParent, false);
        }

    }
}
