namespace EmotionSimulation
{
    /// <summary>
    /// Stores Signaling Molecules (NeuroTransmitters) and releases them 
    /// </summary>
    public class SynapticVessicle
    {
        public IList<Neurotransmitter> Neurotransmitters { get; } = new List<Neurotransmitter>();
    }
}
