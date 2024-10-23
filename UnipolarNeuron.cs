namespace EmotionSimulation
{
    /// <summary>
    /// Sensory Neuron: A neuron that processes incoming stimuli.
    /// Does not occur in humans ??
    /// </summary>
    public class UnipolarNeuron : Neuron
    {
        public AxonHillock AxonHillock { get; }

        public UnipolarNeuron()
        {
            AxonHillock = new AxonHillock();
        }
    }
}
