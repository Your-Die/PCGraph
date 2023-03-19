namespace Chinchillada.PCGraphs
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Ranges/Range Int")]
    public class RangeIntNode : GeneratorNode<RangeInt>
    {
        [Input, ShowAsDrawer] public int minimum;
        [Input, ShowAsDrawer] public int maximum;

        public override RangeInt Generate() => new RangeInt(this.minimum, this.maximum);
    }
}