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

        private readonly float xPadding = 21.43f, yPadding = 42.5f, xHzIncrement = 71.5f, yDbIncrement = 3.75f;

        [SerializeField]
        private Transform[] CarharttParents;

        [SerializeField]
        private GameObject CarharttLeftPrefab;

        [SerializeField]
        private GameObject CarharttRightPrefab;


        [SerializeField]
        private Button carharttTabButton;

        private void Start()
        {
            DataManager dm = DataManager.Instance;

            carharttTabButton.interactable = false;

            if (null != dm.GetPacientsData()[dm.CurrentPacient.ID].carhartts)
            {
                carharttTabButton.interactable = true;
                foreach (var carhartt in dm.GetPacientsData()[dm.CurrentPacient.ID].carhartts)
                {
                    GraphCarhartt(carhartt);
                }
            }

            if (null != dm.GetPacientsData()[dm.CurrentPacient.ID].lastSessions)
            {
                foreach (var session in dm.GetPacientsData()[dm.CurrentPacient.ID].lastSessions)
                {
                    GraphSession(session);
                }
            }
        }

        private void GraphSession(Session session)
        {
            GameObject imageInstance = session.tone.Ear == Tone.EarSide.Right ?
                                        Instantiate(rightEarImagePrefab) :
                                        Instantiate(leftEarImagePrefab);

            imageInstance.transform.localPosition = new Vector3(
                                        xPadding + (xHzIncrement * (session.tone.FrequencyIndex + 1)) - imageSize / 2,    //x
                                        -yPadding - (yDbIncrement * session.tone.dB) + imageSize / 2);             //y

            imageInstance.transform.SetParent(imagesParent, false);
        }

        private void GraphCarhartt(Carhartt session)
        {
            var parent = CarharttParents[session.tone.FrequencyIndex - 2];

            var toneEvent = session.tonePlayEvents.pairs[0];
            var pacientPushEvents = session.pacientPushEvents.pairs;

            parent.GetChild(0).GetComponent<Text>().text = ((int)session.tonePlayEvents.GetLongestDuration()).ToString();

            foreach (var pressedEvent in pacientPushEvents)
            {
                if (pressedEvent.Duration() > 1)
                {
                    GameObject instantiatedLine = null;
                    if (session.tone.Ear == Tone.EarSide.Left)
                    {
                        instantiatedLine = Instantiate(CarharttLeftPrefab, parent);
                    }
                    else
                    {
                        instantiatedLine = Instantiate(CarharttRightPrefab, parent);
                    }

                    LineRenderer renderer = instantiatedLine.transform.GetChild(0).GetComponent<LineRenderer>();

                    float width = renderer.gameObject.GetComponent<RectTransform>().sizeDelta.x;
                    if (session.tone.Ear == Tone.EarSide.Left)
                    {
                        renderer.SetPosition(0, new Vector3((pressedEvent.start - toneEvent.start) * width / 60, 6, 0));
                        renderer.SetPosition(1, new Vector3((pressedEvent.end - toneEvent.start) * width / 60, 6, 0));

                        instantiatedLine.transform.GetChild(1).localPosition = new Vector3(
                            (pressedEvent.end - toneEvent.start) * width / 60 - 10, 18, 0);
                    }
                    else
                    {
                        renderer.SetPosition(0, new Vector3((pressedEvent.start - toneEvent.start) * width / 60, -8, 0));
                        renderer.SetPosition(1, new Vector3((pressedEvent.end - toneEvent.start) * width / 60, -8, 0));

                        instantiatedLine.transform.GetChild(1).localPosition = new Vector3(
                            (pressedEvent.end - toneEvent.start) * width / 60 - 10, -20, 0);
                    }

                    instantiatedLine.transform.GetChild(1).GetComponent<Text>().text = pressedEvent.Duration().ToString("0.0");
                }
            }
        }
    }
}
