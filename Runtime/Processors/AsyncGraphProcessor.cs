namespace Chinchillada.PCGraph
{
    using System.Collections;
    using UnityEngine;

    public class AsyncGraphProcessor : GraphProcessorBase
    {
        private readonly IFactory<IRNG> randomFactory;

        public AsyncGraphProcessor(PCGraph graph) : base(graph)
        {
            this.randomFactory = graph.RNG;
        }

        public override void Run()
        {
            this.RunAsync().EnumerateFully();
        }

        public IEnumerator RunAsync()
        {
            var random = this.randomFactory.Create();
            
            foreach (var node in this.NodesByComputeOrder)
            {
                if (node is IUsesRNG randomNode)
                    randomNode.RNG = random;
                
                if (node is IAsyncNode asyncNode)
                {
                    yield return asyncNode.OnProcessAsync();
                    yield return new WaitForSeconds(0.2f);
                }
                else
                {
                    node.OnProcess();
                }
            }
        }
    }
}