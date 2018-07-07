namespace Session
{
    public class Assisted : Session
    {
        private static float toneDuration= 2.5f;

        public Assisted( int frequency, float volume, float prePlayDelay) : base(frequency, volume)
        {
        }
    }
}
