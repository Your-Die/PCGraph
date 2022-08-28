namespace Chinchillada.PCGraph
{
    using System.Collections;

    public interface IAsyncNode
    {
        int ExpectedIterations { get; }

        IEnumerator OnProcessAsync(int ticksPerFrame);
    }

    public static class EnumeratorExtensions
    {
        public static void EnumerateFully(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext()) { }
        }
    }
}