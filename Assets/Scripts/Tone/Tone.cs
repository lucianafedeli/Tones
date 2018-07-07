using UnityEngine;

namespace Tone
{
    /// <summary>
    /// Esta clase define la informacion necesaria para reproducir un tono y se encarga de llamar al reproductor.
    /// </summary>
    public class Tone
    {
        int frequency;
        public int Frequency
        {
            get
            {
                return frequency;
            }

        }

        float volume;
        public float Volume
        {
            get
            {
                return volume;
            }
        }


        public Tone(int frequency, float volume)
        {
            this.frequency = frequency;
            this.volume = volume;
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
