namespace Chinchillada.PCGraphs
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Ints/Subtract")]
    public class SubtractNode : IntMathNode
    {
        protected override int Calculate(int x, int y)
        {
            return x - y;
        }
    }
}