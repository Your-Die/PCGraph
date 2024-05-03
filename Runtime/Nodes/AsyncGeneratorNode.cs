using GraphProcessor;
using UnityEngine;

namespace Chinchillada.PCGraphs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public abstract class AsyncGeneratorNode<T> : GeneratorNode<T>, IAsyncNode
    {
        [SerializeField, Setting] public float speedFactor = 1;

        [SerializeField, Setting] public int maxIterations = -1;

        private IEnumerator<T> enumerator;
        
        public virtual int  ExpectedIterations   => 1;
        public virtual bool ForceOneFramePerStep => false;

        public float SpeedFactor => this.speedFactor;
        public void ResetProcess()
        {
            this.OnBeforeGenerate();
            this.enumerator = this.GetIterations().GetEnumerator();
        }

        public int MoveNext(int steps)
        {
            for (int step = 0; step < steps; step++)
            {
                if (this.enumerator.MoveNext())
                {
                    this.OnGenerate(this.enumerator.Current);
                }
                else
                {
                    this.InvokeOnProcessed();
                    return steps - step;
                }
            }
            
            this.InvokeOnProcessed();
            return 0;
        }

        public override T Generate()
        {
            this.OnBeforeGenerate();

            T result = this.GetIterations().Last();
            this.OnGenerate(result);

            this.InvokeOnProcessed();
            return result;
        }

        public IEnumerator OnProcessAsync(int ticksPerFrame)
        {
            int ticksToGo = ticksPerFrame;

            this.OnBeforeGenerate();

            foreach (T result in this.GetIterations())
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

        private IEnumerable<T> GetIterations()
        {
            var iterations = this.GenerateAsync();
            
            if (this.maxIterations > 0) 
                iterations = iterations.Take(this.maxIterations);

            return iterations;
        }
    }
}