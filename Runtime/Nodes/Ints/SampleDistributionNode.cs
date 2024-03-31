using System;
using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    [Serializable, NodeMenuItem("Ints/Distributions/Sample")]
    public class SampleDistributionNode : GeneratorNode<int>, IUsesRNG
    {
        [Input] public IDistribution<int> distribution;

        public IRNG RNG { get; set; }

        public override int Generate()
        {
            return this.distribution.Sample(this.RNG);
        }
    }
}