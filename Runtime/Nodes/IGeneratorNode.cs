namespace Chinchillada.PCGraphs
{
    public interface IGeneratorNode<out T>
    {
        public T Result { get; }
    }
}