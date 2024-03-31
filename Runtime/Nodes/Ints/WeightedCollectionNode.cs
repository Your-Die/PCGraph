using System;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    [Serializable, NodeMenuItem("Ints/Weighted Collection")]
    public class WeightedCollectionNode : IntDistributionNode
    {
        [Input(allowMultiple = true)] public IEnumerable<WeightedItem<int>> weightedItems;

        public override IDistribution<int> Generate()
        {
            var weightedItems = this.BuildDictionary();
            return new WeightedCollection<int>(weightedItems);
        }

        private IDictionary<int, float> BuildDictionary()
        {
            return this.weightedItems.ToDictionary(item => item.Item, item => item.Weight);
        }
        
        [CustomPortBehavior(nameof(weightedItems))]
        IEnumerable< PortData > GetPortsForInputs(List< SerializableEdge > edges)
        {
            yield return new PortData{ displayName = "In ", displayType = typeof(WeightedItem<int>), acceptMultipleEdges = true};
        }

        [CustomPortInput(nameof(weightedItems), typeof(float), allowCast = true)]
        public void GetInputs(List< SerializableEdge > edges)
        {
            weightedItems = edges.Select(e => (WeightedItem<int>)e.passThroughBuffer);
        }
    }
}