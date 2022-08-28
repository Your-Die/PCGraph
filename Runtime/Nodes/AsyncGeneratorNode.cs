namespace Chinchillada.PCGraph
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [Serializable]
    public abstract class AsyncGeneratorNode<T> : GeneratorNode<T>, IAsyncNode
    {
        [SerializeField, HideInInspector] private int iterations = 10;
        
        public override T Generate() => this.GenerateAsync().Last();

        public virtual int CalculateExpectedIterations()
        {
            return this.iterations;
        }
        
        public IEnumerator OnProcessAsync()
        {
            this.iterations = 0;
            
            this.inputPorts.PullDatas();

            foreach (var result in this.GenerateAsync())
            {
                this.OnGenerate(result);
                this.InvokeOnProcessed();

                this.iterations++;
                yield return null;
            }
            
            this.outputPorts.PushDatas();
        }

        protected abstract IEnumerable<T> GenerateAsync();
    }
}