namespace Chinchillada.PCGraph
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class AsyncGraphProcessor : GraphProcessorBase
    {
        private readonly float          durationPerNode;
        private readonly IFactory<IRNG> randomFactory;

        public AsyncGraphProcessor(PCGraph graph, float durationPerNode) : base(graph)
        {
            this.durationPerNode = durationPerNode;
            this.randomFactory   = graph.RNG;
        }

        public override void Run()
        {
            this.RunAsync().Last();
        }

        public IEnumerable<float> RunAsync()
        {
            var random = this.randomFactory.Create();
            
            foreach (var node in this.NodesByComputeOrder)
            {
                if (node is IUsesRNG randomNode)
                    randomNode.RNG = random;
                
                if (node is IAsyncNode asyncNode)
                {
                    var expectedIterations   = asyncNode.CalculateExpectedIterations();
                    var durationPerIteration = this.durationPerNode / expectedIterations;
                    
                    var enumerator = asyncNode.OnProcessAsync();
                    while (enumerator.MoveNext())
                        yield return durationPerIteration;
                }
                else
                {
                    node.OnProcess();
                    yield return this.durationPerNode;
                }
            }
        }
    }
}