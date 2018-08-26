using Design_Patterns;
using UnityEngine;

namespace Tones.Session
{
    [RequireComponent(typeof(AudioSource))]
    public class TonePlayer : Singleton<TonePlayer>
    {
        public enum Ear
        {
            Left = -1, Right = 1
        }

        public int sampleRate = 44100;

        public int position = 0;

        public Ear CurrentEar
        {
            get
            {
                return currentEar;
            }
            set
            {
                currentEar = value;
                toneSource.panStereo = (int)currentEar;
            }
        }

        public bool CurrentlyPlaying
        {
            get { return toneSource.isPlaying; }
        }

        [SerializeField]
        private AudioSource toneSource = null;

        private AudioClip theSineClip;

        private int currentFrequency = 0;

        private Ear currentEar = Ear.Left;

        private void Start()
        {
            if (null == toneSource)
                toneSource = GetComponent<AudioSource>();

            toneSource.panStereo = (int)currentEar;
        }

        public void PlayTone(Tone tone)
        {
            if (!CurrentlyPlaying)
            {

                currentFrequency = tone.Frequency;

                theSineClip = AudioClip.Create("CurrentTone", sampleRate * 2, 1, sampleRate, false, OnAudioRead);
                //AudioSettings.speakerMode = AudioSpeakerMode.Mono;
                toneSource.clip = theSineClip;

                toneSource.volume = tone.Volume;

                {
                    int length = (int)theSineClip.length;

                    float[] buffer = new float[128];

                    theSineClip.GetData(buffer, 0);
                }

                toneSource.Play();
            }
        }

        public void StopTone()
        {
            toneSource.Stop();
        }

        private void OnAudioRead(float[] data)
        {
            int count = 0;
            while (count < data.Length)
            {
                data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * currentFrequency * position / sampleRate));

                position++;
                count++;
            }
        }

        private void OnValidate()
        {
            if (null == toneSource)
            {
                toneSource = GetComponent<AudioSource>();
            }
        }

        public void ToggleEar()
        {
            if (CurrentEar == Ear.Left)
                CurrentEar = Ear.Right;
            else
                CurrentEar = Ear.Left;
            //return CurrentEar;
        }
    }
}
