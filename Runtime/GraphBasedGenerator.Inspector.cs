namespace Chinchillada.PCGraphs
{
    
#if UNITY_EDITOR

    using Sirenix.OdinInspector;
    using UnityEditor;
    using UnityEngine;

    public partial class GraphBasedGenerator<T>
    {
        [Button, PropertyOrder(2)]
        private void CreateAssetFromGraph(string assetName)
        {
            if (this.graph == null)
                this.CreateGraph();
            
            AssetDatabase.CreateAsset(this.graph, $"Assets/{assetName}.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = this.graph;
        }

        [Button, PropertyOrder(1)]
        private void CreateGraph() => this.graph = ScriptableObject.CreateInstance<PCGraph>();
    }

#endif
    
}