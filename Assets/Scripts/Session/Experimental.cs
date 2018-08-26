using Tones.Managers;
using UnityEngine;

namespace Tones.Session
{
    /// <summary>
    /// Esta es la clase que define el modo experimental de las sesiones.
    /// El modo exprimental consiste en mas de un tono con una espera aleatoria entre ellos.
    /// </summary>
    public class Experimental : Session
    {
        private float totalDuration;

        protected int frequency;

        protected float volume;

        private static Vector2 numberOfTones;

        private static Vector2 timeBetweenTones;

        private static float shortToneDuration;
        private static float longToneDuration;

        public Experimental(int frequency, float volume, TestManager manager) : base(frequency, volume, manager)
        {

        }

        private void CreateTones()
        {

        }

    }
}
