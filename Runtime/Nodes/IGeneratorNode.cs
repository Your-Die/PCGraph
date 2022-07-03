namespace Chinchillada.PCGraph
{
    public interface IGeneratorNode<out T>
    {
        public T Result { get; }
    }
}