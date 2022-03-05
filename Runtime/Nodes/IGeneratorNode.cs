namespace Chinchillada.PCGraph
{
    using System;

    public interface IGeneratorNode<T>
    {
        public T Result { get; }

        event Action<T> Generated;
    }
}