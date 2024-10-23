namespace EmotionSimulation
{
    /// <summary>
    /// Contains Synaptic Vessicles and is responible for releasing neurotransmitters and responding to action potentials.
    /// </summary>
    public class AxonTerminal
    {
        public IList<SynapticVessicle> SynapticVessicles { get; }

        public AxonTerminal()
        {
            SynapticVessicles = new List<SynapticVessicle>();
        }
    }
}
