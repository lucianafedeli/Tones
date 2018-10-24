///—————————————————————–
///   File: GraphManager.cs
///   Author: Luciano Donati
///   me@lucianodonati.com	www.lucianodonati.com
///   Last edit: 24-Oct-18
///   Description: 
///—————————————————————–

using Design_Patterns;
using Managers;
using Tones.Sessions;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField]
        private Transform[] CarharttParents;

        [SerializeField]
        private GameObject CarharttLinePrefab;

        private void Start()
        {
            DataManager dm = DataManager.Instance;
            foreach (var carhartt in dm.PacientsData[dm.CurrentPacient.ID].carhartts)
            {
                GraphCarhartt(carhartt);
            }
        }

        private void GraphSession(Session session)
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

        private void GraphCarhartt(Carhartt session)
        {
            var parent = CarharttParents[session.Tone.FrequencyIndex - 2];

            var events = session.PacientPushEvents.Pairs;

            parent.GetChild(0).GetComponent<Text>().text = ((int)session.TonePlayEvents.GetLongestDuration()).ToString();

            foreach (var pressedEvent in events)
            {
                GameObject line = Instantiate(CarharttLinePrefab, parent);
                LineRenderer renderer = line.transform.GetChild(0).GetComponent<LineRenderer>();

                float width = renderer.gameObject.GetComponent<RectTransform>().sizeDelta.x;

                renderer.SetPosition(0, new Vector3(pressedEvent.Start * width / 60, 5, 0));
                renderer.SetPosition(1, new Vector3(pressedEvent.End * width / 60, 5, 0));

                line.transform.GetChild(1).GetComponent<Text>().text = ((int)pressedEvent.Duration()).ToString();
            }
        }
    }
}
