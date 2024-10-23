namespace EmotionSimulation
{
    /// <summary>
    /// A collection of neurons and the synapses between them that form a specific function when activated.
    /// </summary>
    public class NeuralCircuit
    {
        // Hyper parameters
        public double learningRate; // eta
        public double momentum; // gamma
        public string decaySchedule = string.Empty;

    }
}
