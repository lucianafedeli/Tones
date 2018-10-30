using Design_Patterns;
using Tones.Managers;
using UnityEngine;

namespace Tones.Sessions
{
    [RequireComponent(typeof(AudioSource))]
    public class TonePlayer : Singleton<TonePlayer>
    {
        public int sampleRate = 44100;

        public int position = 0;

        public bool CurrentlyPlaying
        {
            get { return toneSource.isPlaying; }
        }

        [SerializeField]
        private AudioSource toneSource = null;

        private AudioClip theSineClip;

        private int currentFrequency = 0;


        private void Start()
        {
            if (null == toneSource)
            {
                toneSource = GetComponent<AudioSource>();
            }
        }

        public void PlayTone(Tone tone)
        {
            if (!CurrentlyPlaying)
            {
                currentFrequency = TestManager.frequencies[tone.FrequencyIndex];

                theSineClip = AudioClip.Create("CurrentTone", sampleRate * 2, 1, sampleRate, false, OnAudioRead);
                //AudioSettings.speakerMode = AudioSpeakerMode.Mono;

                toneSource.clip = theSineClip;
                toneSource.loop = true;
                toneSource.panStereo = (int)tone.Ear;
                toneSource.volume = tone.dB / 90; // 90 --- 1
                                                  // dB --- X

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

        //public void ToggleEar()
        //{
        //    if (CurrentEar == Ear.Left)
        //        CurrentEar = Ear.Right;
        //    else
        //        CurrentEar = Ear.Left;
        //    //return CurrentEar;
        //}
    }
}
