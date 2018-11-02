using System;
using Tones.Managers;

namespace Tones.Sessions
{
    /// <summary>
    /// Esta es la clase que define el modo experimental de las sesiones.
    /// El modo exprimental consiste en mas de un tono con una espera aleatoria entre ellos.
    /// </summary>
    [Serializable]
    public class Experimental : Classic
    {
        public Experimental(byte frequencyIndex, int dB, TestManager manager, Tone.EarSide isLeftEar) : base(frequencyIndex, dB, manager, isLeftEar)
        {

        }
    }
}
