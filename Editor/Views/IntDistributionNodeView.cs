using System.Linq;
using GraphProcessor;
using JetBrains.Annotations;
using UnityEngine;

namespace Chinchillada.PCGraphs.Editor
{
    [UsedImplicitly, NodeCustomEditor(typeof(IntDistributionNode))]
    public class IntDistributionNodeView : DistributionNodeView<IntDistributionNode, int>
    {
        protected override IRange<int> CreateRange(int[] samples)
        {
            return new RangeInt(samples.Min(), samples.Max());
        }
        
        protected override float InverseLerp(int min, int max, int sample)
        {
            return Mathf.InverseLerp(min, max, sample);
        }
    }
}