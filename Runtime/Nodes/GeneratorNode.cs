namespace Chinchillada.PCGraph
{
    using System;
    using GraphProcessor;

    [Serializable]
    public abstract class GeneratorNode<T> : BaseNode, IGeneratorNode<T>
    {
        [Output] public T output;

        public T Result => this.output;

        public event Action<T> Generated;

        protected override void Process()
        {
            var result = this.Generate();

            this.OnGenerate(result);
        }

        public abstract T Generate();

        protected void OnGenerate(T result)
        {
            this.output = result;
            this.Generated?.Invoke(result);
        }

        public override string ToString() => $"{this.graph.name}/{this.name}";
    }
}