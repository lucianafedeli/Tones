using UnityEngine;

namespace Tones.Sessions
{
    /// <summary>
    /// Esta clase define la informacion necesaria para reproducir un tono y se encarga de llamar al reproductor.
    /// </summary>
    public class Tone
    {
        private int frequencyIndex;
        public int FrequencyIndex
        {
            get
            {
                return frequencyIndex;
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


        public Tone(int frequencyIndex, float volume, EarSide ear)
        {
            this.frequencyIndex = frequencyIndex;
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
