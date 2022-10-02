namespace Chinchillada.PCGraphs
{
    using GraphProcessor;

    public abstract class SubgraphNode<T> : GeneratorNode<T>, IUsesRNG
    {
        [Input, ShowAsDrawer] public PCGraph subGraph;

        public IRNG RNG { get; set; }

        public override T Generate()
        {
            var processor = new PCGraphProcessor(this.subGraph, this.RNG);
            return processor.Generate<T>();
        }
    }
}