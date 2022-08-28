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
        public IEnumerator OnProcessAsync(int ticksPerFrame)
        {
            int ticksToGo = ticksPerFrame;
            
            foreach (var result in this.GenerateAsync())
            {
                if (--ticksToGo > 0)
                    continue;
                
                this.OnGenerate(result);
                this.InvokeOnProcessed();
                yield return null;
                ticksToGo = ticksPerFrame;
            }
        }

        protected abstract IEnumerable<T> GenerateAsync();
    }
}