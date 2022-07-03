namespace Chinchillada.PCGraph
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Bool")]
    public class BoolNode : GeneratorNode<bool>, IUsesRNG
    {
        [Input, ShowAsDrawer] public float chance = 0.5f;

        public IRNG RNG { get; set; }

        public override bool Generate() => this.RNG.Flip(this.chance);
    }
}