namespace Chinchillada.PCGraphs.Nodes.Floats
{
    using System;
    using Distributions;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Floats/Distributions/Standard Continuous Uniform")]
    public class StandardContinuousUniformNode : FloatDistributionNode
    {
        public override IDistribution<float> Generate()
        {
            return StandardContinuousUniform.Distribution;
        }
    }
}