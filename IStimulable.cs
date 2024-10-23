namespace EmotionSimulation
{
    public interface IStimulable<T> where T : Stimulus
    {
        void Stimulate(T stimulus);
    }
}
