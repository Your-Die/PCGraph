using System;
using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    public class ManualGraphProcessor : GraphProcessorBase
    {
        private readonly int seed;

        private int currentNodeIndex;
        private IAsyncNode currentAsyncNode;

        private IRNG random;
        private int executedSteps = 0;

        public BaseNode CurrentNode { get; private set; }

        public object Current => this.CurrentNode;

        public ManualGraphProcessor(BaseGraph graph, int seed) : base(graph)
        {
            this.seed = seed;
            this.Reset();
        }

        public override void Run()
        {
            while (this.MoveNext())
            {
            }
        }

        public void Reset()
        {
            this.currentNodeIndex = -1;
            this.currentAsyncNode = null;
            this.executedSteps = 0;
            this.random = new CRandom(this.seed);
            this.CurrentNode = null;
        }

        public bool MovePrevious(int steps = 1)
        {
            var targetStep = this.executedSteps - steps;
            if (targetStep < 0)
            {
                this.Reset();
                return this.MoveNext();
            }

            this.Reset();
            return this.MoveUntilStep(targetStep);
        }

        public bool MoveNext(int steps = 1)
        {
            if (this.currentAsyncNode != null)
                steps = this.EnumerateSteps(steps);

            while (steps > 0)
            {
                this.currentNodeIndex++;
                if (this.currentNodeIndex >= this.NodesByComputeOrder.Length)
                    return false;

                this.CurrentNode = this.NodesByComputeOrder[this.currentNodeIndex];
                if (this.CurrentNode is IUsesRNG randomNode)
                    randomNode.RNG = this.random;

                this.PullCurrentNode();

                if (this.CurrentNode is IAsyncNode asyncNode)
                {
                    this.currentAsyncNode = asyncNode;
                    this.currentAsyncNode.ResetProcess();
                    
                    steps = this.EnumerateSteps(steps);
                }
                else
                {
                    this.CurrentNode.OnProcess();
                    this.PushCurrentNode();
                }
            }

            return true;
        }

        private bool MoveUntilStep(int step)
        {
            var difference = step - this.executedSteps;
            if (difference <= 0)
                throw new ArgumentException($"sep is in the past: {step}, current step: {this.executedSteps}");

            return this.MoveNext(difference);
        }

        private int EnumerateSteps(int steps)
        {
            var remainingSteps = this.currentAsyncNode.MoveNext(steps);
            if (remainingSteps <= 0)
            {
                this.executedSteps += steps;
                return 0;
            }

            this.executedSteps += steps - remainingSteps;
            
            this.PushCurrentNode();
            this.currentAsyncNode = null;

            return remainingSteps;
        }

        private void PullCurrentNode() => this.CurrentNode.inputPorts.PullDatas();
        private void PushCurrentNode() => this.CurrentNode.outputPorts.PushDatas();
    }
}