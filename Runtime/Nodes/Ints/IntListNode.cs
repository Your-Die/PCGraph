using System;
using System.Collections.Generic;
using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    [Serializable, NodeMenuItem("List/Int/From Distribution")]
    public class IntListNode : IntListGeneratorNode, IUsesRNG
    {
        [Input] public IDistribution<int> distribution;
        [Input, ShowAsDrawer] public int count = 10;

        public IRNG RNG { get; set; }

        public override List<int> Generate()
        {
            var list = new List<int>(this.count);

            for (int i = 0; i < this.count; i++)
            {
                var value = this.distribution.Sample(this.RNG);
                list.Add(value);
            }

            return list;
        }

    }
}