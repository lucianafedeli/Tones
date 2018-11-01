using Tones.Sessions;
using UnityEngine;

namespace Tones.Managers
{
    public abstract class TestManager : MonoBehaviour
    {
        [SerializeField]
        protected ToneSettingsManager toneManager;

        public bool OngoingTest = false;

        protected Tone.EarSide ear = Tone.EarSide.Left;

        public static readonly int[] frequencies = { 125, 250, 500, 1000, 2000, 4000, 8000 };

        protected Session currentSession = null;

        protected virtual void Start()
        {
        }

        private void Init()
        {
            OngoingTest = true;
        }

        public void ToggleEar()
        {
            if (ear == Tone.EarSide.Left)
            {
                ear = Tone.EarSide.Right;
            }
            else
            {
                ear = Tone.EarSide.Left;
            }
        }

        public void OnPacientButtonDown()
        {
            currentSession.PacientButtonDown();
        }

        public void OnPacientButtonUp()
        {
            currentSession.PacientButtonUp();
        }

        public virtual void StartTest()
        {
            Init();
        }

        public abstract void SessionEnd(bool sessionSucceeded);
    }
}
