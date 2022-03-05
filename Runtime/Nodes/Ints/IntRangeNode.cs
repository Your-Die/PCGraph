namespace Chinchillada.PCGraph
{
    using System;
    using GraphProcessor;
    using UnityEngine;

    [NodeMenuItem("Ints/Range"), Serializable]
    public class IntRangeNode : RandomGeneratorNode<int>
    {
        [SerializeField, Input, ShowAsDrawer] private int minimum;
        [SerializeField, Input, ShowAsDrawer] private int maximum;

        protected override int Generate(IRNG rng) => rng.Range(this.minimum, this.maximum);
    }
}