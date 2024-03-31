namespace Chinchillada.PCGraphs.Editor
{
    using System.Linq;
    using GraphProcessor;
    using JetBrains.Annotations;
    using UnityEngine;

    [UsedImplicitly, NodeCustomEditor(typeof(FloatDistributionNode))]
    public class FloatDistributionNodeView : DistributionNodeView<FloatDistributionNode, float>
    {
        protected override IRange<float> CreateRange(float[] samples)
        {
            return new Range
            {
                Minimum = samples.First(),
                Maximum = samples.Last()
            };
        }
        
        protected override float InverseLerp(float min, float max, float sample)
        {
            return Mathf.InverseLerp(min, max, sample);
        }
    }
}