namespace EmotionSimulation
{
    /// <summary>
    /// Connected to the Soma, and is where the axon propogates from.
    /// Responsible for determining if neuron will send an action potential.
    /// </summary>
    public class AxonHillock
    {
        public Axon Axon { get; }

        public AxonHillock()
        {
            Axon = new Axon();
        }
    }
}
