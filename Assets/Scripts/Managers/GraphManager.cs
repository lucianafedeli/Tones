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
        }

        private void GraphSession(Session session)
        {
            GameObject imageInstance = session.tone.Ear == Tone.EarSide.Left ?
                                        Instantiate(leftEarImagePrefab) :
                                        Instantiate(rightEarImagePrefab);

            //imageInstance.transform.localPosition = new Vector3(0, 0);

            imageInstance.transform.localPosition = new Vector3(
                                        xPadding + (xHzIncrement * (session.tone.FrequencyIndex + 1)) - imageSize / 2,    //x
                                        -yPadding - yDbIncrement * session.tone.Volume * 15 + imageSize * .5f);             //y

            imageInstance.transform.SetParent(imagesParent, false);
        }

        private void GraphCarhartt(Carhartt session)
        {
            var parent = CarharttParents[session.tone.FrequencyIndex - 3];

            var events = session.pacientPushEvents.pairs;

            parent.GetChild(0).GetComponent<Text>().text = ((int)session.tonePlayEvents.GetLongestDuration()).ToString();

            foreach (var pressedEvent in events)
            {
                if (pressedEvent.Duration() > 1)
                {
                    GameObject line = Instantiate(CarharttLinePrefab, parent);
                    LineRenderer renderer = line.transform.GetChild(0).GetComponent<LineRenderer>();

                    float width = renderer.gameObject.GetComponent<RectTransform>().sizeDelta.x;

                    renderer.SetPosition(0, new Vector3(pressedEvent.start * width / 60, 5, 0));
                    renderer.SetPosition(1, new Vector3(pressedEvent.end * width / 60, 5, 0));

                    line.transform.GetChild(1).localPosition = new Vector3((pressedEvent.end * width / 60) - 10, -10, 0);
                    line.transform.GetChild(1).GetComponent<Text>().text = pressedEvent.Duration().ToString("0.0");

                    line.transform.localPosition = new Vector3(-140 * 3, 0, 0);
                }
            }
        }
    }
}
