namespace Chinchillada.PCGraphs
{
    using System;
    using GraphProcessor;

    public abstract class GeneratorNode : BaseNode
    {
        public abstract object ResultObject { get; }
    }

    [Serializable]
    public abstract class GeneratorNode<T> : GeneratorNode, IGeneratorNode<T>
    {
        [Output] public T output;

        public T Result => this.output;

        public override object ResultObject => this.Result;

        protected override void Process()
        {
            var result = this.Generate();

            this.OnGenerate(result);
        }

        public abstract T Generate();

        protected void OnGenerate(T result)
        {
            this.output = result;
        }

        public override string ToString() => $"{this.graph.name}/{this.name}";
    }
}