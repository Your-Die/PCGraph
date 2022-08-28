namespace Chinchillada.PCGraph
{
    using System.Collections;

    public interface IAsyncNode
    {
        int ExpectedIterations { get; }

        IEnumerator OnProcessAsync();
    }

    public static class EnumeratorExtensions
    {
        public static void EnumerateFully(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext()) { }
        }
    }
}