using System;
using Tones.Managers;
using UnityEngine;

namespace Tones.Sessions
{
    /// <summary>
    /// Esta es la clase que define el modo experimental de las sesiones.
    /// El modo exprimental consiste en mas de un tono con una espera aleatoria entre ellos.
    /// </summary>
    [Serializable]
    public class Experimental : Classic
    {
        private float totalDuration;

        protected int frequency;

        protected float volume;

        private static Vector2 numberOfTones;

        private static Vector2 timeBetweenTones;

        private static float shortToneDuration;
        private static float longToneDuration;

        public Experimental(byte frequencyIndex, int dB, TestManager manager, Tone.EarSide isLeftEar) : base(frequencyIndex, dB, manager, isLeftEar)
        {

        }

        private void CreateTones()
        {

        }

        public override void StartSession()
        {
            throw new NotImplementedException();
        }
    }
}
