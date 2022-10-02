namespace Chinchillada.PCGraphs
{
    using GraphProcessor;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class PCGraph : BaseGraph
    {
        [OdinSerialize, Required] private IFactory<IRNG> random = new URandomFactory();

        public IFactory<IRNG> RNG => this.random;
    }
}