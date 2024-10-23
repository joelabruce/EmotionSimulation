namespace EmotionSimulation
{
    /// <summary>
    /// Do not hace axons and only dendrites.
    /// </summary>
    public class AnaxonicNeuron
    {
        public IList<Dendrite> Dendrites { get; }

        public AnaxonicNeuron()
        {
            Dendrites = new List<Dendrite>();
        }
    }
}
