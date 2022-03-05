namespace Chinchillada.PCGraph.Nodes.Floats
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Floats/Add")]
    public class FloatAddNode : FloatArithmeticNode
    {
        public override float Calculate(float augend, float addend) => augend + addend;
    }
}