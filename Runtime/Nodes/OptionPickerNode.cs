namespace Chinchillada.MagicCircles.Nodes
{
    using System.Collections.Generic;
    using System.Linq;
    using PCGraph;

    public abstract class OptionPickerNode<T> : GeneratorNode<T>, IUsesRNG
    {
        public IRNG RNG { get; set; }

        public override T Generate()
        {
            var types = this.GetOptions().ToList();
            return this.RNG.Choose<T>(types);
        }

        protected abstract IEnumerable<T> GetOptions();
    }
}