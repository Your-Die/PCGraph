namespace Chinchillada.PCGraph.Editor
{
    using GraphProcessor;
    using UnityEngine;

    public class PCGraphWindow : BaseGraphWindow
    {
        protected override void InitializeWindow(BaseGraph graph)
        {
            this.titleContent = new GUIContent("PCG Graph");

            if (this.graphView == null) 
                this.CreateView();

            this.rootView.Add(this.graphView);
        }

        private void CreateView()
        {
            this.graphView = new PCGraphView(this);
            
            var toolbar = new PCGToolbarView(this.graphView);
            this.graphView.Add(toolbar);
        }

        protected override void OnDestroy()
        {
            this.graphView?.Dispose();
        }
    }
}