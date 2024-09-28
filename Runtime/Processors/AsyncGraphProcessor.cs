using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AsyncGraphProcessor : GraphProcessorBase
    {
        private readonly float durationPerNode;
        private readonly IRNG  random;

        private const float FrameDuration = 1 / 60f;
        
        public BaseNode CurrentNode { get; private set; }

        public AsyncGraphProcessor(BaseGraph graph, float durationPerNode, IRNG random = null) : base(graph)
        {
            this.durationPerNode = durationPerNode;
            this.random = random ?? UnityRandom.Shared;
        }

        public override void Run() => this.RunAsync().Enumerate();

        public IEnumerable<float> RunAsync()
        {
            foreach (BaseNode node in this.NodesByComputeOrder)
            {
                this.CurrentNode = node;

                if (node is IUsesRNG randomNode)
                    randomNode.RNG = this.random;

                node.PullInputs();

                if (node is IAsyncNode asyncNode)
                {
                    var processor = new AsyncNodeProcessor(asyncNode, this.durationPerNode);
                    var process = processor.Process();

                    while (process.MoveNext())
                        yield return processor.Duration;
                }
                else
                {
                    node.OnProcess();
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

                int expectedIterations = node.ExpectedIterations;
                float durationPerIteration = (durationPerNode / expectedIterations) * node.SpeedFactor;

                if (node.ForceOneFramePerStep || durationPerIteration > FrameDuration)
                {
                    this.iterationsPerFrame = 1;
                    this.Duration = durationPerIteration;
                }
                else
                {
                    this.iterationsPerFrame = Mathf.CeilToInt(FrameDuration / durationPerIteration);
                    this.Duration = -1;
                }
            }

            public IEnumerator Process() => this.node.OnProcessAsync(this.iterationsPerFrame);
        }
    }
}