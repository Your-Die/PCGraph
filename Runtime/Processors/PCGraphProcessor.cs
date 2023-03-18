namespace Chinchillada.PCGraphs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GraphProcessor;

    public class PCGraphProcessor : GraphProcessorBase
    {
        private new readonly PCGraph graph;

        private readonly IRNG random;

        private readonly Dictionary<Type, BaseNode> outputsByType = new Dictionary<Type, BaseNode>();

        public PCGraphProcessor(PCGraph graph, IRNG random = null) : base(graph)
        {
            this.graph = graph;
            this.random = random ?? UnityRandom.Shared;
        }

        public T Generate<T>()
        {
            var outputNode = this.GetOutput<T>();

            this.Run();

            return outputNode.Result;
        }

        public override void Run()
        {
            foreach (var node in this.NodesByComputeOrder)
            {
                if (node is IUsesRNG randomNode)
                    randomNode.RNG = this.random;

                node.OnProcess();
            }
        }

        private IGeneratorNode<T> GetOutput<T>()
        {
            var type = typeof(T);

            return this.outputsByType.TryGetValue(type, out var output)
                ? (IGeneratorNode<T>) output
                : FindOutput();

            IGeneratorNode<T> FindOutput()
            {
                var matchingOutputs = this.graph.graphOutputs.OfType<IGeneratorNode<T>>();

                var node = matchingOutputs.First();

                this.outputsByType[type] = (BaseNode) node;

                return node;
            }
        }
    }
}