namespace EmotionSimulation
{
    /// <summary>
    /// Undiferentiated neuron.
    /// </summary>
    public abstract class Neuron
    {
        public Soma CellBody { get; }

        public Neuron()
        {
            CellBody = new Soma();
        }
    }
}
