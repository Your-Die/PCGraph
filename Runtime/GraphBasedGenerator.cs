namespace Chinchillada.PCGraph
{
    using System;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    [Serializable]
    public partial class GraphBasedGenerator<T> : IGenerator<T>, IRandomGenerator<T>
    {
        [OdinSerialize, Required] private PCGraph graph;

        [Button]
        public T Generate()
        {
            var graphProcessor = new PCGraphProcessor(this.graph);
            return graphProcessor.Generate<T>();
        }

        public T Generate(IRNG random)
        {
            var graphProcessor = new PCGraphProcessor(this.graph, random);
            return graphProcessor.Generate<T>();
        }
    }
}