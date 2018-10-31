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


                //              L       R
                //  125       95.39f   93.806f
                //  250       94.365f  93.125f
                //  500       90.447f  89.715f
                //  1000      91.712f  88.11f
                //  2000      89.252f  89.673f
                //  4000      82.399f  84.312f
                //  8000      92.714f  92.756f


                switch (currentFrequency)
                {
                    case 125:
                        toneSource.volume = tone.dB / (tone.Ear == Tone.EarSide.Left ? 95.39f : 93.806f);
                        break;
                    case 250:
                        toneSource.volume = tone.dB / (tone.Ear == Tone.EarSide.Left ? 94.365f : 93.125f);
                        break;
                    case 500:
                        toneSource.volume = tone.dB / (tone.Ear == Tone.EarSide.Left ? 90.447f : 89.715f);
                        break;
                    case 1000:
                        toneSource.volume = tone.dB / (tone.Ear == Tone.EarSide.Left ? 91.712f : 88.11f);
                        break;
                    case 2000:
                        toneSource.volume = tone.dB / (tone.Ear == Tone.EarSide.Left ? 89.252f : 89.673f);
                        break;
                    case 4000:
                        toneSource.volume = tone.dB / (tone.Ear == Tone.EarSide.Left ? 82.399f : 84.312f);
                        break;
                    case 8000:
                        toneSource.volume = tone.dB / (tone.Ear == Tone.EarSide.Left ? 92.714f : 92.756f);
                        break;
                }

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
