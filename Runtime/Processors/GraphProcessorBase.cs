namespace Chinchillada.PCGraph
{
    using System.Collections.Generic;
    using System.Linq;
    using GraphProcessor;

    public abstract class GraphProcessorBase : BaseGraphProcessor
    {
        protected BaseNode[] NodesByComputeOrder { get; private set; }

        protected GraphProcessorBase(BaseGraph graph) : base(graph)
        {
        }
        
        public override void UpdateComputeOrder()
        {
            this.NodesByComputeOrder = GetOrderedNodes().ToArray();

            IEnumerable<BaseNode> GetOrderedNodes()
            {
                this.graph.UpdateComputeOrder();
                return this.graph.nodes.OrderBy(node => node.computeOrder);
            }
        }
    }
}