namespace Chinchillada.PCGraphs
{
    using GraphProcessor;

    public abstract class IntMathNode : GeneratorNode<int>
    {
        [Input, ShowAsDrawer] public int a; 
        [Input, ShowAsDrawer] public int b; 
        
        public override int Generate()
        {
            return this.Calculate(this.a, this.b);
        }

        protected abstract int Calculate(int x, int y);
    }
}