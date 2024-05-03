using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    public class ManualGraphProcessor : GraphProcessorBase
    {
        private readonly int seed;
        private IRNG  random;
        private int currentNodeIndex;

        private int executedSteps = 0;
        
        public BaseNode CurrentNode { get; private set; }

        public ManualGraphProcessor(BaseGraph graph, int seed) : base(graph)
        {
            this.seed = seed;
            this.Reset();
        }

        public override void Run()
        {
            while (this.MoveNext()) { }
        }

        public void Reset()
        {
            this.currentNodeIndex = -1;
            this.random = new CRandom(this.seed);
        }

        public bool MovePrevious(int steps = 1)
        {
            if (this.executedSteps < steps)
            {
                this.Reset();
                return this.MoveNext();
            }

            this.Reset();
            return this.MoveUntilStep(this.executedSteps - steps);
        }
        
        public bool MoveUntilStep(int step)
        {
            while (this.executedSteps < step)
            {
                if (!this.MoveNext())
                    return false;
            }

            return true;
        }

        public bool MoveNext(int steps = 1)
        {
            this.currentNodeIndex++;
            if (this.currentNodeIndex >= this.NodesByComputeOrder.Length)
                return false;
            
            this.CurrentNode = this.NodesByComputeOrder[this.currentNodeIndex];
            
            if (this.CurrentNode is IUsesRNG randomNode)
                randomNode.RNG = this.random;

            this.CurrentNode.inputPorts.PullDatas();
            
            if (this.CurrentNode is IAsyncNode asyncNode)
            {
                asyncNode.OnProcessAsync(steps);
            }
            else
            {
                this.CurrentNode.OnProcess();
            }

            this.CurrentNode.outputPorts.PushDatas();
            this.executedSteps += steps;
            return true;
        }
    }
}