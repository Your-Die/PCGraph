namespace Chinchillada.PCGraph.Editor
{
    using GraphProcessor;
    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(PCGraph))]
    public class PCGraphAssetInspector : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Edit graph", GUILayout.Height(40)))
            {
                this.OpenWindow();
            }

            base.OnInspectorGUI();
        }

        private void OpenWindow()
        {
            var window      = EditorWindow.GetWindow<PCGraphWindow>();
            var targetGraph = (BaseGraph) this.target;

            window.InitializeGraph(targetGraph);
        }
    }
}