namespace EmotionSimulation
{
    /// <summary>
    /// Interneuron: A neuron that connects sensory neurons to motor neurons.
    /// </summary>
    public class BipolarNeuron : Neuron
    {
        public AxonHillock AxonHillock { get; }
        public Dendrite Dendrite { get; }

        public BipolarNeuron()
        {
            AxonHillock = new AxonHillock();
            Dendrite = new Dendrite();
        }
    }
}
