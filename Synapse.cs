namespace EmotionSimulation
{
    /// <summary>
    /// The intermediary between neurons.
    /// </summary>
    public class Synapse
    {
        public Neuron PresynapticNeuron { get; }
        public Neuron PostsynapticNeuron { get; }

        public Synapse(Neuron presynapticNeuron, Neuron postsynapticNeuron)
        {
            PresynapticNeuron = presynapticNeuron;
            PostsynapticNeuron = postsynapticNeuron;
        }
    }
}
