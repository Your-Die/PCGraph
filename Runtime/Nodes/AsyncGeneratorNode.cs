namespace Chinchillada.PCGraphs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public abstract class AsyncGeneratorNode<T> : GeneratorNode<T>, IAsyncNode
    {
        public override T Generate()
        {
            this.OnBeforeGenerate();

            T result = this.GenerateAsync().Last();
            this.OnGenerate(result);

            this.InvokeOnProcessed();
            return result;
        }

        public virtual int ExpectedIterations => 1;

        public IEnumerator OnProcessAsync(int ticksPerFrame)
        {
            int ticksToGo = ticksPerFrame;

            this.OnBeforeGenerate();

            foreach (T result in this.GenerateAsync())
            {
                this.OnGenerate(result);

                if (--ticksToGo > 0)
                    continue;

                this.InvokeOnProcessed();
                yield return null;
                ticksToGo = ticksPerFrame;
            }

            this.InvokeOnProcessed();
            yield return null;
        }

        protected abstract IEnumerable<T> GenerateAsync();

        protected virtual void OnBeforeGenerate()
        {
        }
    }
}