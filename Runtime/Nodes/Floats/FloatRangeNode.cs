namespace Chinchillada.PCGraph.Nodes.Floats
{
    using System;
    using GraphProcessor;
    using UnityEngine;

    [Serializable, NodeMenuItem("Floats/Range")]
    public class FloatRangeNode : RandomGeneratorNode<float>
    {
        [SerializeField, Input, ShowAsDrawer] private float minimum;
        [SerializeField, Input, ShowAsDrawer] private float maximum;

        protected override float Generate(IRNG rng) => rng.Range(this.minimum, this.maximum);
    }
}