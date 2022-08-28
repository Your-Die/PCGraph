namespace Chinchillada.PCGraph
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public abstract class AsyncGeneratorNode<T> : GeneratorNode<T>, IAsyncNode
    {
        public override T Generate() => this.GenerateAsync().Last();

        public virtual int ExpectedIterations => 1;

        public IEnumerator OnProcessAsync()
        {
            this.inputPorts.PullDatas();

            foreach (var result in this.GenerateAsync())
            {
                this.OnGenerate(result);
                this.InvokeOnProcessed();
                
                yield return null;
            }
            
            this.outputPorts.PushDatas();
        }

        protected abstract IEnumerable<T> GenerateAsync();
    }
}