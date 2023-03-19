namespace Chinchillada.PCGraphs
{
    using System;
    using GraphProcessor;
    using PCGraphs;

    [Serializable, NodeMenuItem("Floats/From Distribution")]
    public class FloatFromDistributionNode : GeneratorNode<float>, IUsesRNG
    {
        [Input] public IDistribution<float> distribution;

        public IRNG RNG { get; set; }

        public override float Generate() => this.distribution.Sample(this.RNG);
    }
}