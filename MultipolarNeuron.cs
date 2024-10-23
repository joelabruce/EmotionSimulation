namespace EmotionSimulation
{
    /// <summary>
    /// Motor neuron: a neuron that controls.
    /// </summary>
    public class MultipolarNeuron : Neuron
    {
        public AxonHillock AxonHillock { get; }
        public IList<Dendrite> Dendrites { get; }

        public MultipolarNeuron()
        {
            AxonHillock = new AxonHillock();
            Dendrites = new List<Dendrite>();
        }
    }
}
