namespace Chinchillada.PCGraph.Editor
{
    using System.Collections.Generic;
    using GraphProcessor;
    using Nodes.Floats;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;

    [NodeCustomEditor(typeof(FloatGeneratorNode))]
    public class FloatGeneratorNodeView : GeneratorNodeView<FloatGeneratorNode, float>
    {
        private FloatField previewField;

        protected override IEnumerable<VisualElement> CreateControls()
        {
            yield return this.previewField = new FloatField {isReadOnly = true};
        }

        protected override void UpdatePreview(float value)
        {
            this.previewField.value = value;
        }
    }
}