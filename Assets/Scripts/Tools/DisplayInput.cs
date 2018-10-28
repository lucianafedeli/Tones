using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayInput : MonoBehaviour
{
    [SerializeField]
    private Image pulsador;

    [SerializeField]
    private Text displayOn;

    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        displayOn.text = "";

        StartCoroutine(RecordMic());

    }
    private void Update()
    {
        foreach (var joystick in Input.GetJoystickNames())
        {
            displayOn.text += joystick;
        }
    }

    private IEnumerator RecordMic()
    {
        AudioClip recording;
        float recordingDuration = .1f;
        float[] samples;

        while (true)
        {
            recording = Microphone.Start(null, true, 100, 44100);
            pulsador.color = Color.black;
            yield return new WaitForSeconds(recordingDuration);
            Microphone.End(null);

            samples = new float[recording.samples * recording.channels];
            recording.GetData(samples, 0);

            for (int i = 0; i < samples.Length; ++i)
            {
                if (samples[i] > .9f)
                {
                    pulsador.color = Color.red;
                    break;
                }
            }

            yield return new WaitForSeconds(recordingDuration);

            if (Time.realtimeSinceStartup > 100)
            {
                break;
            }
        }
    }
}
