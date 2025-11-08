namespace App.Audio.Ambience.Effects
{
    public interface IAmbientEffector
    {
        public bool IsSameAmbienceSource(AmbienceSource ambienceSource);
        public void Apply(AmbienceSource ambienceSource);
        public void Revert(AmbienceSource ambienceSource);
    }
}