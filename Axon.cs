namespace EmotionSimulation
{
    /// <summary>
    /// Carries signals to axon terminals.
    /// </summary>
    public class Axon
    {
        public IList<AxonTerminal> AxonTerminals { get; } = new List<AxonTerminal>();
    }
}
