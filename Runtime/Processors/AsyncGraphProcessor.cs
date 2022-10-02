namespace Chinchillada.PCGraphs
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;

    public class AsyncGraphProcessor : GraphProcessorBase
    {
        private readonly float          durationPerNode;
        private readonly IFactory<IRNG> randomFactory;

        private const float FrameDuration = 1 / 60f;

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

                node.PullInputs();

                if (node is IAsyncNode asyncNode)
                {
                    var processor = new AsyncNodeProcessor(asyncNode, this.durationPerNode);
                    var process   = processor.Process();

                    while (process.MoveNext())
                        yield return processor.Duration;
                }
                else
                {
                    node.OnProcess();
                    yield return this.durationPerNode;
                }

                node.PushOutputs();
            }
        }

        private readonly struct AsyncNodeProcessor
        {
            private readonly IAsyncNode node;

            private readonly int iterationsPerFrame;

            public float Duration { get; }

            public AsyncNodeProcessor(IAsyncNode node, float durationPerNode)
            {
                this.node = node;

                var expectedIterations   = node.ExpectedIterations;
                var durationPerIteration = durationPerNode / expectedIterations;

                if (durationPerIteration < FrameDuration)
                {
                    this.iterationsPerFrame = Mathf.CeilToInt(FrameDuration / durationPerIteration);
                    this.Duration           = -1;
                }
                else
                {
                    this.iterationsPerFrame = 1;
                    this.Duration           = durationPerIteration;
                }
            }

            public IEnumerator Process() => this.node.OnProcessAsync(this.iterationsPerFrame);
        }
    }
}