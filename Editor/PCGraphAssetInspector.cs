namespace Chinchillada.PCGraph.Editor
{
    using GraphProcessor;
    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;

    [CustomEditor(typeof(PCGraph))]
    public class PCGraphAssetInspector : GraphInspector
    {
        protected override void CreateInspector()
        {
            base.CreateInspector();

            var button = new Button(this.OpenWindow) { text = "Edit Graph" };
            this.root.Add(button);
        }

        private void OpenWindow()
        {
            var window      = EditorWindow.GetWindow<PCGraphWindow>();
            var targetGraph = (BaseGraph)this.target;

            window.InitializeGraph(targetGraph);
        }
    }
}