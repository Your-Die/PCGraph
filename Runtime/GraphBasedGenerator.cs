namespace Chinchillada.PCGraphs
{
    using System;
    using Chinchillada;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    [Serializable]
    public partial class GraphBasedGenerator<T> : IGenerator<T>
    {
        [OdinSerialize, Required] private PCGraph graph;

        [Button]
        public T Generate(IRNG random)
        {
            var graphProcessor = new PCGraphProcessor(this.graph);
            return graphProcessor.Generate<T>();
        }
    }
}