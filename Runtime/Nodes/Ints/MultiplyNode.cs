namespace Chinchillada.PCGraphs
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Ints/Multiply")]
    public class MultiplyNode : IntMathNode
    {
        protected override int Calculate(int x, int y)
        {
            return x * y;
        }
    }
}