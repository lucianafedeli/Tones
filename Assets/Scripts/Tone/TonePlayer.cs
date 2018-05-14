using UnityEngine;using System.Collections;using System.Collections.Generic;[RequireComponent(typeof(AudioSource))]public class TonePlayer : Singleton<TonePlayer>{    public enum Ear    {        Left = -1, Right = 1    }    public int sampleRate = 44100;    public int position = 0;    public Ear CurrentEar    {        get        {            return currentEar;        }        set        {            currentEar = value;            toneSource.panStereo = (int)currentEar;        }    }    public bool CurrentlyPlaying
    {
        get { return toneSource.isPlaying; }
    }    [SerializeField]    private AudioSource toneSource = null;    private AudioClip theSineClip;    private int currentFrequency = 0;    private Ear currentEar = Ear.Left;

    private Tone[] testTones = {
        new Tone(125,.1f),
        new Tone(125, .5f),
        new Tone(125, 1f),

        new Tone(250, .1f),
        new Tone(250, .5f),
        new Tone(250, 1f),

        new Tone(500, .1f),
        new Tone(500, .5f),
        new Tone(500, 1f),

        new Tone(1000, .1f),
        new Tone(1000, .5f),
        new Tone(1000, 1f),

        new Tone(2000, .1f),
        new Tone(2000, .5f),
        new Tone(2000, 1f),

        new Tone(4000, .1f),
        new Tone(4000, .5f),
        new Tone(4000, 1f),

        new Tone(8000, .1f),
        new Tone(8000, .5f),
        new Tone(8000, 1f),        };    private void Start()    {        if (null == toneSource)            toneSource = GetComponent<AudioSource>();        toneSource.panStereo = (int)currentEar;

        StartCoroutine(PlayTone());
    }    IEnumerator PlayTone()
    {
        while (true)
        {
            for (int i = 0; i < testTones.Length; i++)
            {
                PlayTone(testTones[i]);
                yield return new WaitForSeconds(4);
                StopTone();
                yield return new WaitForSeconds(2);
            }
        }
    }    public void PlayTone(Tone tone)    {        if (!CurrentlyPlaying)
        {
            TurnLightOn();

            currentFrequency = tone.Frequency;

            theSineClip = AudioClip.Create("CurrentTone", sampleRate * 2, 1, sampleRate, false, OnAudioRead);
            //AudioSettings.speakerMode = AudioSpeakerMode.Mono;
            toneSource.clip = theSineClip;

            toneSource.volume = tone.Volume;

            {
                int length = (int)theSineClip.length;

                float[] buffer = new float[128];

                theSineClip.GetData(buffer, 0);

                Debug.Log("dB: " + AudioPower.ComputeDB(buffer, 0, ref length));
            }

            toneSource.Play();
        }
    }    private void TurnLightOn()
    {
        // TODO
    }

    private void TurnLightOff()
    {
        // TODO
    }    public void StopTone()    {        TurnLightOff();        toneSource.Stop();    }    private void OnAudioRead(float[] data)    {        int count = 0;        while (count < data.Length)        {            data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * currentFrequency * position / sampleRate));            position++;            count++;        }    }    private void OnValidate()    {        if (null == toneSource)        {            toneSource = GetComponent<AudioSource>();        }    }    public void ToggleEar()    {        if (CurrentEar == Ear.Left)            CurrentEar = Ear.Right;        else            CurrentEar = Ear.Left;
        //return CurrentEar;
    }}