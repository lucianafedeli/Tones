using System;
using UnityEngine;

namespace Tones.Sessions
{
    /// <summary>
    /// Esta clase define la informacion necesaria para reproducir un tono y se encarga de llamar al reproductor.
    /// </summary>
    [Serializable]
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

        public int dB;


        [Serializable]
        public enum EarSide
        {
            Left = -1, Right = 1
        }

        private EarSide ear;
        public EarSide Ear
        {
            get { return ear; }
        }


        public Tone(int frequencyIndex, int dB, EarSide ear)
        {
            this.frequencyIndex = frequencyIndex;
            this.dB = dB;
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
