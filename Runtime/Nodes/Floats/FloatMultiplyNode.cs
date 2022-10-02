namespace Chinchillada.PCGraphs.Nodes.Floats
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Floats/Multiply")]
    public class FloatMultiplyNode : FloatArithmeticNode
    {
        public override float Calculate(float a, float b) => a * b;
    }
}