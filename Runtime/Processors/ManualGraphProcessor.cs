using System.Collections;
using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    public class ManualGraphProcessor : GraphProcessorBase, IEnumerator
    {
        private readonly IRNG  random;
        private int currentNodeIndex;

        public BaseNode CurrentNode { get; private set; }

        public object Current => this.CurrentNode;

        public ManualGraphProcessor(BaseGraph graph, IRNG random = null) : base(graph)
        {
            this.random = random ?? UnityRandom.Shared;
        }

        public override void Run() => this.EnumerateFully();

        public void Reset()
        {
            this.currentNodeIndex = -1;
        }

        public bool MoveNext()
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
                asyncNode.OnProcessAsync(1);
            }
            else
            {
                this.CurrentNode.OnProcess();
            }

            this.CurrentNode.outputPorts.PushDatas();
            return true;
        }

    }
}