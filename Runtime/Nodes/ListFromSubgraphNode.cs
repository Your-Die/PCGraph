using System;
using System.Collections.Generic;
using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    [Serializable]
    public class ListFromSubgraphNode<T> : GeneratorNode<List<T>>, IUsesRNG
    {
        [Input, ShowAsDrawer] public int amount;

        [Input, ShowAsDrawer] public PCGraph graph;

        public IRNG RNG { get; set; }

        public override List<T> Generate()
        {
            var list = new List<T>(this.amount);

            var processor = new PCGraphProcessor(this.graph, this.RNG);
            for (var i = 0; i < this.amount; i++)
            {
                var item = processor.Generate<T>();
                list.Add(item);
            }

            return list;
        }
    }
}