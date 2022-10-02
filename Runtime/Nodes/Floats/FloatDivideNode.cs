namespace Chinchillada.PCGraphs.Nodes.Floats
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Floats/Divide")]
    public class FloatDivideNode : FloatArithmeticNode
    {
        public override float Calculate(float a, float b) => a / b;
    }
}