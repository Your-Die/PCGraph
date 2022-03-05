namespace Chinchillada.PCGraph
{
    using System.Collections;
    using GraphProcessor;

    public interface IAsyncNode
    {
        IEnumerator OnProcessAsync();
    }

    public abstract class AsyncNode : BaseNode, IAsyncNode
    {
        public IEnumerator OnProcessAsync()
        {
            this.inputPorts.PullDatas();

            yield return this.ProcessAsync();
            
            this.InvokeOnProcessed();
            
            this.outputPorts.PushDatas();
        }

        protected override void Process() => this.ProcessAsync().EnumerateFully();

        protected abstract IEnumerator ProcessAsync();

    }

    public static class EnumeratorExtensions
    {
        public static void EnumerateFully(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext()) { }
        }
    }
}