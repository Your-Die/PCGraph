namespace Chinchillada.PCGraph
{
    using System;

    [Serializable]
    public abstract class RandomGeneratorNode<T> : GeneratorNode<T>, IUsesRNG
    {
        public IRNG RNG { private get; set; } = UnityRandom.Shared;

        public override T Generate() => this.Generate(this.RNG);

        protected abstract T Generate(IRNG rng);
    }
}