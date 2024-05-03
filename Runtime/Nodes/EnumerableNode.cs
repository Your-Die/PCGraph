namespace Chinchillada.PCGraphs
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IAsyncNode
    {
        int ExpectedIterations { get; }
        
        bool  ForceOneFramePerStep { get; }
        float SpeedFactor          { get; }

        void ResetProcess();
        
        /// <summary>
        /// Moves the amount of <paramref name="steps"/> forward.
        /// </summary>
        /// <returns>
        /// If the node finished before executing the requested amount of <paramref name="steps"/>,
        /// it will return the remainder. Otherwise 0.
        /// </returns>
        int MoveNext(int steps);

        IEnumerator OnProcessAsync(int ticksPerFrame);
    }

    public static class EnumeratorExtensions
    {
        public static List<T> ToList<T>(this IEnumerator<T> enumerator)
        {
            var list = new List<T>();

            while (enumerator.MoveNext()) 
                list.Add(enumerator.Current);

            return list;
        }

        public static void EnumerateFully(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext()) { }
        }
    }
}