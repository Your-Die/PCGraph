namespace Chinchillada.PCGraphs.Editor
{
    using GraphProcessor;
    using UnityEditor;

    public class PCGraphView : BaseGraphView
    {
        public PCGraph Graph => (PCGraph) this.graph;
        
        public PCGraphView(EditorWindow window) : base(window)
        {
        }
    }
}