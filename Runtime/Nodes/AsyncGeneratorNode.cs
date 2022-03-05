namespace Chinchillada.PCGraph
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AsyncGeneratorNode<T> : GeneratorNode<T>, IAsyncNode
    {
        public override T Generate() => this.GenerateAsync().Last();

        public IEnumerator OnProcessAsync()
        {
            this.inputPorts.PullDatas();

            foreach (var result in this.GenerateAsync())
            {
                this.OnGenerate(result);
                yield return null;
            }
            
            this.InvokeOnProcessed();
                
            this.outputPorts.PushDatas();
        }

        protected abstract IEnumerable<T> GenerateAsync();
    }
}