using UnityEngine;

namespace Tones.Sessions
{
    /// <summary>
    /// Esta clase define la informacion necesaria para reproducir un tono y se encarga de llamar al reproductor.
    /// </summary>
    public class Tone
    {
        private int frequency;
        public int Frequency
        {
            get
            {
                return frequency;
            }

        }

        private float volume;
        public float Volume
        {
            get
            {
                return volume;
            }
        }

        public enum EarSide
        {
            Left = -1, Right = 1
        }

        private EarSide ear;
        public EarSide Ear
        {
            get { return ear; }
        }


        public Tone(int frequency, float volume, EarSide ear)
        {
            this.frequency = frequency;
            this.volume = volume;
            this.ear = ear;
        }

        public static float LinearToDecibel(float linear)
        {
            return (linear != 0) ? 20f * Mathf.Log10(linear) : -144f;
        }

        public static float DecibelToLinear(float dB)
        {
            return Mathf.Pow(10.0f, dB / 20.0f);
        }
    }
}
