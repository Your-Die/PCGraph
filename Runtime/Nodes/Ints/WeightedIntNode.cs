using System;
using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    [Serializable, NodeMenuItem("Ints/Weighted Int")]
    public class WeightedIntNode : GeneratorNode<WeightedItem<int>>
    {
        [Input, ShowAsDrawer] public int   value;
        [Input, ShowAsDrawer] public int weight;
        
        public override WeightedItem<int> Generate()
        {
            return new WeightedItem<int>(this.value, this.weight);
        }
    }
}