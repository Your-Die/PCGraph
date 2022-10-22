namespace Chinchillada.PCGraphs
{
    using System;
    using GraphProcessor;

    [Serializable, NodeMenuItem("Ints/Add")]
    public class AddNode : IntMathNode
    {
        protected override int Calculate(int x, int y)
        {
            return x + y;
        }
    }
}