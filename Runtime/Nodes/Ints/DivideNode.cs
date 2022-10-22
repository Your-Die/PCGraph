namespace Chinchillada.PCGraphs
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Ints/Divide")]
    public class DivideNode : IntMathNode
    {
        protected override int Calculate(int x, int y)
        {
            return x / y;
        }
    }
}