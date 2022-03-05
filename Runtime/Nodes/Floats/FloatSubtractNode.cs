namespace Chinchillada.PCGraph.Nodes.Floats
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Floats/Substract")]
    public class FloatSubtractNode : FloatArithmeticNode
    {
        public override float Calculate(float a, float b) => a - b;
    }
}